using System;
using System.Collections.Generic;

namespace HCI_Project.Models
{
    public partial class Takeableclass
    {
        public Takeableclass()
        {
            Sections = new HashSet<Section>();
        }

        public int Classid { get; set; }
        public decimal? Code { get; set; }
        public string Prefix { get; set; }
        public string Classname { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
    }
}
