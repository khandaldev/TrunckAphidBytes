
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
    
public partial class tblToolsInfo
{

    public int ID { get; set; }

    public int ToolID { get; set; }

    public System.Guid UserID { get; set; }

    public System.DateTime CreatedOn { get; set; }

    public System.DateTime Modify { get; set; }



    public virtual tblToolsInfo tblToolsInfo1 { get; set; }

    public virtual tblToolsInfo tblToolsInfo2 { get; set; }

}

}