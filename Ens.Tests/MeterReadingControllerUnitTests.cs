using Ens.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Ens.Tests
{
    public class Tests
    {
        private MeterReadingController meterReadingController;
        private Mock<IFormFile> formFile;

        [SetUp]
        public void Setup()
        {
            meterReadingController = new MeterReadingController();
        }

        [Test]
        public void ProcessAndSaveReadings_ReturnInvalidFile()
        {

            IActionResult returnMessage = meterReadingController.ProcessAndSaveReadings(null);
            var notFound = returnMessage as NotFoundObjectResult;
            Assert.IsNotNull(notFound);
            Assert.That(notFound.Value.ToString().ToUpper().Contains("ERROR"));
            Assert.AreEqual(404, notFound.StatusCode);
        }

    }
}