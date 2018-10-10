namespace UTH.Meeting.Win.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Timers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using Newtonsoft.Json.Linq;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Threading;
    using GalaSoft.MvvmLight.Messaging;
    using culture = UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Framework.Wpf;
    using UTH.Domain;
    using UTH.Plug;
    using UTH.Meeting.Domain;

    /// <summary>
    /// 组员信息
    /// </summary>
    public class EmployeViewModel : UTHViewModel
    {
        public EmployeViewModel() : base(culture.Lang.userYongHuGuanLi, "")
        {
            Initialize();
        }

        #region 公共属性

        /// <summary>
        /// 员工列表
        /// </summary>
        public ObservableCollection<EmployeObservable> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }
        private ObservableCollection<EmployeObservable> _items = new ObservableCollection<EmployeObservable>();

        #endregion

        #region 私有变量

        #endregion

        #region 辅助操作

        private void Initialize()
        {
        }

        #endregion

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="form"></param>
        /// <param name="size"></param>
        /// <param name="name"></param>
        /// <param name="status"></param>
        public PagingModel<EmployeOutput> Query(int page, int size = 10, string name = null, int status = -1)
        {
            Items = new ObservableCollection<EmployeObservable>();
            var result = PlugCoreHelper.ApiUrl.User.EmployePaging.GetResult<PagingModel<EmployeOutput>, QueryInput>(new QueryInput()
            {
                Page = page,
                Size = size,
                Query = new JObject() {
                    new JProperty("organize",CurrentUser.User.Organize)
                }
            });
            if (result.Code == EnumCode.成功)
            {
                var i = 1;
                result.Obj.Rows.ForEach((x) =>
                {
                    Items.Add(new EmployeObservable()
                    {
                        Index = i,
                        Id = x.Id,
                        Name = x.Name
                    });
                    i++;
                });
                return result.Obj;
            }
            return null;
        }

        public bool Add(string no, string name, string password)
        {
            var input = new EmployeEditInput()
            {
                Organize = CurrentUser.User.Account,
                No = no,
                Name = name,
                Password = password
            };

            var result = PlugCoreHelper.ApiUrl.User.EmployeInsert.GetResult<EmployeOutput, EmployeEditInput>(input);
            if (result.Code == EnumCode.成功)
            {
                return true;
            }
            return false;
        }
    }
}