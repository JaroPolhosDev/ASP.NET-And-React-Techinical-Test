using Ens.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Ens.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {

        public MeterReadingController() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        /// <summary>
        /// Process the given file, validate readings and save them into DB
        /// This is called by the UI API
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ProcessAndSaveReadings([FromForm] IFormFile file)
        {
            if (file == null)
            {
                return NotFound("Error - No File Supplied");
            }

            DataTable processedCsvDataTable = ExcelDataParser.GetProcessedCsvTable(file);
            string readingsImportResult = TableValidation.ValidateDataTableForReadings(processedCsvDataTable);

            //If the message contains 'Error' message, display not found message
            if (readingsImportResult.ToUpper().Contains("ERROR"))
            {
                return NotFound(readingsImportResult);
            }

            return Ok(readingsImportResult);
        }
    }

}