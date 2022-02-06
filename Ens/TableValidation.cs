using DevExpress.Xpo;
using System;
using System.Data;

namespace Ens
{
    public static class TableValidation
    {
        /// <summary>
        /// Datatable with user meter readings. Validate according to the file provided.
        /// Apologies - I've had no time to implement the DB, as was very busy during the weekend, so left comments,
        /// Explaining how I would tackle the issue - separate 'DataAccess' file needed for data access and db querying.
        /// </summary>
        /// <param name="readingsTable"></param>
        /// <returns></returns>
        public static string ValidateDataTableForReadings(DataTable readingsTable)
        {
            //If the datatable doesn't contain any data, return error warning user
            if(readingsTable.Rows.Count == 0 || readingsTable.Rows.Count == -1 )
            {
                return "Error - Invalid file uploaded, no rows to upload!";
            }

            //Get rid of duplicate entries in the supplied CSV file
            DataTable distinctReadingsTable = readingsTable.DefaultView.ToTable(true);

            //Loop through each row provided in the CSV file
            for(int a = 0; a < distinctReadingsTable.Rows.Count; ++a)
            {
                //Get the account ID string value, and set up meter reading variable
                string accountIDValue = distinctReadingsTable.Rows[a][0].ToString();
                int meterReadingValue; 

                //Try get the meter reading value from current row
                int.TryParse(distinctReadingsTable.Rows[a][2].ToString(), out meterReadingValue);

                // TODO: Get a datatable of existing reads stored
                // If the current exists in the DB, and the current account reading date < db reading date,
                // delete this record, and jump into next, as validation fails

                //If the account ID is 0, or an invalid number, delete the record and continue
                if (!int.TryParse(accountIDValue, out int num) || num == 0)
                {
                    distinctReadingsTable.Rows[a].Delete();
                    continue;
                }

                //If the meter reading is a negative number or 0, delete the record and continue
                else if (meterReadingValue <= 0)
                {
                    distinctReadingsTable.Rows[a].Delete();
                    continue;
                }
                //If the meter reading doesn't adhere to the NNNNN format, delete the record
                else if (meterReadingValue.ToString().Length != 5)
                {
                    distinctReadingsTable.Rows[a].Delete();
                    continue;
                }

                // TODO: Save the current row in the DB, as validation passes            
            }

            //Return the number of passed/failed processed rows.
            return $"Succesfull Readings Completed: {distinctReadingsTable.Rows.Count}. " +
                   $"Failed Number Of Readings: {readingsTable.Rows.Count - distinctReadingsTable.Rows.Count}";
        }
    }

}