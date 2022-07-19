namespace HMS_BE.DTO
{
    public class GroupUserCreateRequest
    {
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
        public bool IsLeadder { get; set; }
    }
}
