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
    public class VideosViewModelTests
    {
        readonly Mock<IDatabaseService> _databaseServiceMock;
        readonly Mock<IFileService> _fileServiceMock;
        readonly Mock<IMvxNavigationService> _navigationServiceMock;

        public VideosViewModelTests()
        {
            _fileServiceMock = new Mock<IFileService>();
            _databaseServiceMock = new Mock<IDatabaseService>();
            _navigationServiceMock = new Mock<IMvxNavigationService>();

            _fileServiceMock.Setup(x => x.GetVideoFile(It.IsAny<string>())).Returns(new byte[0]);
            _databaseServiceMock.Setup(x => x.GetVideosAsync()).Returns(Task.Factory.StartNew(() =>
            {
                return new List<Video>
                {
                    new Video { GUID = "1", FileName = "1.jpg", Name = "1"},
                    new Video { GUID = "2", FileName = "2.jpg", Name = "2" }
                }; 
            }));
        }

        [Test]
        public async Task CheckFillingItems()
        {
            var vm = new VideosViewModel(_databaseServiceMock.Object, _fileServiceMock.Object, _navigationServiceMock.Object);
            vm.Prepare();
            await vm.Initialize();
            Assert.True(vm.Items.Count == 2);
        }
    }
}
