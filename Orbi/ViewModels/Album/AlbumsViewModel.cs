using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Orbi.Models;
using Orbi.Services;

namespace Orbi.ViewModels
{
    public class AlbumsViewModel : MvxViewModel
    {
        readonly IMvxNavigationService _navigationService;
        readonly IDatabaseService _databaseService;
        readonly IDialogService _dialogService;

        public string Title => "Albums";
        public MvxObservableCollection<AlbumCellViewModel> Albums { get; set; }

        #region Commands
        /// <summary>
        /// Obviously the command for deleting an exist album
        /// </summary>
        public IMvxCommand<AlbumCellViewModel> DeleteAlbumCommand => new MvxCommand<AlbumCellViewModel>(DeleteAlbum);
        /// <summary>
        /// The command opens the list view model with videos inside
        /// </summary>
        public IMvxCommand<AlbumCellViewModel> OpenAlbumCommand => new MvxCommand<AlbumCellViewModel>(OpenAlbum);
        /// <summary>
        /// Creating a new album in new view model
        /// </summary>
        public IMvxAsyncCommand CreateAlbumCommand => new MvxAsyncCommand(CreateAlbum);
        #endregion

        public AlbumsViewModel(IMvxNavigationService navigationService, IDatabaseService databaseService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
            _dialogService = dialogService;
        }

        public override void Prepare()
        {
            Albums = new MvxObservableCollection<AlbumCellViewModel>();
        }

        public async override Task Initialize()
		{
            var albums = await _databaseService.GetAlbumsAsync();
            if (albums != null && albums.Any())
            {
                foreach (var album in albums)
                {
                    var cell = new AlbumCellViewModel
                    {
                        GUID = album.GUID,
                        Name = album.Title
                    };
                    cell.DeleteAction = () => DeleteAlbum(cell);
                    cell.OpenAction = () => OpenAlbum(cell);
                    Albums.Add(cell);
                }
            }
		}

        public void OpenAlbum(AlbumCellViewModel cellVM)
        {
            var album = _databaseService.GetAlbum(cellVM.GUID);
            _navigationService.Navigate<VideosViewModel, VideoParameter>(new VideoParameter
            {
                IsSelecting = false,
                Owner = album
            });
        }

        public void DeleteAlbum(AlbumCellViewModel cellVM)
        {
            var album = _databaseService.GetAlbum(cellVM.GUID);
            if (album != null)
                _databaseService.DeleteAlbum(album);

            Albums.Remove(cellVM);
        }

        public void DeleteAlbum(int index)
        {
            var albumCellVM = Albums.ElementAt(index);
            if (albumCellVM != null)
                _databaseService.DeleteAlbum(albumCellVM.GUID);
            
            Albums.Remove(albumCellVM);
        }

        async Task CreateAlbum()
        {
            var result = await _dialogService.AskToAddAlbum();
            if (result.IsOk)
            {
                var newAlbum = new Album { Title = result.Title };
                _databaseService.AddAlbum(newAlbum);
                Albums.Add(new AlbumCellViewModel { Name = newAlbum.Title });
            }
        }
    }
}
