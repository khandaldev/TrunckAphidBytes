
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
    
public partial class tbl_AphidLabSoftware
{

    public int ID { get; set; }

    public System.Guid UserID { get; set; }

    public System.Guid SoftwareID { get; set; }

    public string SoftwareTitle { get; set; }

    public string MatrixImagePath { get; set; }

    public string AvalibaleForDownload { get; set; }

    public string DownloadPassword { get; set; }

    public string SoftwareDeiscription { get; set; }

    public System.DateTime DateCreated { get; set; }

}

}
