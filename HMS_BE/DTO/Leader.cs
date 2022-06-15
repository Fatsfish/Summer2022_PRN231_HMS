using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_BE.DTO
{
    public partial class Leader
    {
        public int Id { get; set; }
        public bool IsLeader { get; set; }
        public int Votes { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
