
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace AphidTise.Entity
{

using System;
    using System.Collections.Generic;
    
public partial class tblCategory
{

    public tblCategory()
    {

        this.tbl_SocialNetworkStatus = new HashSet<tbl_SocialNetworkStatus>();

        this.tblSocialNetworks = new HashSet<tblSocialNetwork>();

    }


    public int CategoryID { get; set; }

    public string CategoryName { get; set; }

    public Nullable<int> Credit { get; set; }

    public string Channel { get; set; }

    public Nullable<bool> IsActive { get; set; }



    public virtual ICollection<tbl_SocialNetworkStatus> tbl_SocialNetworkStatus { get; set; }

    public virtual ICollection<tblSocialNetwork> tblSocialNetworks { get; set; }

}

}