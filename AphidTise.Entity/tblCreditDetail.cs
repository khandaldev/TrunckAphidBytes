
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
    
public partial class tblCreditDetail
{

    public int ID { get; set; }

    public Nullable<System.Guid> Aphid { get; set; }

    public string Channel { get; set; }

    public Nullable<int> Credit { get; set; }

    public string Category { get; set; }

    public string File_Size { get; set; }

    public string Path { get; set; }

    public string Title { get; set; }

    public Nullable<System.DateTime> CreatedDate { get; set; }

    public Nullable<System.DateTime> ModifyDate { get; set; }

    public bool IsActive { get; set; }

    public string TrackingID { get; set; }

    public Nullable<bool> LinktoPost { get; set; }

}

}