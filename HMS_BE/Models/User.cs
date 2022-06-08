using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_BE.Models
{
    public partial class User
    {
        public User()
        {
            GroupUsers = new HashSet<GroupUser>();
            HelpRequests = new HashSet<HelpRequest>();
            Leaders = new HashSet<Leader>();
            UserRoles = new HashSet<UserRole>();
            WorkTickets = new HashSet<WorkTicket>();
            Works = new HashSet<Work>();
        }

        public int Id { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<GroupUser> GroupUsers { get; set; }
        public virtual ICollection<HelpRequest> HelpRequests { get; set; }
        public virtual ICollection<Leader> Leaders { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<WorkTicket> WorkTickets { get; set; }
        public virtual ICollection<Work> Works { get; set; }
    }
}
