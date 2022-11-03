using System;
using System.Collections.Generic;

namespace HCI_Project.Models
{
    public partial class Section
    {
        public decimal Crn { get; set; }
        public int? Classid { get; set; }
        public string Prof { get; set; }
        public string Days { get; set; }
        public string Timeslot { get; set; }
        public string Locations { get; set; }
        public string Typeofclass { get; set; }

        public virtual Takeableclass Class { get; set; }
    }
}
