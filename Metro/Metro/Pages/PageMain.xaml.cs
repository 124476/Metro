using Metro.Models;
using Metro.Servies;
using Metro.Windows;
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
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
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
            var dialog = new OknoTrain(train);
            dialog.ShowDialog();
        }
    }
}
