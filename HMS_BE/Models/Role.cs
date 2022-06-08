using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_BE.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
