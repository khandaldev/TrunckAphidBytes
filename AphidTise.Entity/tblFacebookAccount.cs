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
    
    public partial class tblFacebookAccount
    {
        public System.Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Phone { get; set; }
        public byte[] ProfilePicture { get; set; }
        public byte[] FbprofilePicture { get; set; }
        public string RecoveryEmail { get; set; }
        public Nullable<System.Guid> AddressID { get; set; }
        public Nullable<System.Guid> SecurityQuestionID { get; set; }
        public Nullable<int> AccountTypeId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public virtual tblSecurityQuestion tblSecurityQuestion { get; set; }
    }
}
