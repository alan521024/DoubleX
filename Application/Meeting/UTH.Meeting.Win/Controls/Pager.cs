using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using culture = UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Framework.Wpf;
using UTH.Domain;
namespace UTH.Meeting.Win
{
    /// <summary>
    /// 分页控件
    /// </summary>
    public class Pager2 : Control
    {
        private const int DefaultValue = 0;

        static Pager2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pager2), new FrameworkPropertyMetadata(typeof(Pager2)));
            Initialize();
        }

        #region 公共属性

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get { return (int)this.GetValue(PageIndexProperty); }
            set { this.SetValue(PageIndexProperty, value); }
        }
        public static readonly DependencyProperty PageIndexProperty = DependencyProperty.Register("PageIndex", typeof(int), typeof(Pager2));

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get { return (int)this.GetValue(PageSizeProperty); }
            set { this.SetValue(PageSizeProperty, value); }
        }
        public static readonly DependencyProperty PageSizeProperty = DependencyProperty.Register("PageSize", typeof(int), typeof(Pager2));

        /// <summary>
        /// 数据总数
        /// </summary>
        public int PageTotal
        {
            get { return (int)this.GetValue(PageTotalProperty); }
            set { this.SetValue(PageTotalProperty, value); }
        }
        public static readonly DependencyProperty PageTotalProperty = DependencyProperty.Register("PageTotal", typeof(int), typeof(Pager2));

        /// <summary>
        /// 分页项数
        /// </summary>
        public ObservableCollection<KeyValuePair<int, bool>> PageItems
        {
            get { return (ObservableCollection<KeyValuePair<int, bool>>)this.GetValue(PageItemsProperty); }
            set { this.SetValue(PageItemsProperty, value); }
        }
        public static readonly DependencyProperty PageItemsProperty = DependencyProperty.Register("PageItems", typeof(ObservableCollection<KeyValuePair<int, bool>>), typeof(Pager2));

        /// <summary>
        /// 首页，上一页 是否可用
        /// </summary>
        public bool IsFirst
        {
            get { return (bool)this.GetValue(IsFirstProperty); }
            set { this.SetValue(IsFirstProperty, value); }
        }
        public static readonly DependencyProperty IsFirstProperty = DependencyProperty.Register("IsFirst", typeof(bool), typeof(Pager2));

        /// <summary>
        /// 尾页，下一页 是否可能
        /// </summary>
        public bool IsLast
        {
            get { return (bool)this.GetValue(IsLastProperty); }
            set { this.SetValue(IsLastProperty, value); }
        }
        public static readonly DependencyProperty IsLastProperty = DependencyProperty.Register("IsLast", typeof(bool), typeof(Pager2));


        /// <summary>
        /// 首页
        /// </summary>
        public static RoutedCommand FirstCommand
        {
            get { return _firstCommand; }
        }
        private static RoutedCommand _firstCommand;

        /// <summary>
        /// 尾页
        /// </summary>
        public static RoutedCommand LastCommand
        {
            get { return _lastCommand; }
        }
        private static RoutedCommand _lastCommand;

        /// <summary>
        /// 上一页
        /// </summary>
        public static RoutedCommand PrevCommand
        {
            get { return _prevCommand; }
        }
        private static RoutedCommand _prevCommand;

        /// <summary>
        /// 下一页
        /// </summary>
        public static RoutedCommand NextCommand
        {
            get { return _nextCommand; }
        }
        private static RoutedCommand _nextCommand;

        /// <summary>
        /// 页码选择
        /// </summary>
        public static RoutedCommand SelectCommand
        {
            get { return _selectCommand; }
        }
        private static RoutedCommand _selectCommand;

        /// <summary>
        /// 跳转页面
        /// </summary>
        public static RoutedCommand GotoCommand
        {
            get { return _gotoCommand; }
        }
        private static RoutedCommand _gotoCommand;

        /// <summary>
        /// 页码变更
        /// </summary>
        public static RoutedCommand SizeCommand
        {
            get { return _sizeCommand; }
        }
        private static RoutedCommand _sizeCommand;

        /// <summary>
        /// 分页改变
        /// </summary>
        public static readonly RoutedEvent PagerChangedEvent = EventManager.RegisterRoutedEvent("PagerChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pager2));
        public event RoutedEventHandler PagerChanged
        {
            add { AddHandler(PagerChangedEvent, value); }
            remove { RemoveHandler(PagerChangedEvent, value); }
        }

        #endregion

        #region 私有变量

        /// <summary>
        /// 数据总数
        /// </summary>
        private int DataTotal { get; set; }

        #endregion

        #region 辅助方法

        private static void Initialize()
        {
            #region commands 

            _firstCommand = new RoutedCommand("FirstCommand", typeof(Pager2));
            CommandManager.RegisterClassCommandBinding(typeof(Pager2), new CommandBinding(_firstCommand, OnFirstCommand));

            _lastCommand = new RoutedCommand("LastCommand", typeof(Pager2));
            CommandManager.RegisterClassCommandBinding(typeof(Pager2), new CommandBinding(_lastCommand, OnLastCommand));

            _prevCommand = new RoutedCommand("PrevCommand", typeof(Pager2));
            CommandManager.RegisterClassCommandBinding(typeof(Pager2), new CommandBinding(_prevCommand, OnPrevCommand));

            _nextCommand = new RoutedCommand("NextCommand", typeof(Pager2));
            CommandManager.RegisterClassCommandBinding(typeof(Pager2), new CommandBinding(_nextCommand, OnNextCommand));

            _selectCommand = new RoutedCommand("SelectCommand", typeof(Pager2));
            CommandManager.RegisterClassCommandBinding(typeof(Pager2), new CommandBinding(_selectCommand, OnSelectCommand));

            _gotoCommand = new RoutedCommand("GotoCommand", typeof(Pager2));
            CommandManager.RegisterClassCommandBinding(typeof(Pager2), new CommandBinding(_gotoCommand, OnGotoCommand));
            CommandManager.RegisterClassInputBinding(typeof(Pager2), new InputBinding(_gotoCommand, new KeyGesture(Key.Enter)));

            _sizeCommand = new RoutedCommand("SizeCommand", typeof(Pager2));
            CommandManager.RegisterClassCommandBinding(typeof(Pager2), new CommandBinding(_sizeCommand, OnSizeCommand));
            CommandManager.RegisterClassInputBinding(typeof(Pager2), new InputBinding(_sizeCommand, new KeyGesture(Key.Enter)));

            #endregion
        }

        private static object CoerceValue(DependencyObject element, object value)
        {
            Pager2 control = (Pager2)element;
            int newValue = (int)value;
            return newValue;
        }
        private static void OnFirstCommand(object sender, ExecutedRoutedEventArgs e)
        {
            (sender as Pager2)?.OnFirst();
        }
        private static void OnLastCommand(object sender, ExecutedRoutedEventArgs e)
        {
            (sender as Pager2)?.OnLast();
        }
        private static void OnPrevCommand(object sender, ExecutedRoutedEventArgs e)
        {
            (sender as Pager2)?.OnPrev();
        }
        private static void OnNextCommand(object sender, ExecutedRoutedEventArgs e)
        {
            (sender as Pager2)?.OnNext();
        }
        private static void OnSelectCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var btn = e.OriginalSource as Button;
            btn.CheckNull();
            var selectIndex = (int)btn.DataContext;
            (sender as Pager2)?.OnSelect(selectIndex);
        }
        private static void OnGotoCommand(object sender, ExecutedRoutedEventArgs e)
        {
            (sender as Pager2)?.OnGoto();
        }
        private static void OnSizeCommand(object sender, ExecutedRoutedEventArgs e)
        {
            (sender as Pager2)?.OnSize();
        }


        protected virtual void OnFirst()
        {
            if (this.PageIndex != 1)
            {
                this.PageIndex = 1;
                this.Sync();
                this.Query();
            }
        }
        protected virtual void OnLast()
        {
            if (this.PageIndex != PageTotal)
            {
                this.PageIndex = PageTotal;
                this.Sync();
                this.Query();
            }
        }
        protected virtual void OnPrev()
        {
            if (this.PageIndex > 1)
            {
                this.PageIndex--;
                this.Sync();
                this.Query();
            }
        }
        protected virtual void OnNext()
        {
            if (this.PageIndex < PageTotal)
            {
                this.PageIndex++;
                this.Sync();
                this.Query();
            }
        }
        protected virtual void OnSelect(int pageIndex = 0)
        {
            if (pageIndex > 0)
            {
                this.PageIndex = pageIndex;
            }
            if (this.PageIndex < 1)
            {
                this.PageIndex = 1;
            }
            if (this.PageIndex > PageTotal)
            {
                this.PageIndex = PageTotal;
            }
            this.Sync();
            this.Query();
        }
        protected virtual void OnGoto()
        {
            if (this.PageIndex < 1)
            {
                this.PageIndex = 1;
            }
            if (this.PageIndex > PageTotal)
            {
                this.PageIndex = PageTotal;
            }
            this.Sync();
            this.Query();
        }
        protected virtual void OnSize()
        {
            if (this.PageSize > 0)
            {
                this.Sync();
                this.Query();
            }
        }

        protected virtual void OnValueChanged(RoutedEventArgs e = null)
        {
            if (e == null)
            {
                e = new RoutedEventArgs(PagerChangedEvent, this);
            }
            RaiseEvent(e);
        }

        private void FormatValue()
        {
            //_numberFormatInfo.NumberDecimalDigits = this.DecimalPlaces;
            //string newValueString = this.Value.ToString("f", _numberFormatInfo);
            //his.SetValue(ValueStringPropertyKey, this.PageIndex);
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        public void Configuration(int pageIndex, int pageSize, int dataTotal = 0)
        {
            if (pageIndex > 0)
            {
                this.PageIndex = pageIndex;
            }
            if (pageSize > 0)
            {
                this.PageSize = pageSize;
            }
            if (dataTotal > 0)
            {
                this.DataTotal = dataTotal;
            }
            this.Sync();
            this.Query();
        }

        public void Sync(int dataTotal = -1)
        {
            if (dataTotal > -1)
            {
                this.DataTotal = dataTotal;
            }

            if (this.PageSize > 0)
            {
                this.PageTotal = DataTotal / this.PageSize;
                if (DataTotal % this.PageSize > 0)
                {
                    this.PageTotal++;
                }
            }

            if (this.PageIndex <= 0)
            {
                this.PageIndex = 1;
            }

            if (this.PageTotal <= 0)
            {
                this.PageTotal = 1;
            }

            if (this.PageIndex > this.PageTotal)
            {
                this.PageIndex = this.PageTotal;
            }

            this.PageItems = new ObservableCollection<KeyValuePair<int, bool>>();
            for (var i = 1; i <= this.PageTotal; i++)
            {
                PageItems.Add(new KeyValuePair<int, bool>(i, i != PageIndex));
            }
            IsFirst = PageIndex > 1;
            IsLast = PageIndex < PageTotal;
        }

        public void Query()
        {
            this.OnValueChanged();
        }
    }
}
