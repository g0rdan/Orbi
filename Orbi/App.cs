﻿using System;
using Acr.UserDialogs;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using Orbi.Services;
using Orbi.ViewModels;

namespace Orbi
{
	public class App : MvxApplication
    {
		public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            SQLitePCL.Batteries_V2.Init();

            Mvx.Resolve<IFileService>().InitFiles();
            Mvx.Resolve<IDatabaseService>().InitConnection();
            Mvx.Resolve<IDatabaseService>().InitTables();

            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

            RegisterNavigationServiceAppStart<MainViewModel>();
        }
    }
}
