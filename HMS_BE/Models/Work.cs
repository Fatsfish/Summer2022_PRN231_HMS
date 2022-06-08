using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_BE.Models
{
    public partial class Work
    {
        public Work()
        {
            AllowedWorkGroups = new HashSet<AllowedWorkGroup>();
            WorkTickets = new HashSet<WorkTicket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int? CreationUserId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsDelete { get; set; }
        public string Status { get; set; }
        public double Progress { get; set; }
        public int Capacity { get; set; }

        public virtual User CreationUser { get; set; }
        public virtual ICollection<AllowedWorkGroup> AllowedWorkGroups { get; set; }
        public virtual ICollection<WorkTicket> WorkTickets { get; set; }
    }
}
