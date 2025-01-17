//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Campus.School.Management.Logic.DataBaseLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class AssetTree
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AssetTree()
        {
            this.AccountAssets = new HashSet<AccountAsset>();
            this.AssetTree1 = new HashSet<AssetTree>();
            this.EmployeeAssets = new HashSet<EmployeeAsset>();
        }
    
        public int AssetTreeID { get; set; }
        public string AssetNameAR { get; set; }
        public string AssetNameEN { get; set; }
        public string AssetTreeCode { get; set; }
        public Nullable<int> AssetTreeParentID { get; set; }
        public int AssetTreeLevel { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
        public int SchoolID { get; set; }
        public string Notes { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountAsset> AccountAssets { get; set; }
        public virtual AdmSchool AdmSchool { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssetTree> AssetTree1 { get; set; }
        public virtual AssetTree AssetTree2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeAsset> EmployeeAssets { get; set; }
    }
}
