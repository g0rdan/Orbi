using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AlbumsViewModelTests
    {
        readonly Mock<IMvxNavigationService> _navigationServiceMock;
        readonly Mock<IDatabaseService> _databaseServiceMock;
        readonly Mock<IDialogService> _dialogServiceMock;

        public AlbumsViewModelTests()
        {
            _navigationServiceMock = new Mock<IMvxNavigationService>();
            _databaseServiceMock = new Mock<IDatabaseService>();
            _dialogServiceMock = new Mock<IDialogService>();

            _databaseServiceMock.Setup(x => x.GetAlbumsAsync()).Returns(Task.Factory.StartNew(() => {
                return new List<Album> {
                    new Album { Title = "cats" },
                    new Album { Title = "dogs" }
                };
            }));
        }

        [Test]
        public async Task CheckVMInit()
        {
            var vm = new AlbumsViewModel(_navigationServiceMock.Object, _databaseServiceMock.Object, _dialogServiceMock.Object);
            vm.Prepare();
            await vm.Initialize();
            Assert.True(vm.Albums.ElementAt(0).Name == "cats");
        }
    }
}
