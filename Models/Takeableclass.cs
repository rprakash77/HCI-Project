using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HCI_Project.Models
{
    public partial class Takeableclass
    {
        public Takeableclass()
        {
            Sections = new HashSet<Section>();
        }

        public int Classid { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:G0}")]
        public decimal? Code { get; set; }
        public string Prefix { get; set; }
        [Display(Name="Class Name")]
        public string Classname { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
    }
}
