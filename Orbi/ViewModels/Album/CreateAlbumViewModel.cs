using System;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Orbi.Models;
using Orbi.Services;

namespace Orbi.ViewModels
{
    public class CreateAlbumViewModel : MvxViewModel
    {
        readonly IMvxNavigationService _navigationService;
        readonly IDatabaseService _databaseService;

        string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(Name);
            }
        }

        public IMvxCommand CreateCommand => new MvxCommand(Create);
        public IMvxCommand CloseVMCommand => new MvxCommand(CloseViewModel);

        public CreateAlbumViewModel(IMvxNavigationService navigationService, IDatabaseService databaseService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
        }

        void Create()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return;

            var newAlbum = new Album { Title = Name };
            _databaseService.AddAlbum(newAlbum);
        }

        void CloseViewModel()
        {
            _navigationService.Close(this);
        }
    }
}
