using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web;

namespace DesktopBlazor.Contracts.Test
{
    [TestClass]
    public class RequestUrlTest
    {
        [TestMethod]
        public void FromString_WithApi_ReturnsApi()
        {
            var stringUrl = "http://test/test.csproj";

            var result = RequestUrl.FromString(stringUrl);

            var apiResult = result.Api;
            Assert.AreEqual("test", apiResult);
        }

        [TestMethod]
        public void FromString_WithRequestedResource_ReturnsResourcePath()
        {
            var stringUrl = "http://test/test.csproj";

            var result = RequestUrl.FromString(stringUrl);

            var resourceResult = result.Resource;
            Assert.AreEqual("test.csproj", resourceResult);
        }

        [TestMethod]
        public void FromString_WithGetParameter_ReturnsParameter()
        {
            var stringUrl = $"http://test/test.csproj?key=value";

            var result = RequestUrl.FromString(stringUrl);

            var parameterResult = result.Parameters.Single();
            Assert.AreEqual("key", parameterResult.Key);
            Assert.AreEqual("value", parameterResult.Value);
        }

        [TestMethod]
        public void FromString_WithTwoGetParameters_ReturnsParameters()
        {
            var stringUrl = $"http://test/test.csproj?key=value&key2=value2";

            var result = RequestUrl.FromString(stringUrl);

            var parameterResult = result.Parameters.Skip(1).Single();
            Assert.AreEqual("key2", parameterResult.Key);
            Assert.AreEqual("value2", parameterResult.Value);
        }

        [TestMethod]
        public void ToString_WithPathAndParameters_ReturnsRequest()
        {
            var api = "testApi";
            var resource = "test/csproj.csproj";
            var parameter1 = new RequestParameter("key1", "value1");
            var parameter2 = new RequestParameter("key2", "value2");
            var testee = new RequestUrl(api, resource, new[] { parameter1, parameter2 });

            var result = testee.ToString();

            Assert.AreEqual("http://testApi/test/csproj.csproj?key1=value1&key2=value2", result);
        }

    }
}
