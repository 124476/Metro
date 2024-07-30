using Metro.Models;
using Metro.Servies;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Metro.Windows
{
    /// <summary>
    /// Логика взаимодействия для OknoTrain.xaml
    /// </summary>
    public partial class OknoTrain : Window
    {
        DispatcherTimer dispatcherTimer;
        Train contextTrain;
        public OknoTrain(Train train)
        {
            InitializeComponent();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += SetRefresh;
            dispatcherTimer.Start();
            contextTrain = train;

            UplodeBranches();
            Refresh();
        }

        private async void UplodeBranches()
        {
            var stations = (await NetManage.Get<List<Station>>("api/stations")).ToList();
            var startToEnds = (await NetManage.Get<List<StartToEnd>>("api/startToEnds")).ToList();
            startToEnds = startToEnds.Where(x => x.BranchId == contextTrain.BranchId).ToList();
            stations = new List<Station>();
            foreach (StartToEnd startToEnd in startToEnds)
            {
                stations.Add(startToEnd.StationStart);
                stations.Add(startToEnd.StationEnd);
            }
            var nameStations = stations.Select(x => x.Name).Distinct().ToList();

            foreach (string station in nameStations)
            {
                TextBlock textStation = new TextBlock();
                textStation.Text = station;
                textStation.FontSize = 17;
                textStation.HorizontalAlignment = HorizontalAlignment.Center;
                textStation.VerticalAlignment = VerticalAlignment.Bottom;

                RotateTransform rotateTransform = new RotateTransform(-90) //-45
                {
                    CenterX = 25
                };
                textStation.LayoutTransform = rotateTransform;
                GridStations.Children.Add(textStation);
                GridStations.ColumnDefinitions.Add(new ColumnDefinition());
                Grid.SetColumn(textStation, GridStations.ColumnDefinitions.Count - 1);
            }
        }

        private void SetRefresh(object sender, EventArgs e)
        {
            Refresh();
        }

        DispatcherTimer timerOfBorder = new DispatcherTimer();
        int timeOfBorder;
        int span;

        private async void Refresh()
        {
            var stations = (await NetManage.Get<List<Station>>("api/stations")).ToList();
            var startToEnds = (await NetManage.Get<List<StartToEnd>>("api/startToEnds")).ToList();
            startToEnds = startToEnds.Where(x => x.BranchId == contextTrain.BranchId).ToList();
            stations = new List<Station>();
            foreach (StartToEnd startToEnd in startToEnds)
            {
                stations.Add(startToEnd.StationStart);
                stations.Add(startToEnd.StationEnd);
            }
            var nameStations = stations.Select(x => x.Name).Distinct().ToList();

            contextTrain = (await NetManage.Get<Train>($"api/trains/{contextTrain.Id}"));

            if (contextTrain.IsUp)
            {
                Grid.SetColumn(BorderSlider, 0);
                Grid.SetColumnSpan(BorderSlider, nameStations.Count - contextTrain.Stations.Count);
                span = nameStations.Count - contextTrain.Stations.Count;
            }
            else
            {
                Grid.SetColumn(BorderSlider, contextTrain.Stations.Count);
                Grid.SetColumnSpan(BorderSlider, 120);
                span = contextTrain.Stations.Count;
            }
            TextTrain.Text = contextTrain.textStations;

            timerOfBorder.Stop();
            if (!contextTrain.IsRun)
                return;

            timerOfBorder.Interval = TimeSpan.FromMilliseconds(250);
            timerOfBorder.Tick += SetTime;
            timerOfBorder.Start();
        }

        private void SetTime(object sender, EventArgs e)
        {
            if (contextTrain.IsUp)
            {
                if (timeOfBorder % 2 == 0)
                    Grid.SetColumnSpan(BorderSlider, span + 1);
                else
                    Grid.SetColumnSpan(BorderSlider, span);
            }
            else
            {
                if (timeOfBorder % 2 == 0)
                    Grid.SetColumn(BorderSlider, span - 1);
                else
                    Grid.SetColumn(BorderSlider, span);
            }
            timeOfBorder++;
        }
    }
}
