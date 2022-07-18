using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_BE.DTO
{
    public class WorkTicketModel
    {
        public WorkTicket WorkTicket { get; set; }
        public Group Group { get; set; }
        public Work Work { get; set; }
        public User User { get; set; }

    }
}
