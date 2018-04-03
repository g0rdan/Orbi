using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Orbi.Services;

namespace Orbi.Tests
{
    [TestFixture]
    public class DialogServiceTests
    {
        [Test]
        // checking impossibility to use Acr.UserDialogs plugin in that particular type of project
        // useless test, but still...
        public void NothingToCheckTest()
        {
            Assert.Throws<ArgumentException>(Test);
            async void Test()
            {
                var dialogService = new DialogService();
                await dialogService.AskToAddAlbum();
            }
        }
    }
}
