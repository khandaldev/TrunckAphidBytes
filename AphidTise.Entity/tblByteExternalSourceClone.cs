
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
    
public partial class tblByteExternalSourceClone
{

    public int ByteExternalSourceCloneId { get; set; }

    public Nullable<System.Guid> CloneId { get; set; }

    public Nullable<int> ByteExternalSourceId { get; set; }

    public string ByteExternalSourceClone { get; set; }



    public virtual tblByteExternalSource tblByteExternalSource { get; set; }

}

}