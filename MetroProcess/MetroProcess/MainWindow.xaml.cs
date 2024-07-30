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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MetroProcess
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer;
        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(5);
            dispatcherTimer.Tick += SetRefresh;
            dispatcherTimer.Start();
            Refresh();
        }

        private void SetRefresh(object sender, EventArgs e)
        {
            Refresh();
        }

        private async void Refresh()
        {
            var trains = (await NetManage.Get<List<Train>>("api/trains")).ToList();
            var stations = (await NetManage.Get<List<Station>>("api/stations")).ToList();
            var branches = (await NetManage.Get<List<Branch>>("api/branches")).ToList();
            var startToEnds = (await NetManage.Get<List<StartToEnd>>("api/startToEnds")).ToList();

            App.startToEnds = startToEnds;

            foreach (var train in trains)
            {
                if (train.IsRun)
                {
                    Station stationEnd;
                    try
                    {
                        if (train.IsUp)
                            stationEnd = App.startToEnds.FirstOrDefault(x => x.StationStartId == train.Station.Id && x.BranchId == train.BranchId).StationEnd;
                        else
                            stationEnd = App.startToEnds.FirstOrDefault(x => x.StationEndId == train.Station.Id && x.BranchId == train.BranchId).StationStart;
                        train.StationId = stationEnd.Id;
                    }
                    catch
                    {
                        train.IsUp = !train.IsUp;
                    }
                }
                train.IsRun = !train.IsRun;
                NetManage.Set($"api/trains/{train.Id}", new Train()
                {
                    Id = train.Id,
                    IsUp = train.IsUp,
                    IsRun = train.IsRun,
                    StationId = train.StationId,
                    BranchId = train.BranchId,
                });
            }
        }
    }
}
