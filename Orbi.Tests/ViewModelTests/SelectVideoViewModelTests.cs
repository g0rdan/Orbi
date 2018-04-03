using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MvvmCross.Core.Navigation;
using MvvmCross.Plugins.Messenger;
using NUnit.Framework;
using Orbi.Models;
using Orbi.Services;
using Orbi.ViewModels;

namespace Orbi.Tests.ViewModelTests
{
    [TestFixture]
    public class SelectVideoViewModelTests
    {
        readonly Mock<IDatabaseService> _databaseServiceMock;
        readonly Mock<IFileService> _fileServiceMock;
        readonly Mock<IMvxNavigationService> _navigationServiceMock;
        readonly Mock<IMvxMessenger> _messengerMock;

        public SelectVideoViewModelTests()
        {
            _fileServiceMock = new Mock<IFileService>();
            _databaseServiceMock = new Mock<IDatabaseService>();
            _navigationServiceMock = new Mock<IMvxNavigationService>();
            _messengerMock = new Mock<IMvxMessenger>();

            _fileServiceMock.Setup(x => x.GetVideoFile(It.IsAny<string>())).Returns(new byte[0]);
            _databaseServiceMock.Setup(x => x.GetVideosAsync()).Returns(Task.Factory.StartNew(() =>
            {
                return new List<Video>
                {
                    new Video { GUID = "1", FileName = "1", Name = "1"},
                    new Video { GUID = "2", FileName = "2", Name = "2" },
                    new Video { GUID = "3", FileName = "3", Name = "3"},
                    new Video { GUID = "4", FileName = "4", Name = "4"},
                    new Video { GUID = "5", FileName = "5", Name = "5"}
                };
            }));
        }

        [Test]
        public async Task CheckAddingVideoBySelect()
        {
            var vm = new SelectVideosViewModel(
                _databaseServiceMock.Object, 
                _fileServiceMock.Object, 
                _navigationServiceMock.Object, 
                _messengerMock.Object
            );
            vm.Prepare();
            await vm.Initialize();
            vm.AddSelectedItem(0);
            Assert.True(vm.SelectedItems.Count == 1);
        }
    }
}
