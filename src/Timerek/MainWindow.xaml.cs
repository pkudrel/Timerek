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
using Timerek.Misc;

namespace Timerek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var config = new Config();
            InitializeComponent();
            InitView(config);
            var t = new TimerCounter();
            t.AddAction(SetTime);
            t.Start();
        }

        void SetTime()
        {
            //var time = DateTime.UtcNow.ToString("s");
            //LabelInfo.Content = time;
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LabelInfo.Content = GetText(e.NewValue);
        }

        private string GetText(double d)
        {

            var t = DateTime.Now;
            var tn = t.AddMinutes(d);
            var s = $"Shout down at: {tn:HH:mm:ss} ({d} minutes)";
            return s;
        }
        private void InitView(Config config)
        {
            TimeSlider.Minimum = config.MinTimeInMinutes;
            TimeSlider.Maximum = config.MaxTimeInMinutes;
            var stepsList = Enumerable.Range(config.MinTimeInMinutes, config.MaxTimeInMinutes)
                .Where(x => x % 5 == 0)
                .Select(x=>(double) x)
                .ToList();
            TimeSlider.Ticks = new DoubleCollection(stepsList);
        }
    }
}
