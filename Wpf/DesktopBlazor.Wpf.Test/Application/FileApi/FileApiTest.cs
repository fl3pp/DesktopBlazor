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
        public void ProcessRequest_FileSystemReturnsFiles_ReturnsFolderJson()
        {
            var setup = new TestSetup();
            var requestUrl = new RequestUrl("FileApi", "files.json", new RequestParameter[] { new RequestParameter("Directory", @"C:\temp") });
            setup.FileSystem
                .Setup(x => x.GetFiles(It.Is<string>(y => y == @"C:\temp")))
                .Returns(new[] { new File { Kind = FileKind.File, Path = "TestFile" } });
            var testee = setup.CreateTestee();

            var result = Encoding.UTF8.GetString(
                testee.ProcessRequest(RequestUrl.FromString(requestUrl.ToString())));

            Assert.AreEqual("{\"files\":[{\"kind\":0,\"path\":\"TestFile\"}],\"errorMessage\":null}", result);
        }

        [TestMethod]
        public void ProcessRequest_FileSystemThrowsException_ReturnsFolderJson()
        {
            var setup = new TestSetup();
            var requestUrl = new RequestUrl("FileApi", "files.json", new RequestParameter[] { new RequestParameter("Directory", @"C:\temp") });
            setup.FileSystem
                .Setup(x => x.GetFiles(It.IsAny<string>()))
                .Throws(new System.IO.IOException("Could not load folder"));
            var testee = setup.CreateTestee();

            var result = Encoding.UTF8.GetString(
                testee.ProcessRequest(RequestUrl.FromString(requestUrl.ToString())));

            Assert.AreEqual("{\"files\":null,\"errorMessage\":\"Could not load folder\"}", result);
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
