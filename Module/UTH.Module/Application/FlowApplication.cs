namespace UTH.Module
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;

    /// <summary>
    /// 验证码业务
    /// </summary>
    public class FlowApplication : ApplicationService, IFlowApplication
    {
        IFlowService _service;
        IAccountDomainService _accountService;
        IMemberDomainService _memberService;
        IOrganizeDomainService _organizeService;
        IEmployeDomainService _employeService;

        public FlowApplication(IFlowService service, IAccountDomainService accountService,
            IMemberDomainService memberService, IOrganizeDomainService organizeService, IEmployeDomainService employeService,
            IApplicationSession session, ICachingService caching) :
            base(session, caching)
        {
            _service = service;
            _accountService = accountService;
            _memberService = memberService;
            _organizeService = organizeService;
            _employeService = employeService;
        }

        /// <summary>
        /// 创建流程
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public FlowModel Create(FlowEditInput input)
        {
            var model = _service.Create(input);

            new Thread(new ThreadStart(() =>
            {
                try
                {
                    if (input.FlowType == EnumFlowType.UserImport)
                    {
                        string md5 = StringHelper.Get(input.Param.md5);
                        string name = StringHelper.Get(input.Param.name);
                        UserImport(model, md5, name);
                    }
                }
                catch (Exception ex)
                {
                    model.Message = ExceptionHelper.GetMessage(ex);
                    _service.SetProgress(model);
                }
            })).Start();

            return model;
        }

        /// <summary>
        /// 获取进度
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public FlowModel Progress(Guid flowId)
        {
            return _service.Progress(flowId);
        }

        /// <summary>
        /// 用户导入
        /// </summary>
        /// <param name="model"></param>
        /// <param name="md5"></param>
        /// <param name="name"></param>
        private void UserImport(FlowModel model, string md5, string name)
        {
            var rootPath = EngineHelper.Configuration.FileServer.Upload;
            var fileDir = FilesHelper.GetMd5DirPath(rootPath, md5);
            var fileName = FilesHelper.GetMd5FileName(md5, name);

            model.IsOver = false;
            model.IsSuccess = false;

            FilesHelper.ImportExcel(fileDir, fileName, (total, current, sTotal, sName, sIndex, rowTotal, rowIndex, row) =>
            {
                //rowIndex==0 为表头
                if (rowIndex == 0)
                    return true;

                model.Total = total;
                model.Current = current;
                model.Message = $"{Lang.sysZhengZaiDaoRu} {total} /{current}";

                string actName = string.Empty, password = string.Empty, mobile = string.Empty, email = string.Empty;
                try
                {
                    actName = StringHelper.Get(row.GetCell(0));
                    password = StringHelper.Get(row.GetCell(1));
                    mobile = StringHelper.Get(row.GetCell(2));
                    email = StringHelper.Get(row.GetCell(3));

                    if (sIndex == 0)
                    {
                        using (var unit = EngineHelper.Resolve<IUnitOfWorkManager>().Begin())
                        {
                            string memberName = StringHelper.Get(row.GetCell(4));
                            var account = _accountService.Create(Guid.Empty,
                                actName, mobile, email, null, password, null, null, false);
                            _memberService.Insert(new MemberEntity()
                            {
                                Id = account.Id,
                                Name = _memberService.GetDefaultName(memberName, account),
                                Gender = EnumsHelper.Get<EnumGender>(row.GetCell(5)),
                                Birthdate = DateTimeHelper.Get(row.GetCell(6)),
                            });
                            unit.Complete();
                        }
                    }
                    else if (sIndex == 1)
                    {
                        using (var unit = EngineHelper.Resolve<IUnitOfWorkManager>().Begin())
                        {
                            string code = StringHelper.Get(row.GetCell(4)), orgName = StringHelper.Get(row.GetCell(5)), phone = StringHelper.Get(row.GetCell(6)),
                            fax = StringHelper.Get(row.GetCell(7));

                            var account = _accountService.Create(Guid.Empty,
                                actName, mobile, email, null, password, code, null, false);

                            var input = new OrganizeEntity()
                            {
                                Id = account.Id,
                                Code = code,
                                Name = _organizeService.GetDefaultName(orgName, account),
                                Phone = phone,
                                Fax = fax,
                                AreaCode = "",
                                Address = ""
                            };
                            _organizeService.Insert(input);
                            unit.Complete();
                        }
                    }
                    else if (sIndex == 2)
                    {
                        using (var unit = EngineHelper.Resolve<IUnitOfWorkManager>().Begin())
                        {
                            string orgCode = StringHelper.Get(row.GetCell(4)), code = StringHelper.Get(row.GetCell(5)), empName = StringHelper.Get(row.GetCell(6));

                            var account = _accountService.Create(Guid.Empty,
                                actName, mobile, email, null, password, orgCode, code, false);

                            var input = new EmployeEntity()
                            {
                                Id = account.Id,
                                Organize = orgCode,
                                Code = code,
                                Name = _employeService.GetDefaultName(empName, account)
                            };
                            _employeService.Insert(input);
                            unit.Complete();
                        }
                    }

                    if (total == current)
                    {
                        model.IsOver = true;
                        model.IsSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    model.Errors.Add($"{Lang.sysDaoRu} ->  sheet:{sName} row:{rowIndex} {actName} {ExceptionHelper.GetMessage(ex)}");
                }
                finally
                {
                    _service.SetProgress(model);
                }
                return true;
            });

            model.IsOver = true;
            model.IsSuccess = model.Errors.IsEmpty();
            model.Message = model.IsSuccess ? Lang.sysDaoRuWanCheng : Lang.sysDaoRuShiBai;
            _service.SetProgress(model);
        }

    }
}
