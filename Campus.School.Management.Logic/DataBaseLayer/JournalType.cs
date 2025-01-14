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
    
    public partial class JournalType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JournalType()
        {
            this.AccountDailyJournals = new HashSet<AccountDailyJournal>();
        }
    
        public int ID { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int CompanyId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDefault { get; set; }
    
        public virtual AdmCompany AdmCompany { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountDailyJournal> AccountDailyJournals { get; set; }
    }
}