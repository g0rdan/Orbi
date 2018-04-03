using System;
using MvvmCross.Core.ViewModels;

namespace Orbi.ViewModels
{
    public class VideoCellViewModel : MvxViewModel
    {
        public string GUID { get; set; }
        public string Title { get; set; }
        public byte[] Data { get; set; }

        public Action DeleteAction { get; set; }
        public IMvxCommand DeleteCommand => new MvxCommand(() => DeleteAction?.Invoke());

        public VideoCellViewModel()
        {
        }
    }
}
