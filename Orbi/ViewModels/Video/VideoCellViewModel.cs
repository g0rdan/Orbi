using System;
using MvvmCross.Core.ViewModels;

namespace Orbi.ViewModels
{
    public class VideoCellViewModel : MvxViewModel
    {
        public string FileName { get; set; }
        public string Title { get; set; }

        public VideoCellViewModel()
        {
        }
    }
}
