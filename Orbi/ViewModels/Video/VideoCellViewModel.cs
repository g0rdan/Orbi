﻿using System;
using MvvmCross.Core.ViewModels;

namespace Orbi.ViewModels
{
    public class VideoCellViewModel : MvxViewModel
    {
        public string Title { get; set; }
        public byte[] Data { get; set; }

        public VideoCellViewModel()
        {
        }
    }
}
