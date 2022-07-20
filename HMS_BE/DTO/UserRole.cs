using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HMS_BE.DTO
{
    public partial class UserRole
    {
        public int? UserId { get; set; }
        public int? RoleId { get; set; }

        [NotMapped]
        public int Id { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
