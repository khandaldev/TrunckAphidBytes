using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidTise.Entity.Repository
{
    public class RepositoryChat : GenericRepository<tblAdmin>
    {
        static List<myChatUserModel> UsersList = new List<myChatUserModel>();
        static List<myAdminModel> adminlist = new List<myAdminModel>();
       

        public int fetch_acType(string uid)
        {
            int result = 0;
            try
            {
                Guid nuid = Guid.Parse(uid);
                var data = context.tblUsers.Where(m => (m.UserId == nuid)).SingleOrDefault();
                if (data != null)
                {
                    if (data.AccountTypeID == 5)
                    {
                        myAdminModel ob = new myAdminModel();
                        ob.AdminID = data.UserId.ToString();
                        ob.AdminName = data.UserName.ToString();
                        ob.GroupId = System.Guid.NewGuid().ToString();
                        ob.IsFree = true;
                        adminlist.Add(ob);
                        result = int.Parse(data.AccountTypeID.ToString());
                    }
                    else
                    {
                        myChatUserModel obb = new myChatUserModel();
                        obb.GroupId = null;
                        obb.uname = data.UserName.ToString();
                        obb.userId = data.UserId.ToString();
                        if (adminlist.Any(m => m.IsFree == true))
                        {
                            var st = adminlist.FirstOrDefault();
                            if (st != null)
                            {
                                obb.GroupId = st.GroupId;
                                st.IsFree = false;
                            }
                            result = int.Parse(data.AccountTypeID.ToString());
                        }
                        else
                        {
                            result = 9;    // shows no admin available...
                        }
                        UsersList.Add(obb);
                    }

                }
                else 
                {
                    //this portion is for guest when he already registered and refreshes

                }
            }
            catch
            {

            }
            return result;
        }

        public int fetch_guestacType(string uidd)
        {
            int result = 0;
            try
            {
                myChatUserModel obb = new myChatUserModel();
                obb.GroupId = null;
                obb.uname = "Guest";
                obb.userId = uidd;
                if (adminlist.Any(m => m.IsFree == true))
                {
                    var st = adminlist.FirstOrDefault();
                    if (st != null)
                    {
                        obb.GroupId = st.GroupId;
                        st.IsFree = false;
                    }
                }
                else
                {
                    result = 9;    // shows no admin available...
                }
                UsersList.Add(obb);
            }
            catch
            {

            }
            return result;
        }

    }
}
