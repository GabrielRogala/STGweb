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
    
    public partial class Timetables
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Timetables()
        {
            this.Size = 1;
            this.Part = 1;
        }
    
        public int Id { get; set; }
        public int SchoolsId { get; set; }
        public int LessonsId { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int RoomsId { get; set; }
        public int Size { get; set; }
        public int Part { get; set; }
    
        public virtual Schools Schools { get; set; }
        public virtual Lessons Lessons { get; set; }
        public virtual Rooms Rooms { get; set; }
    }
}
