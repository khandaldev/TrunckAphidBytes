
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
    
public partial class tbl_ChannelSubscription
{

    public int ID { get; set; }

    public Nullable<System.Guid> ByterUserId { get; set; }

    public Nullable<System.Guid> PremiumUserid { get; set; }

    public string ChannelImage { get; set; }

    public string Tillte { get; set; }

    public Nullable<bool> Status { get; set; }

    public Nullable<System.DateTime> SubscribeDate { get; set; }

    public Nullable<System.Guid> ChannelID { get; set; }

}

}