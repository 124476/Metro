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

namespace Metro.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        DispatcherTimer dispatcherTimer;
        public PageMain()
        {
            InitializeComponent();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(5);
            dispatcherTimer.Tick += SetRefresh;
            dispatcherTimer.Start();

            GetCombo();
            Refresh();
        }

        private async void GetCombo()
        {
            var branches = (await NetManage.Get<List<Branch>>("api/branches")).ToList();
            branches.Insert(0, new Branch { Name = "Все" });
            ComboLines.ItemsSource = branches;
            ComboLines.SelectedIndex = 0;
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

            foreach (var train in trains)
                train.Station = stations.FirstOrDefault(x => x.Id == train.StationId);

            foreach (var train in trains)
                train.Branch = branches.FirstOrDefault(x => x.Id == train.BranchId);

            foreach (var branch in branches)
                branch.StationStart = stations.FirstOrDefault(x => x.Id == branch.StationStartId);

            foreach (var branch in branches)
                branch.StationEnd = stations.FirstOrDefault(x => x.Id == branch.StationEndId);

            foreach (var startToEnd in startToEnds)
                startToEnd.StationStart = stations.FirstOrDefault(x => x.Id == startToEnd.StationStartId);

            foreach (var startToEnd in startToEnds)
                startToEnd.StationEnd = stations.FirstOrDefault(x => x.Id == startToEnd.StationEndId);

            foreach (var startToEnd in startToEnds)
                startToEnd.Branch = branches.FirstOrDefault(x => x.Id == startToEnd.BranchId);

            App.startToEnds = startToEnds;
            
            if (ComboLines.SelectedIndex != 0)
            {
                var branch = ComboLines.SelectedItem as Branch;
                trains = trains.Where(x => x.BranchId == branch.Id).ToList();
            }
            ListTrains.ItemsSource = trains;
        }

        private void ComboLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void ListTrains_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var train = ListTrains.SelectedItem as Train;
            if (train == null)
                return;
            MessageBox.Show(train.Stations);
        }
    }
}
