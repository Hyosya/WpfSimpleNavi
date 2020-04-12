using System;

using System.Windows.Input;

namespace SampleApp
{
    public class DelegateCommand : ICommand
    {
        public DelegateCommand(Action action)
        {
            Action = action;
        }

        private Action Action { get; set; }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Action();
        }
    }
}
