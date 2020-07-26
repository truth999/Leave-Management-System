using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagementModels
{
    public class LeaveModel
    {
        public int empid { get; set; }
        public string leavetype { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int leaveduration { get; set; }
        public string leavedescription { get; set; }
        public string leavestatus { get; set; }
        public int leavebalance { get; set; }
    }

    public class ProfileModel
    {
        public int Empid { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime Dateofbirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
}
