using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_BE.DTO
{
    public partial class AllowedWorkGroup
    {
        public int? WorkId { get; set; }
        public int? GroupId { get; set; }
        public int Id { get; set; }

        public virtual Group Group { get; set; }
        public virtual Work Work { get; set; }
    }
}
