using AutoMapper;

namespace HMS_BE.Profiles
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            // Map models and DTOs -- Sources -> Destinations and reverse
            CreateMap<HMS_BE.Models.AllowedWorkGroup, HMS_BE.DTO.AllowedWorkGroup>().ReverseMap();
            CreateMap<HMS_BE.Models.Group, HMS_BE.DTO.Group>().ReverseMap();
            CreateMap<HMS_BE.Models.GroupUser, HMS_BE.DTO.GroupUser>().ReverseMap();
            CreateMap<HMS_BE.Models.HelpRequest, HMS_BE.DTO.HelpRequest>().ReverseMap();
            CreateMap<HMS_BE.Models.Leader, HMS_BE.DTO.Leader>().ReverseMap();
            CreateMap<HMS_BE.Models.Role, HMS_BE.DTO.Role>().ReverseMap();
            CreateMap<HMS_BE.Models.User, HMS_BE.DTO.User>().ReverseMap();
            CreateMap<HMS_BE.Models.UserRole, HMS_BE.DTO.UserRole>().ReverseMap();
            CreateMap<HMS_BE.Models.Work, HMS_BE.DTO.Work>().ReverseMap();
            CreateMap<HMS_BE.Models.WorkTicket, HMS_BE.DTO.WorkTicket>().ReverseMap();
            CreateMap<HMS_BE.Models.GroupUser, HMS_BE.DTO.GroupUserCreateRequest>().ReverseMap();
        }
    }
}
