using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Domain
{
    public class MeetingDomainProfile : IDomainProfile
    {
        public void Configuration()
        {
            //DOT
            EngineHelper.RegisterType(typeof(IValidator<MeetingEditInput>), typeof(MeetingEditInputValidator));
            EngineHelper.RegisterType(typeof(IValidator<MeetingRecordEditInput>), typeof(MeetingRecordEditInputValidator));
            EngineHelper.RegisterType(typeof(IValidator<MeetingTranslationEditInput>), typeof(MeetingTranslationEditInputValidator));
        }

        public void Mapper(IMapperConfigurationExpression config)
        {
            config.CreateMap<MeetingSyncModel, MeetingRecordEditInput>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.RecordId));
            config.CreateMap<MeetingRecordEntity, MeetingSyncModel>()
                .ForMember(dest => dest.RecordId, opts => opts.MapFrom(src => src.Id));
            config.CreateMap<MeetingRecordOutput, MeetingSyncModel>()
                .ForMember(dest => dest.RecordId, opts => opts.MapFrom(src => src.Id));

            config.CreateMap<MeetingSyncModel, MeetingTranslationEditInput>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.TranslationId));
            config.CreateMap<MeetingTranslationEntity, MeetingSyncModel>()
                .ForMember(dest => dest.TranslationId, opts => opts.MapFrom(src => src.Id));
            config.CreateMap<MeetingTranslationOutput, MeetingSyncModel>()
                .ForMember(dest => dest.TranslationId, opts => opts.MapFrom(src => src.Id));


            config.CreateMap<MeetingBase, MeetingEditInput>();
            config.CreateMap<MeetingEditInput, MeetingEntity>();
            config.CreateMap<MeetingEntity, MeetingOutput>();

            config.CreateMap<MeetingRecordBase, MeetingRecordEditInput>();
            config.CreateMap<MeetingRecordEditInput, MeetingRecordEntity>();
            config.CreateMap<MeetingRecordEntity, MeetingRecordOutput>();

            config.CreateMap<MeetingTranslationBase, MeetingTranslationEditInput>();
            config.CreateMap<MeetingTranslationEditInput, MeetingTranslationEntity>();
            config.CreateMap<MeetingTranslationEntity, MeetingTranslationOutput>();

            config.CreateMap<MeetingProfileBase, MeetingProfileEditInput>();
            config.CreateMap<MeetingProfileEditInput, MeetingProfileEntity>();
            config.CreateMap<MeetingProfileEntity, MeetingProfileOutput>();

            config.CreateMap<MeetingProfileOutput, MeetingSettingModel>()
                .ForMember(dest => dest.ProfileId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
