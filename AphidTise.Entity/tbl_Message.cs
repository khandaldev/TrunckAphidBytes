
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
    
public partial class tbl_Message
{

    public int ID { get; set; }

    public Nullable<System.Guid> UserID { get; set; }

    public string CreditPoint { get; set; }

    public Nullable<int> IsNew { get; set; }

    public string Message { get; set; }

    public Nullable<bool> ByterMessage { get; set; }

    public Nullable<bool> ByterLink { get; set; }

    public Nullable<System.Guid> ChannelId { get; set; }

}

}
