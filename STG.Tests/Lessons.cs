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
    
    public partial class Lessons
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lessons()
        {
            this.Timetables = new HashSet<Timetables>();
        }
    
        public int Id { get; set; }
        public int SubjectsId { get; set; }
        public int TeachersId { get; set; }
        public int GroupsId { get; set; }
        public Nullable<int> RoomsId { get; set; }
        public int RoomTypesId { get; set; }
        public string Schedule { get; set; }
        public int SchoolsId { get; set; }
    
        public virtual Groups Groups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Timetables> Timetables { get; set; }
        public virtual Rooms Rooms { get; set; }
        public virtual RoomTypes RoomTypes { get; set; }
        public virtual Schools Schools { get; set; }
        public virtual Subjects Subjects { get; set; }
        public virtual Teachers Teachers { get; set; }
    }
}
