using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FFLeastResistancePath.Utilities
{
    class Common
    {
        /// <summary>
        /// Converts object to integer using Int32.TryParse
        /// </summary>
        /// <param name="value">Object that needs to be converted to integer</param>
        /// <returns>int</returns>
        public static int ToInt32(object value)
        {
            int result;
            Int32.TryParse(Convert.ToString(value), out result);
            return result;
        }
        /// <summary>
        /// Bind the input data from file to DataTable.
        /// </summary>
        /// <param name="filePath">Input file path</param>
        /// <returns>DataTable</returns>
        public static ReturnResult GetDataTableFromInputFile(string filePath)
        {
            ReturnResult result = new ReturnResult();
            DataTable dataGrid = new DataTable();
            try
            {
                // Validate if file exists.
                if (File.Exists(filePath))
                {
                    using (TextReader tr = File.OpenText(filePath))
                    {
                        string line;
                        while ((line = tr.ReadLine()) != null)
                        {
                            string[] items = line.Trim().Split(' ');

                            // Add Columns to table if not exists
                            if (dataGrid.Columns.Count == 0)
                            {
                                for (int iCount = 0; iCount < items.Length; iCount++)
                                {
                                    dataGrid.Columns.Add();
                                    dataGrid.Columns[iCount].DataType = typeof(int);
                                }
                            }
                            dataGrid.Rows.Add(items);
                        }
                    }
                } else
                {
                    string message = "File does not exist or not accessible.";
                    WriteLog(message);
                    result.HasErrors = true;
                    result.ErrorMessage = message;
                }
            }
            catch(Exception ex)
            {
                WriteLog(ex.Message);
                result.HasErrors = true;
                result.ErrorMessage = ex.Message;
            }
            // Check if there is already some error.
            if (!result.HasErrors)
            {
                ReturnResult objDataTableValidation = new ReturnResult();
                objDataTableValidation = ValidateData(dataGrid);
                result.HasErrors = objDataTableValidation.HasErrors;
                result.ErrorMessage = objDataTableValidation.ErrorMessage;
            }
            result.ReturnValue = dataGrid;
            return result;
        }
        /// <summary>
        /// Validates input data
        /// </summary>
        /// <param name="message">Message that needs to be logged</param>
        private static ReturnResult ValidateData(DataTable dataGrid)
        {
            ReturnResult objResult = new ReturnResult();
            // Validating minimum 1 row
            if (dataGrid.Rows.Count < 1)
            {
                objResult.HasErrors = true;
                objResult.ErrorMessage = "Invalid data. Minimum 1 row required.";
            }
            else if (dataGrid.Columns.Count < 5)
            {
                objResult.HasErrors = true;
                objResult.ErrorMessage = "Invalid data. Minimum 5 columns required.";
            }
            else if (dataGrid.Rows.Count > 10)
            {
                objResult.HasErrors = true;
                objResult.ErrorMessage = "Invalid data. Maximum 10 rows allowed.";
            }
            else if (dataGrid.Columns.Count > 100)
            {
                objResult.HasErrors = true;
                objResult.ErrorMessage = "Invalid data. Maximum 100 columns allowed.";
            }
            return objResult;
        }
        /// <summary>
        /// Write log to Log.txt
        /// </summary>
        /// <param name="message">Message that needs to be logged</param>
        public static void WriteLog(string message)
        {
            string path = HttpContext.Current.Server.MapPath("~/Log.txt");
            File.AppendAllText(path, message);
        }
    }
}
