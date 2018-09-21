using System;
using System.Collections.Generic;
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

namespace UTH.Framework.Wpf
{
    /// <summary>
    /// 分页控件
    /// </summary>
    public class Pager : Control
    {
        private const int DefaultValue = 0;

        public Pager() : base()
        {
            SyncValue();
        }

        static Pager()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pager), new FrameworkPropertyMetadata(typeof(Pager)));
        }

        #region 公共属性

        /// <summary>
        /// 当前页码
        /// </summary>
        public int Current
        {
            get { return (int)GetValue(CurrentProperty); }
            set { SetValue(CurrentProperty, value); }
        }

        /// <summary>
        /// Current依赖属性标识
        /// </summary>
        public static readonly DependencyProperty CurrentProperty = DependencyProperty.Register("Current", typeof(int), typeof(Pager));

        #endregion

        #region 事件信息

        /// <summary>
        /// Identifies the ValueChanged routed event.
        /// </summary>
        public static readonly RoutedEvent PageChangedEvent = EventManager.RegisterRoutedEvent("PageChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<int>), typeof(Pager));

        /// <summary>
        /// Occurs when the Value property changes.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<int> PageChanged
        {
            add { AddHandler(PageChangedEvent, value); }
            remove { RemoveHandler(PageChangedEvent, value); }
        }

        #endregion

        #region 私有变量

        public void SyncValue()
        {
            var dd = this.Current;
            this.SetValue(CurrentProperty, Current);
        }

        #endregion

        #region 辅助方法

        #endregion
    }
}
