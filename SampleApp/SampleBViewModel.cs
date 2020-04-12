using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfSimpleNavi.Navigation;

namespace SampleApp
{
    class SampleBViewModel : INotifyPropertyChanged
    {
        public SampleBViewModel()
        {
            NavigateToACommand =
     new DelegateCommand(() => NavigationManager.NavigateSameArea(this, new SampleAViewModel()));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string Test { get; set; } = "This is B";

        public ICommand NavigateToACommand { get; set; }
    }
}
