
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
    
public partial class tblByteSubType
{

    public int ByteSubTypeId { get; set; }

    public Nullable<int> ByteTypeId { get; set; }

    public string ByteSutType { get; set; }



    public virtual tblByteType tblByteType { get; set; }

}

}
