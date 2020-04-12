using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfSimpleNavi.Navigation;

namespace SampleApp
{
    public class SampleAViewModel : INotifyPropertyChanged
    {
        public SampleAViewModel()
        {
            NavigateToBCommand =
                 new DelegateCommand(() => NavigationManager.NavigateSameArea(this, new SampleBViewModel()));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string Test { get; set; } = "This is A";

        public ICommand NavigateToBCommand { get; set; } 
    }
}
