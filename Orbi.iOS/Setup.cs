using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.Platform;
using Orbi.iOS.Converters;
using Orbi.iOS.Services;
using Orbi.Services;
using UIKit;

namespace Orbi.iOS
{
	public class Setup : MvxIosSetup
    {
		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        public Setup(MvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
            : base(applicationDelegate, presenter)
        {
        }
        
        protected override IMvxApplication CreateApp()
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());
            return new App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.RegisterSingleton<IFileService>(new FileService());
        }

		protected override void FillValueConverters(IMvxValueConverterRegistry registry)
		{
            base.FillValueConverters(registry);
            registry.AddOrOverwrite(nameof(ImageConverter), new ImageConverter());
		}

		protected override IMvxIosViewPresenter CreatePresenter()
        {
            return new MvxIosViewPresenter(ApplicationDelegate, Window);
        }

    }
}
