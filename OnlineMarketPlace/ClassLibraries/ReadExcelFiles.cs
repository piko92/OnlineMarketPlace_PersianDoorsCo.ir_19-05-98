
using System.Data;
using IronXL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries
{
    public class ReadExcelFiles
    {
        /*
         * ******************************* ATTENTION **********************************
         * 
         * Using of this class needs to install below Nugets:
         * 1 - IronXL
         * Source Project: https://ironsoftware.com/csharp/excel/
         * ****************************************************************************
         */

        public static List<string[]> Read(string filePath)
        {
            //Supported spreadsheet formats for reading include: XLSX, XLS, CSV and TSV
            WorkBook workbook = WorkBook.Load(filePath);
            WorkSheet sheet = workbook.WorkSheets.First();

            List<string[]> sheetValues = new List<string[]>();

            var rows = sheet.Rows;
            rows.ForEach(x =>
            {
                sheetValues.Add(x.StringValue.Split("\t"));
            });

            return sheetValues;
        }
    }
}
