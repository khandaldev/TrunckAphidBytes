
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
    
public partial class tblMasterAdsType
{

    public tblMasterAdsType()
    {

        this.tblAds = new HashSet<tblAd>();

    }


    public int AdTypeID { get; set; }

    public string AdTypeName { get; set; }

    public Nullable<short> AdFeature { get; set; }



    public virtual ICollection<tblAd> tblAds { get; set; }

}

}
