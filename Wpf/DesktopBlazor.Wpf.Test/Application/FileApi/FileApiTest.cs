using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CefSharp;
using System.Threading.Tasks;
using DesktopBlazor.Contracts;

namespace DesktopBlazor.Wpf.Test
{
    [TestClass]
    public class FileApiTest
    {
        [TestMethod]
        public void GetResourceHandler_ReuqestForFolder_ReturnsFolderJson()
        {
            var setup = new TestSetup();
            setup.FileSystem
                .Setup(x => x.GetFiles(It.Is<string>(y => y == "test")))
                .Returns(new[] { new File { Kind = FileKind.File, Path = "TestFile" } });
            var testee = setup.CreateTestee();

            var result = Encoding.UTF8.GetString(testee.ProcessRequest("test"));

            Assert.AreEqual("[{\"Kind\":0,\"Path\":\"TestFile\"}]", result);
        }

        private class TestSetup
        {
            public Mock<IFileSystem> FileSystem { get; } = new Mock<IFileSystem>();

            public FileApi CreateTestee()
            {
                return new FileApi(FileSystem.Object);
            }
        }
    }
}
