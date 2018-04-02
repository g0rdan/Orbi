using System;
using MvvmCross.Core.ViewModels;

namespace Orbi.ViewModels
{
    public class AlbumCellViewModel : MvxViewModel
    {
        public string GUID { get; set; }

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

        public Action DeleteAction { get; set; }

        public IMvxCommand DeleteCommand => new MvxCommand(() => DeleteAction?.Invoke());
    }
}
