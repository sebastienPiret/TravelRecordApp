using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TravelRecordApp.ViewModel.Commands
{
    public class NavigationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public HomeVM homeViewModel { get; set; }

        public NavigationCommand(HomeVM homeVM)
        {
            homeViewModel = homeVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            homeViewModel.Navigate();
        }
    }
}
