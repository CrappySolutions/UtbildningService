using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

namespace WPFApplication.Map.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : UserControl
    {
        public enum MapMode
        {
            Standard = 0,
            Positionate = 1
        }

        public INotifyCollectionChanged Issues
        {
            get { return (INotifyCollectionChanged)GetValue(IssuesProperty); }
            set { SetValue(IssuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Issues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IssuesProperty =
            DependencyProperty.Register("Issues", typeof(INotifyCollectionChanged), typeof(MapView), new PropertyMetadata(null, OnChanged));

        private static void OnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((INotifyCollectionChanged)e.NewValue).CollectionChanged += ((MapView)sender).MapView_CollectionChanged;
        }

        private void MapView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var c = new Converters.IssuePushPinConverter();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (DataService.IssueItem item in e.NewItems)
                    {
                       var p = (UIElement)c.Convert(item, null, null, null);
                       map.Children.Add(p);
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }



        public MapMode Mode
        {
            get { return (MapMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(MapMode), typeof(MapView), new PropertyMetadata(MapMode.Standard, OnModeChanged));

        private static void OnModeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var mode = (MapMode)e.NewValue;
            if (mode == MapMode.Positionate)
            {
                ((MapView)sender).map.Cursor = Cursors.Cross;
            }
            else
            {
                ((MapView)sender).map.Cursor = Cursors.Arrow;
            }
        }

        public ICommand PositionateCommand
        {
            get { return (ICommand)GetValue(PositionateCommandProperty); }
            set { SetValue(PositionateCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PositionateCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionateCommandProperty =
            DependencyProperty.Register("PositionateCommand", typeof(ICommand), typeof(MapView), new PropertyMetadata(null));



        public MapView(ViewModels.IMapViewModel mapviewmodel)
        {
            InitializeComponent();
            DataContext = mapviewmodel;
            this.SetBinding(IssuesProperty, new Binding("Issues"));
            this.SetBinding(ModeProperty, new Binding("Mode") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            this.SetBinding(PositionateCommandProperty, new Binding("PositionateCommand"));
            map.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mode == MapMode.Positionate)
            {
                if(PositionateCommand != null && PositionateCommand.CanExecute(null))
                    PositionateCommand.Execute(map.ViewportPointToLocation(e.GetPosition(map)));
                Mode = MapMode.Standard;
                map.Cursor = Cursors.Arrow;
            }
        }
    }
}
