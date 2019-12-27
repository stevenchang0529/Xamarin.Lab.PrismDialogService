using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
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
        public ICommand OnShowConfirm { get; set; }
        public ICommand OnShowSheet { get; set; }
        public ICommand OnMyAlert { get; set; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDialogService dialogService)
            : base(navigationService)
        {
            Title = "Main Page";
            this.OnShowAlert = new DelegateCommand(() =>
              {
                  pageDialogService.DisplayAlertAsync("我是Title", "我是Message", "確定");
              });
            this.OnShowConfirm = new DelegateCommand(async () =>
              {
                 var result=await pageDialogService.DisplayAlertAsync("Title", "Message", "Yes", "No");
                 
              });

            this.OnShowSheet = new DelegateCommand(async () =>
              {
                 var val=await pageDialogService.DisplayActionSheetAsync("title", "OK", "Cancel", "0001", "0002", "00003");
              });

            this.OnMyAlert = new DelegateCommand(() =>
              {

                  var parameters = new DialogParameters
                    {
                        { "title", "我是Title~~" },
                        { "message", "我是MessageMessageMessageMessageMessageMessage!~" }
                    };

                  dialogService.ShowDialog("MyAlert", parameters);
              });
        }
    }

    public class MyAlertModel : BindableBase, IDialogAware, IAutoInitialize
    {
        public event Action<IDialogParameters> RequestClose;
        public ICommand OnClose { get; set; }//關閉Command
        [AutoInitialize(true)]
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _title;
       
        [AutoInitialize(true)]
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
        private string _message;

        public MyAlertModel()
        {
            this.OnClose = new DelegateCommand(() =>
            {
                this.RequestClose(new DialogParameters());
            });
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() {//Alert視窗被關閉時
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
           //這裡可以收到傳來的參數。
        }
    }
}
