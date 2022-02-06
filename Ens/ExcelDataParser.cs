using DevExpress.Xpo;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Ens.Pages
{
    /// <summary>
    /// Excel Data Parser class
    /// </summary>
    public static class ExcelDataParser
    {
        /// <summary>
        /// Process a given Excel file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable GetProcessedCsvTable(IFormFile file)
        {
            //We could technically check for correct extension here, by doing file.filename.extension,
            //however, we are only allowing CSV and XLSX files currently through the UI.
            
            //Create the results table to return to user with column names
            DataTable resultsTable = new DataTable();
            resultsTable.Columns.Add("Account ID");
            resultsTable.Columns.Add("Reading Date Time");
            resultsTable.Columns.Add("Reading Value");

            //Register the provider to get correct Excel file encoding
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            //Create a reader for the given file
            using (var reader = ExcelReaderFactory.CreateReader(file.OpenReadStream()))
            {
                //Read every row in the file
                while (reader.Read()) //Each row of the file
                {
                    int accountID;
                    DateTime readDateTime;
                    int readValue;

                    //Try parse variables, instead of parsing it directly to get the correct expected values
                    int.TryParse(reader?.GetValue(0)?.ToString(), out accountID);
                    DateTime.TryParse(reader?.GetValue(1)?.ToString(), out readDateTime);
                    int.TryParse(reader?.GetValue(2)?.ToString(), out readValue);

                    //Add the row to the data table
                    resultsTable.Rows.Add(accountID, readDateTime, readValue);
                }
            }

            return resultsTable;
        }

    }

}