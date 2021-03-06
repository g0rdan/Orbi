﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Moq;
using NUnit.Framework;
using Orbi.Services;

namespace Orbi.Tests
{
    [TestFixture]
    public class DatabaseServiceTests
    {
        readonly Mock<IFileService> _fileServiceMock;

        public DatabaseServiceTests()
        {
            var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _fileServiceMock = new Mock<IFileService>();
            _fileServiceMock
                .Setup(x => x.DatabasePath)
                .Returns(Path.Combine(assemblyFolder, "Assets", "data.sqlite"));

            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
        }

        [Test]
        public void CheckGettingVideos()
        {
            var dbService = new DatabaseService(_fileServiceMock.Object);
            dbService.InitConnection();
            var videos = dbService.GetVideos();
            Assert.True(videos.Any());
        }
    }
}
