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
    
    public partial class SubGroupTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubGroupTypes()
        {
            this.Groups1 = new HashSet<Groups>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupsId { get; set; }
    
        public virtual Groups Groups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Groups> Groups1 { get; set; }
    }
}
