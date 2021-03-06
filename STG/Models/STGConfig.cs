//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STG.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class STGConfig
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STGConfig()
        {
            this.PopulationSize = 100;
            this.EpocheSize = 500;
            this.NumberOfLessonToPositioning = 5;
            this.BottomBorderOfBestSlots = 0;
            this.TopBorderOfBestSlots = 6;
            this.ProbabilityOfMutation = 5;
            this.Schools = new HashSet<Schools>();
        }
    
        public int Id { get; set; }
        public int PopulationSize { get; set; }
        public int EpocheSize { get; set; }
        public int NumberOfLessonToPositioning { get; set; }
        public int BottomBorderOfBestSlots { get; set; }
        public int TopBorderOfBestSlots { get; set; }
        public int ProbabilityOfMutation { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schools> Schools { get; set; }
    }
}
