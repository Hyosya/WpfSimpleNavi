using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WpfSimpleNavi.Navigation
{
    public static class NavigationManager
    {
        private static Dictionary<string, Area> Areas = new Dictionary<string, Area>();

        public static void NavigateSameArea(INotifyPropertyChanged currentVM, INotifyPropertyChanged newVM)
        {
            var targetArea = Areas.Values.FirstOrDefault(x => x.ViewModel == currentVM);
            if (targetArea is null)
                throw new InvalidOperationException("There is no Area that given by current Parameter");
            targetArea.NavigateTo(newVM);
        }

        public static void Navigate(string areaid, INotifyPropertyChanged newVM)
        {
            if (Areas.TryGetValue(areaid, out var area))
            {
                area.NavigateTo(newVM);
                return;
            }
            throw new InvalidOperationException("Area not found");
        }

        public static Area PublishArea(string id, INotifyPropertyChanged initialViewModel)
        {
            if (Areas.ContainsKey(id))
                throw new InvalidOperationException("Could not use same key.");
            var area = new Area(id, initialViewModel);
            Areas.Add(id, area);
            return area;
        }

        public static void WithdrawArea(string id)
        {
            Areas.Remove(id);
        }
    }
}
