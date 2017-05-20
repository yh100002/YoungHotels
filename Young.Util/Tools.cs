using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.IO;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace Young.Util
{
    public class Tools
    {
        public static string GenerateExcel2007(string dir,string json)
        {
            try
            {
                string filename = Path.GetRandomFileName() + ".xlsx";
                string fullpath = Path.Combine(dir, filename);

                using (ExcelPackage objExcelPackage = new ExcelPackage())
                {
                    ExcelPackage pck = new ExcelPackage();
                    var ws = pck.Workbook.Worksheets.Add("Hotels");
                    using (ExcelRange objRange = ws.Cells["A1:XFD1"])
                    {
                        objRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        objRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    };
                    ws.Cells.Style.Font.SetFromFont(new Font("Calibri", 10));
                    ws.Cells.AutoFitColumns();
                    ws.Cells[1, 1, 1, 10].Style.Font.Bold = true;
                    ws.Cells[1, 1, 1, 10].Style.Font.SetFromFont(new Font("Calibri", 15));
                    ws.Cells[1, 1].Value = "HOTEL ID";
                    ws.Cells[1, 2].Value = "HOTEL NAME";
                    ws.Cells[1, 3].Value = "CLASSIFICATION";
                    ws.Cells[1, 4].Value = "SCORE";
                    ws.Cells[1, 5].Value = "RATE ID";
                    ws.Cells[1, 6].Value = "RATE NAME";
                    ws.Cells[1, 7].Value = "TARGET DAY";
                    ws.Cells[1, 8].Value = "RATE DESCRIPTION";
                    ws.Cells[1, 9].Value = "CURRENCY";
                    ws.Cells[1, 10].Value = "PRICE";

                    dynamic hotels = JsonConvert.DeserializeObject<dynamic>(json);
                    int row = 2;
                    int col = 1;
                    foreach (var item in hotels.Hotels)
                    {
                        foreach (var rate in item.hotelRates)
                        {
                            int hotelid = item.hotel.hotelID;
                            ws.Cells[row, col++].Value = hotelid;
                            string hotelname = item.hotel.name;
                            ws.Cells[row, col++].Value = hotelname;
                            int classification = item.hotel.classification;
                            ws.Cells[row, col++].Value = classification;
                            double reviewscore = item.hotel.reviewscore;
                            ws.Cells[row, col++].Value = reviewscore;
                            string rateID = rate.rateID;
                            ws.Cells[row, col++].Value = rateID;
                            string rateName = rate.rateName;
                            ws.Cells[row, col++].Value = rateName;
                            string targetDay = rate.targetDay;
                            ws.Cells[row, col++].Value = targetDay;
                            string rateDescription = rate.rateDescription;
                            ws.Cells[row, col++].Value = rateDescription;
                            string currency = rate.price.currency;
                            ws.Cells[row, col++].Value = currency;
                            int price = rate.price.numericInteger;
                            ws.Cells[row, col++].Value = price;

                            col = 1;
                            row++;
                        }
                    }
                    if (File.Exists(fullpath))
                        File.Delete(fullpath);

                    pck.SaveAs(new FileInfo(fullpath));

                    return fullpath;
                }
            }
            catch(Exception)
            {
                return "";
            }
            
        }

        public static bool IsBetween(string target, string start, string end)
        {
            try
            {
                DateTime targetDT = DateTime.ParseExact(target, "yyyy-MM-ddTHH:mm:ss.fffzzz", CultureInfo.InvariantCulture);

                DateTime startDT = DateTime.Parse(start);

                DateTime endDT = DateTime.Parse(end);

                if ((targetDT.CompareTo(startDT) >= 0) && (targetDT.CompareTo(endDT) <= 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsBetween(string target, string arrival)
        {
            try
            {
                DateTime targetDT = DateTime.ParseExact(target, "yyyy-MM-ddTHH:mm:ss.fffzzz", CultureInfo.InvariantCulture);

                DateTime arrivalDT = DateTime.Parse(arrival);

                if ((targetDT.CompareTo(arrivalDT) >= 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
