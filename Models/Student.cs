using System;
using System.Collections.Generic;

namespace HCI_Project.Models
{
    public partial class Student
    {
        public decimal Uid { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Holds { get; set; }
        public DateOnly? Startdate { get; set; }
    }
}
