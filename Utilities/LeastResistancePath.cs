using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FFLeastResistancePath.Utilities
{
    /// <summary>
    /// Contains methods to find path of least resistance
    /// </summary>
    /// <remarks>
    /// Add more details here.
    /// </remarks>
    public class LeastResistancePath
    {
        /// <summary>
        /// Returns least resistance path.
        /// </summary>
        /// <param name="inputFilePath">Input File Path</param>
        /// <returns>PathResult</returns>
        public PathResult Find(string inputFilePath)
        {
            PathResult result = new PathResult();
            try
            {
                ReturnResult getDataTableResult = new ReturnResult();
                DataTable dataGrid = new DataTable();
                getDataTableResult = Common.GetDataTableFromInputFile(inputFilePath);
                dataGrid = (DataTable)getDataTableResult.ReturnValue;
                if (!getDataTableResult.HasErrors)
                {
                    if (dataGrid.Rows.Count == 0)
                    {
                        result.Success = false;
                        result.Resistance = 0;
                        result.LeastPath = "";
                    }
                    // Loop through each row and all possible paths to get the lease path
                    for (int iCount = 1; iCount <= dataGrid.Rows.Count; iCount++)
                    {
                        PathResult rowLeast = FindRecursion(dataGrid, 0, "", iCount, 1);
                        if (result.Resistance == int.MinValue || rowLeast.Resistance < result.Resistance)
                        {
                            result = rowLeast;
                        }
                    }
                } else
                {
                    // No need to write error to log file as we had already done it inside Common.GetDataTableFromInputFile function
                    result.HasErrors = true;
                    result.ErrorMessage = getDataTableResult.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                Common.WriteLog(ex.Message);
                result.HasErrors = true;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// Find and calculate resistance through all possible paths.
        /// </summary>
        /// <param name="dataGrid">DataTable contains grid data</param>
        /// <param name="oldResistance">Old resistance value</param>
        /// <param name="oldPath">Old least Path</param>
        /// <param name="row">Current row position</param>
        /// <param name="column">Current column position</param>
        /// <returns>PathResult</returns>
        private PathResult FindRecursion(DataTable dataGrid, int? oldResistance, string oldPath, int row, int column)
        {
            PathResult result = new PathResult();
            try {
                if (dataGrid != null && dataGrid.Rows.Count >= row && dataGrid.Columns.Count >= column)
                {
                    // Append current value to the previous path.
                    int? resistance = oldResistance + GetResistence(dataGrid, row, column);
                    string path = String.Format("{0} {1}", oldPath, row).Trim();

                    // Stop recurssion if resistance > 50
                    if (resistance > 50)
                    {
                        result.Success = false;
                        result.Resistance = oldResistance;
                        result.LeastPath = oldPath;
                        return result;
                    }
                    else if (column == dataGrid.Columns.Count) // Stop continuing recursion as we had already reached to the last column
                    {
                        result.Success = true;
                        result.Resistance = resistance;
                        result.LeastPath = path;
                        return result;
                    }

                    // Loop through all possible paths..
                    Coordinates upPair = GetUpCoordinates(dataGrid, row, column);
                    Coordinates straightPair = GetStraightCoordinates(dataGrid, row, column);
                    Coordinates downPair = GetDownCoordinates(dataGrid, row, column);
                    PathResult up = FindRecursion(dataGrid, resistance, path, upPair.row, upPair.column);
                    PathResult straight = FindRecursion(dataGrid, resistance, path, straightPair.row, straightPair.column);
                    PathResult down = FindRecursion(dataGrid, resistance, path, downPair.row, downPair.column);

                    // Validate and return least path.
                    if (up.Resistance < straight.Resistance && up.Resistance < down.Resistance)
                    {
                        return up;
                    }
                    else if (straight.Resistance < down.Resistance)
                    {
                        return straight;
                    }
                    else
                    {
                        return down;
                    }
                } else
                {
                    string message = "File does not exist or not accessible.";
                    Common.WriteLog(message);
                    result.HasErrors = true;
                    result.ErrorMessage = message;
                }
            }catch(Exception ex)
            {
                Common.WriteLog(ex.Message);
                result.HasErrors = true;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// Get resistance value from dataGrid based on row and column index.
        /// </summary>
        /// <param name="dataGrid">DataTable contains grid data</param>
        /// <param name="row">Current row position</param>
        /// <param name="column">Current column position</param>
        /// <returns>PathResult</returns>
        private int? GetResistence(DataTable dataGrid, int row, int column)
        {
            if (row <= 0 || row > dataGrid.Rows.Count || column <= 0 || column > dataGrid.Columns.Count)
            {
                return null;
            }
            return Common.ToInt32(dataGrid.Rows[row - 1][column - 1]);
        }
        /// <summary>
        /// Get straight row and column coordinates for current position.
        /// </summary>
        /// <param name="dataGrid">DataTable contains grid data</param>
        /// <param name="row">Current row position</param>
        /// <param name="column">Current column position</param>
        /// <returns>PathResult</returns>
        private Coordinates GetStraightCoordinates(DataTable dataGrid, int row, int column)
        {
            if (column >= dataGrid.Columns.Count)
            {
                return null;
            }
            return new Coordinates(row, column + 1);
        }
        /// <summary>
        /// Get up row and column coordinates for current position.
        /// </summary>
        /// <param name="dataGrid">DataTable contains grid data</param>
        /// <param name="row">Current row position</param>
        /// <param name="column">Current column position</param>
        /// <returns>PathResult</returns>
        private Coordinates GetUpCoordinates(DataTable dataGrid, int row, int column)
        {
            if (column >= dataGrid.Columns.Count)
            {
                return null;
            }
            return new Coordinates(row == 1 ? dataGrid.Rows.Count : row - 1, column + 1);
        }
        /// <summary>
        /// Get down row and column coordinates for current position.
        /// </summary>
        /// <param name="dataGrid">DataTable contains grid data</param>
        /// <param name="row">Current row position</param>
        /// <param name="column">Current column position</param>
        /// <returns>PathResult</returns>
        private Coordinates GetDownCoordinates(DataTable dataGrid, int row, int column)
        {
            if (column >= dataGrid.Columns.Count)
            {
                return null;
            }
            return new Coordinates(row == dataGrid.Rows.Count ? 1 : row + 1, column + 1);
        }
        /// <summary>
        /// Contains properties to store row and column coordinates.
        /// </summary>
        private class Coordinates
        {
            public int row;
            public int column;
            public Coordinates(int _row, int _column)
            {
                this.row = _row;
                this.column = _column;
            }
        }
    }

}