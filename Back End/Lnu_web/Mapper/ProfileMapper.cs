using AutoMapper;
using Lnu_web.Dbo.Chat;
using Lnu_web.Dbo.Departament;
using Lnu_web.Dbo.Schedule;
using Lnu_web.Dbo.User.Student;
using Lnu_web.Dbo.User.Teacher;
using Lnu_web.Models.ChatModel;
using Lnu_web.Models.DepartamentModel;
using Lnu_web.Models.ScheduleModel;
using Lnu_web.Models.User;
using Lnu_web.Models.User.Student;
using Lnu_web.Models.User.Teacher;

namespace Lnu_web.Mapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {

            CreateMap<StudentDbo, StudentModel>()
            .ForMember(dest => dest.CoreStudent, opt => opt.MapFrom(src => src.CoreStudent))
            .ForMember(dest => dest.AcademicalStudent, opt => opt.MapFrom(src => src.AcademicalStudent));


            CreateMap<CoreStudentDbo, CoreStudentModel>();
            CreateMap<AcademicalStudentDbo, AcademicalStudentModel>();

            CreateMap<TeacherDbo, TeacherModel>()
                .ForMember(dest => dest.CoreInfo, opt => opt.MapFrom(src => src.CoreInfo))
                .ForMember(dest => dest.AdditionalInfo, opt => opt.MapFrom(src => src.AdditionalInfo))
                .ForMember(dest => dest.AcademicDetails, opt => opt.MapFrom(src => src.AcademicDetails));

            CreateMap<CoreTeacherDbo, CoreTeacherModel>();
            CreateMap<AdditionalTeacherDbo,  AdditionalTeacherModel>();
            CreateMap<AcademicalTeacherDbo,  AcademicalTeacherModel>();

            CreateMap<Schedule, ScheduleModel>();
            CreateMap<TeacherSchedule, ScheduleModel>();

            CreateMap<DepartamentDbo, DepartamentModel>()
                .ForMember(dest => dest.departamentListModel, opt => opt.MapFrom(src => src.DepartamentsList))
                .ForMember(dest => dest.departamentAboutModel, opt => opt.MapFrom(src => src.DepartamentAbout));

            CreateMap<DepartamentDbo, DepartamentListModel>()
                .ForMember(dest => dest.KafedtaPhoto, opt => opt.MapFrom(src => src.DepartamentsList.KafedtaPhoto))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.DepartamentsList.TeacherName))
                .ForMember(dest => dest.DepartamentName, opt => opt.MapFrom(src => src.DepartamentsList.DepartamentName))
                .ForMember(dest => dest.DepartamentPhone, opt => opt.MapFrom(src => src.DepartamentsList.DepartamentPhone))
                .ForMember(dest => dest.DepartamentLink, opt => opt.MapFrom(src => src.DepartamentsList.DepartamentLink));

            CreateMap<DepartamentDbo, DepartamentAboutModel>()
                .ForMember(dest => dest.DepartamentAbout, opt => opt.MapFrom(src => src.DepartamentAbout.DepartamentAbout))
                .ForMember(dest => dest.DepartamentMission, opt => opt.MapFrom(src => src.DepartamentAbout.DepartamentMission))
                .ForMember(dest => dest.DepartamnetStrategy, opt => opt.MapFrom(src => src.DepartamentAbout.DepartamnetStrategy))
                .ForMember(dest => dest.DepartamentVizia, opt => opt.MapFrom(src => src.DepartamentAbout.DepartamentVizia));

            CreateMap<TeacherDbo, CoreTeacherModel>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CoreInfo.Name))
                 .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.CoreInfo.Title));

            CreateMap<CoreTeacherModel, CoreTeacherModel>();

            CreateMap<Group, GroupModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.GroupName));

            CreateMap<DepartamentAboutDbo, DepartamentAboutModel>();
            CreateMap<DepartamentsListDbo, DepartamentListModel>();

            CreateMap<GroupUsersDbo, UserModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.RecieverName, opt => opt.MapFrom(src => src.User));
            
        }
    }
}
