
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
    
public partial class tblAphidlabAccount
{

    public System.Guid UserId { get; set; }

    public string DeveloperName { get; set; }

    public string UserEmail { get; set; }

    public string Password { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string DOB { get; set; }

    public string Phonenumber { get; set; }

    public string WebsiteUrl { get; set; }

    public string ChannelUrl { get; set; }

    public System.Guid AddressId { get; set; }

    public System.Guid SecurityQuestionID { get; set; }

    public Nullable<int> Accountid { get; set; }

    public Nullable<System.DateTime> CreatedDate { get; set; }

    public Nullable<System.DateTime> UpdateDate { get; set; }

    public Nullable<bool> IsActive { get; set; }

    public string RecoveryEmail { get; set; }



    public virtual tblPersonAddress tblPersonAddress { get; set; }

    public virtual tblSecurityQuestion tblSecurityQuestion { get; set; }

}

}
