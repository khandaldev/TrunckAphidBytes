
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
    
public partial class tblSecurityQuestion
{

    public tblSecurityQuestion()
    {

        this.tblMerchantAccounts = new HashSet<tblMerchantAccount>();

        this.tblAphidlabAccounts = new HashSet<tblAphidlabAccount>();

        this.tblAphidTiseAccounts = new HashSet<tblAphidTiseAccount>();

        this.tblPremiumAccounts = new HashSet<tblPremiumAccount>();

        this.tblByterAccounts = new HashSet<tblByterAccount>();

        this.tblBasicAccounts = new HashSet<tblBasicAccount>();

    }


    public System.Guid SecurityQuestionID { get; set; }

    public string SecurityQuestion1 { get; set; }

    public string Answer1 { get; set; }

    public string SecurityQuestion2 { get; set; }

    public string Answer2 { get; set; }



    public virtual ICollection<tblMerchantAccount> tblMerchantAccounts { get; set; }

    public virtual ICollection<tblAphidlabAccount> tblAphidlabAccounts { get; set; }

    public virtual ICollection<tblAphidTiseAccount> tblAphidTiseAccounts { get; set; }

    public virtual ICollection<tblPremiumAccount> tblPremiumAccounts { get; set; }

    public virtual ICollection<tblByterAccount> tblByterAccounts { get; set; }

    public virtual ICollection<tblBasicAccount> tblBasicAccounts { get; set; }

}

}
