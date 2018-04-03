using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MvvmCross.Core.Navigation;
using NUnit.Framework;
using Orbi.Models;
using Orbi.Services;
using Orbi.ViewModels;

namespace Orbi.Tests
{
    [TestFixture]
    public class AlbumVideosViewModelTests
    {
        readonly Mock<IDatabaseService> _databaseServiceMock;
        readonly Mock<IFileService> _fileServiceMock;
        readonly Mock<IMvxNavigationService> _navigationServiceMock;
        readonly Album _owner;

        public AlbumVideosViewModelTests()
        {
            _fileServiceMock = new Mock<IFileService>();
            _databaseServiceMock = new Mock<IDatabaseService>();
            _navigationServiceMock = new Mock<IMvxNavigationService>();

            _owner = new Album { Title = "Horses" };
            _fileServiceMock.Setup(x => x.GetVideoFile(It.IsAny<string>())).Returns(new byte[0]);
            _databaseServiceMock.Setup(x => x.GetVideosAsync(_owner)).Returns(Task.Factory.StartNew(() =>
            {
                return new List<Video>
                {
                    new Video { GUID = "1", FileName = "1.jpg", Name = "Mustang"},
                    new Video { GUID = "2", FileName = "2.jpg", Name = "Tequila" }
                };
            }));
        }

        [Test]
        public async Task CheckDeletingVideo()
        {
            var position = 1;
            var vm = new AlbumVideosViewModel(_databaseServiceMock.Object, _fileServiceMock.Object, _navigationServiceMock.Object);
            vm.Prepare();
            vm.Prepare(new VideoParameter { Owner = _owner});
            await vm.Initialize();
            Assert.True(vm.Items.Count == 2);
            vm.DeleteVideo(position);
            Assert.True(vm.Items.Count == 1);
        }
    }
}
