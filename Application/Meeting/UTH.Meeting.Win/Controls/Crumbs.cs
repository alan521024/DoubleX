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
    /// 面包屑导航 crumbs
    /// </summary>
    public class Crumbs : Control
    {
        static Crumbs()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Crumbs), new FrameworkPropertyMetadata(typeof(Crumbs)));
            Initialize();
        }

        #region 公共属性

        public ObservableCollection<CrumbData> Items
        {
            get { return (ObservableCollection<CrumbData>)this.GetValue(ItemsProperty); }
            set { this.SetValue(ItemsProperty, value); }
        }
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<CrumbData>), typeof(Crumbs));

        /// <summary>
        /// 项选择
        /// </summary>
        public static RoutedCommand SelectCommand
        {
            get { return _selectCommand; }
        }
        private static RoutedCommand _selectCommand;

        /// <summary>
        /// 项选择
        /// </summary>
        public static readonly RoutedEvent ItemSelectEvent = EventManager.RegisterRoutedEvent("ItemSelect", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Crumbs));
        public event RoutedEventHandler ItemSelect
        {
            add { AddHandler(ItemSelectEvent, value); }
            remove { RemoveHandler(ItemSelectEvent, value); }
        }

        #endregion

        #region 私有变量

        #endregion

        #region 辅助方法

        private static void Initialize()
        {
            #region commands 

            _selectCommand = new RoutedCommand("SelectCommand", typeof(Crumbs));
            CommandManager.RegisterClassCommandBinding(typeof(Crumbs), new CommandBinding(_selectCommand, OnSelectCommand));

            #endregion
        }

        private static void OnSelectCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var hyl = e.OriginalSource as Hyperlink;
            hyl.CheckNull();
            (sender as Crumbs)?.OnItemSelect(e.OriginalSource);
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected virtual void OnItemSelect(object source)
        {
            RaiseEvent(new RoutedEventArgs(ItemSelectEvent, source));
        }
    }

    public class CrumbData
    {
        public bool IsBack { get; set; }

        public bool IsHome { get; set; }

        public bool IsText { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public string Tip { get; set; }

        public string Split { get; set; } = ">";
    }
}
