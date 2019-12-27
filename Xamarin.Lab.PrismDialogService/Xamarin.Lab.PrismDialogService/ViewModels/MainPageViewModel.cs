using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Xamarin.Lab.PrismDialogService.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand OnShowAlert { get; set; }


        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Title = "Main Page";
            this.OnShowAlert = new DelegateCommand(() =>
              {
                  pageDialogService.DisplayAlertAsync("我是Title", "我是Message", "確定");
              });
        }
    }
}
