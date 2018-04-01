using System;
using MvvmCross.Core.ViewModels;

namespace Orbi.ViewModels
{
    public class AlbumCellViewModel : MvxViewModel
    {
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

        public AlbumCellViewModel()
        {
        }
    }
}
