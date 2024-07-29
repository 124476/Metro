using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Metro.Models
{
    public class Train
    {
        public int Id { get; set; }
        public int? StationId { get; set; }
        public bool IsUp { get; set; }
        public bool IsRun { get; set; }
        public int? BranchId { get; set; }
        public Station Station { get; set; }
        public Branch Branch { get; set; }

        public string UpLine
        {
            get
            {
                if (!IsUp)
                    return "Конечная: " + Branch.StationStart.Name;
                return "Конечная: " + Branch.StationEnd.Name;
            }
        }
        public string Place
        {
            get
            {
                if (!IsRun)
                    return "Стоит на " + Station.Name;

                Station stationEnd;
                try
                {
                    if (IsUp)
                        stationEnd = App.startToEnds.FirstOrDefault(x => x.StationStartId == Station.Id && x.BranchId == BranchId).StationEnd;
                    else
                        stationEnd = App.startToEnds.FirstOrDefault(x => x.StationEndId == Station.Id && x.BranchId == BranchId).StationStart;
                }
                catch
                {
                    return "Поворачивает в другую сторону на " + Station.Name;
                }

                return "Движется от " + Station.Name + " до " + stationEnd.Name;
            }
        }
        public string Stations
        {
            get
            {
                var stationNow = Station;
                var stationEnd = Branch.StationEnd;
                if (!IsUp)
                    stationEnd = Branch.StationStart;

                var text = stationNow.Name;
                while(stationNow.Id != stationEnd.Id)
                {
                    if (IsUp)
                        stationNow = App.startToEnds.FirstOrDefault(x => x.StationStartId == stationNow.Id && x.BranchId == BranchId).StationEnd;
                    else
                        stationNow = App.startToEnds.FirstOrDefault(x => x.StationEndId == stationNow.Id && x.BranchId == BranchId).StationStart;

                    text += " -> " + stationNow.Name;
                }

                if (!IsRun)
                {
                    var branches = App.startToEnds.Where(x => x.StationStartId == StationId && x.BranchId != BranchId).ToList();

                    if (branches.Count > 0)
                        foreach (var branch in branches)
                            text += "\nМожно пересесть на " + branch.Branch.Name;
                }

                return text;
            }
        }
    }
}
