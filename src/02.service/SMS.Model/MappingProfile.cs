using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
