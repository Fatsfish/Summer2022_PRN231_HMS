using System.Collections.Generic;

namespace HMS_BE.DTO
{
    public class GroupUserRequestModel
    {
        public GroupUser groupUser { get; set; }
        public User user { get; set; }
        public Group group { get; set; }
    }
}
