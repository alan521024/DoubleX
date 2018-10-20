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
            EngineHelper.RegisterType(typeof(IValidator<MeetingEditInput>), typeof(MeetingEditInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<MeetingRecordEditInput>), typeof(MeetingRecordEditInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<MeetingTranslationEditInput>), typeof(MeetingTranslationEditInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<MeetingProfileEditInput>), typeof(MeetingProfileEditInputValidator));
        }

        public void Mapper(IMapperConfigurationExpression config)
        {
            config.CreateMap<MeetingEntity, MeetingDTO>();
            config.CreateMap<MeetingDTO, MeetingEntity>();
            config.CreateMap<MeetingEditInput, MeetingEntity>();
            config.CreateMap<MeetingEditInput, MeetingDTO>();

            config.CreateMap<MeetingRecordEntity, MeetingRecordDTO>();
            config.CreateMap<MeetingRecordDTO, MeetingRecordEntity>();
            config.CreateMap<MeetingRecordEditInput, MeetingRecordEntity>();
            config.CreateMap<MeetingRecordEntity, MeetingRecordOutput>();
            config.CreateMap<MeetingRecordDTO, MeetingRecordOutput>();

            config.CreateMap<MeetingTranslationEntity, MeetingTranslationDTO>();
            config.CreateMap<MeetingTranslationDTO, MeetingTranslationEntity>();
            config.CreateMap<MeetingTranslationEditInput, MeetingTranslationEntity>();

            config.CreateMap<MeetingProfileEntity, MeetingProfileDTO>();
            config.CreateMap<MeetingProfileDTO, MeetingProfileEntity>();
            config.CreateMap<MeetingProfileEditInput, MeetingProfileEntity>();




            config.CreateMap<MeetingSyncModel, MeetingRecordEditInput>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.RecordId));
            config.CreateMap<MeetingSyncModel, MeetingRecordEntity>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.RecordId));
            config.CreateMap<MeetingRecordEntity, MeetingSyncModel>()
                .ForMember(dest => dest.RecordId, opts => opts.MapFrom(src => src.Id));
            config.CreateMap<MeetingRecordOutput, MeetingSyncModel>()
                .ForMember(dest => dest.RecordId, opts => opts.MapFrom(src => src.Id));

            config.CreateMap<MeetingSyncModel, MeetingTranslationEditInput>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.TranslationId));
            config.CreateMap<MeetingSyncModel, MeetingTranslationEntity>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.TranslationId));
            config.CreateMap<MeetingTranslationEntity, MeetingSyncModel>()
                .ForMember(dest => dest.TranslationId, opts => opts.MapFrom(src => src.Id));
            config.CreateMap<MeetingTranslationDTO, MeetingSyncModel>()
                .ForMember(dest => dest.TranslationId, opts => opts.MapFrom(src => src.Id));

            config.CreateMap<MeetingProfileDTO, MeetingSettingModel>()
                .ForMember(dest => dest.ProfileId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
