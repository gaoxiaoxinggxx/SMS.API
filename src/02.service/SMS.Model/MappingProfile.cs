using AutoMapper;
using System.Collections.Generic;
using SMS.Data.Entitys;
using SMS.Model.Response.User;

namespace SMS.Model
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 实体映射
            //CreateMap<ExaminerWorkRecord, ExaminerWorkRecordResponseItem>()
            //    .ForMember(x => x.SpeakDate, opt => opt.MapFrom(src => src.SpeakDate.UtcDateTime))
            //    .ForMember(x => x.WorkStartTime, opt => opt.MapFrom(src => src.WorkStartTime.UtcDateTime))
            //    .ForMember(x => x.WorkEndTime,
            //        opt => opt.MapFrom(src =>
            //            src.WorkEndTime.HasValue ? (DateTime?)src.WorkEndTime.Value.UtcDateTime : null));

            //CreateMap<PageResultModel<ExaminerWorkRecordDetail>, ExaminerWorkRecordDetailsResponse>();
        }
    }
}
