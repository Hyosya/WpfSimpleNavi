using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfSimpleNavi.Navigation
{
    public class Area : INotifyPropertyChanged
    {

        internal Area(string id, INotifyPropertyChanged viewModel)
        {
            _ViewModel = viewModel;
            ID = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private INotifyPropertyChanged _ViewModel;

        /// <summary>
        /// BindingSouce
        /// </summary>
        public INotifyPropertyChanged ViewModel
        {
            get { return _ViewModel; }
            private set
            {
                _ViewModel = value;
                RaisePorpertyChanged();
            }
        }

        public string ID { get; private set; }

        private void RaisePorpertyChanged([CallerMemberName] string memberName = "")
                    => PropertyChanged(this, new PropertyChangedEventArgs(memberName));

        internal void NavigateTo(INotifyPropertyChanged newVM)
        {
            ViewModel = newVM;
        }
    }
}
