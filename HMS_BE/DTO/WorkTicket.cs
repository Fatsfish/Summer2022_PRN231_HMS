using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_BE.DTO
{
    public partial class WorkTicket
    {
        public int Id { get; set; }
        public int WorkId { get; set; }
        public int OwnerId { get; set; }
        public int GroupId { get; set; }
        public bool IsDelete { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Status { get; set; }

        public virtual Group Group { get; set; }
        public virtual User Owner { get; set; }
        public virtual Work Work { get; set; }
    }
}
