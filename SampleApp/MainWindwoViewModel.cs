using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WpfSimpleNavi.Navigation;

namespace SampleApp
{
    public class MainWindwoViewModel : INotifyPropertyChanged
    {
        public MainWindwoViewModel()
        {
            MainContent = NavigationManager.PublishArea("MainArea", new SampleAViewModel());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private INotifyPropertyChanged _MainContent;
        public INotifyPropertyChanged MainContent
        {
            get => _MainContent;
            set
            {
                if (_MainContent == value) return;
                _MainContent = value;
                RaisePropertyChanged();
            }
        }
        private void RaisePropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
