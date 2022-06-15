using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_BE.DTO
{
    public partial class Group
    {
        public Group()
        {
            AllowedWorkGroups = new HashSet<AllowedWorkGroup>();
            GroupUsers = new HashSet<GroupUser>();
            Leaders = new HashSet<Leader>();
            WorkTickets = new HashSet<WorkTicket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<AllowedWorkGroup> AllowedWorkGroups { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
        public virtual ICollection<Leader> Leaders { get; set; }
        public virtual ICollection<WorkTicket> WorkTickets { get; set; }
    }
}
