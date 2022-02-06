using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ens.Controllers;
using Ens.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Ens.Tests
{
    class ExcelProcessorUnitTests
    { 

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ProcessFile_ReturnInvalidFile()
        {

            DataTable result = ExcelDataParser.GetProcessedCsvTable(null);
            Assert.IsNull(result);
        }
    }
}
