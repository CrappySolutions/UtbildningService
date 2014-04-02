using CommonLib;
using MapControl;
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

namespace MapLib.View
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : UserControl
    {
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

        public Location MouseLocation
        {
            get { return (Location)GetValue(MouseLocationProperty); }
            set { SetValue(MouseLocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseLocation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseLocationProperty =
            DependencyProperty.Register("MouseLocation", typeof(Location), typeof(MapView), new PropertyMetadata(null));



        public INotifyCollectionChanged IssueCollection
        {
            get { return (INotifyCollectionChanged)GetValue(IssueCollectionProperty); }
            set { SetValue(IssueCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IssueCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IssueCollectionProperty =
            DependencyProperty.Register("IssueCollection", typeof(INotifyCollectionChanged), typeof(MapView), new PropertyMetadata(null, OnCollectionChanged));

        private static void OnCollectionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((INotifyCollectionChanged)args.NewValue).CollectionChanged += ((MapView)sender).IssueCollectionChanged;
        }

        private void IssueCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var convert = new Converters.IssuePushPinConverter();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (CommonLib.AwesomeService.IssueItem item in e.NewItems)
                    {
                        var pin = (UIElement)convert.Convert(item, null, null, null);
                        map.Children.Add(pin);
                    }
                    break;
            }  
        }

        public MapView(ViewModel.IMapViewModel mapviewmodel)
        {
            InitializeComponent();
            DataContext = mapviewmodel;
            this.SetBinding(ModeProperty, new Binding("Mode") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            this.SetBinding(IssueCollectionProperty, new Binding("Issues"));
            this.SetBinding(PositionateCommandProperty, new Binding("PositionateCommand"));
            this.SetBinding(MouseLocationProperty, new Binding("MouseLocation") { Mode = BindingMode.OneWayToSource, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            map.PreviewMouseMove += (a, b) => MouseLocation = map.ViewportPointToLocation(b.GetPosition(map));
            map.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mode == MapMode.Positionate)
            {
                if (PositionateCommand != null && PositionateCommand.CanExecute(null))
                    PositionateCommand.Execute(map.ViewportPointToLocation(e.GetPosition(map)));
                Mode = MapMode.Standard;
                map.Cursor = Cursors.Arrow;
            }
        }
    }
}
