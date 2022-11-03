using System;
using System.Collections.Generic;

namespace HCI_Project.Models
{
    public partial class Hastaken
    {
        public decimal? Uid { get; set; }
        public decimal? Crn { get; set; }

        public virtual Section CrnNavigation { get; set; }
        public virtual Student UidNavigation { get; set; }
    }
}
