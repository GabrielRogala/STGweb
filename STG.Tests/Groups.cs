//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STG.Tests
{
    using System;
    using System.Collections.Generic;
    
    public partial class Groups
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Groups()
        {
            this.Groups1 = new HashSet<Groups>();
            this.Lessons = new HashSet<Lessons>();
            this.SubGroupTypes = new HashSet<SubGroupTypes>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int SchoolsId { get; set; }
        public Nullable<int> ParentGroup { get; set; }
        public Nullable<int> SubGroupTypesId { get; set; }
        public string BlockedHours { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Groups> Groups1 { get; set; }
        public virtual Groups Groups2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lessons> Lessons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubGroupTypes> SubGroupTypes { get; set; }
        public virtual Schools Schools { get; set; }
        public virtual SubGroupTypes SubGroupTypes1 { get; set; }
    }
}
