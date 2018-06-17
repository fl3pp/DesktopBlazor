using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopBlazor.Contracts.Test.Requests
{
    [TestClass]
    public class FilesRequestResponseTest
    {
        [TestMethod]
        public void ToJson_WithError_SerializesToJson()
        {
            var testee = new FilesRequestResponse { ErrorMessage = "This is a error message" };

            var result = testee.ToJson();

            Assert.AreEqual("{\"files\":null,\"errorMessage\":\"This is a error message\"}", result);
        }

        [TestMethod]
        public void ToJson_WithFiles_SerializesJson()
        {
            var file = new File { Kind = FileKind.File, Path = "TempPath" };
            var testee = new FilesRequestResponse { Files = new[] { file } };

            var result = testee.ToJson();

            Assert.AreEqual("{\"files\":[{\"kind\":0,\"path\":\"TempPath\"}],\"errorMessage\":null}", result);
        }

        [TestMethod]
        public void FromJson_WithErrorMessage_ReturnsMessage()
        {
            var json = "{\"files\":null,\"errorMessage\":\"This is a error message\"}";

            var result = FilesRequestResponse.FromJson(json);

            Assert.AreEqual("This is a error message", result.ErrorMessage);
            Assert.IsNull(result.Files);
        }

        [TestMethod]
        public void FromJson_WithFiles_ReturnsFilesAndLeavesErrorMessageEmpty()
        {
            var json = "{\"files\":[{\"kind\":0,\"path\":\"TempPath\"}],\"errorMessage\":null}";

            var result = FilesRequestResponse.FromJson(json);

            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual(result.Files.Single().Path, "TempPath");
        }
    }
}
