
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
    
public partial class tblSocialNetwork
{

    public System.Guid ID { get; set; }

    public Nullable<System.Guid> Aphid { get; set; }

    public string AccessToken { get; set; }

    public Nullable<int> Category { get; set; }

    public Nullable<bool> IsDeleted { get; set; }

    public Nullable<System.DateTime> Expires { get; set; }

    public string Refreshtoken { get; set; }



    public virtual tblCategory tblCategory { get; set; }

}

}
