
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
    
public partial class tbl_Surveys
{

    public int Id { get; set; }

    public System.Guid ByterUserId { get; set; }

    public string TrackingNo { get; set; }

    public string Imagepath { get; set; }

    public int Credits { get; set; }

    public System.DateTime CreatedDate { get; set; }

    public System.DateTime ModifyDate { get; set; }

    public string FeedbackText { get; set; }

    public Nullable<int> IsNew { get; set; }

}

}
