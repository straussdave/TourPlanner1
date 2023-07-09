using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using TourPlanner1.Model;
using System.Collections.Generic;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Windows.Documents;

namespace TourPlanner1.Utility
{
    public class ReportGenerator
    {
        readonly static PathHelper ph = new();
        readonly DatabaseHandler db = new();
        readonly string reportsPath = ph.GetBasePath() + "\\Reports";
        readonly string imagesPath = ph.GetBasePath() + "\\Images";

        /// <summary>
        /// Generates a PDF report of a single tour
        /// </summary>
        /// <param name="tour"></param>
        public void GenerateReport(Tour tour)
        {
            string pdfName = tour.Name + ".pdf";
            string path = Path.Combine(reportsPath, pdfName);
            var writer = new PdfWriter(path);
            var pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf);

            var header = new iText.Layout.Element.Paragraph(tour.Name)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                .SetFontSize(25)
                .SetBold();

            var description = new iText.Layout.Element.Paragraph(tour.Description);
            var transportType = new iText.Layout.Element.Paragraph("Transport Type: " + CapitalizeFirstLetter(tour.TransportType));
            var fromLocation = new iText.Layout.Element.Paragraph("From: " + tour.FromLocation);
            var toLocation = new iText.Layout.Element.Paragraph("To: " + tour.ToLocation);
            var tourDistance = new iText.Layout.Element.Paragraph("Distance: " + tour.TourDistance + "km");
            var time = new iText.Layout.Element.Paragraph("Estimated Time: " + ConvertToHoursAndMinutes(tour.EstimatedTime));
            var imageFileName = tour.RouteImage;
            var imagePath = Path.Combine(imagesPath, imageFileName);
            var image = ImageDataFactory.Create(imagePath);
            var tableHeader = new iText.Layout.Element.Paragraph("Tour Logs")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold()
                .SetFontSize(20);
            var table = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(new float[] { 10, 50, 1, 20, 1 })).UseAllAvailableWidth();
            table.AddHeaderCell("Date");
            table.AddHeaderCell("Comment");
            table.AddHeaderCell("Difficulty");
            table.AddHeaderCell("Real Time");
            table.AddHeaderCell("Rating");
            table.SetFontSize(14).SetBackgroundColor(ColorConstants.WHITE);
            List<Log> logList = db.ReadLogs();
            foreach (var log in logList)
            {
                if (log.TourId == tour.Id)
                {
                    table.AddCell(log.TourDate.ToString("dd/MM/yyyy"));
                    table.AddCell(log.Comment);
                    table.AddCell(log.Difficulty.ToString());
                    table.AddCell(ConvertToHoursAndMinutes(log.TotalTime));
                    table.AddCell(log.Rating.ToString());
                }

            }

            document.Add(header);
            document.Add(description);
            document.Add(transportType);
            document.Add(fromLocation);
            document.Add(toLocation);
            document.Add(tourDistance);
            document.Add(time);
            document.Add(new iText.Layout.Element.Image(image));
            document.Add(tableHeader);
            document.Add(table);
            document.Close();
        }

        /// <summary>
        /// Generates a PDF report of all tours in the database
        /// </summary>
        public void GenerateSummaryReport()
        {
            string pdfName = "ToursSummary.pdf";
            string path = Path.Combine(reportsPath, pdfName);
            var writer = new PdfWriter(path);
            var pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf);

            var header = new iText.Layout.Element.Paragraph("Summary Report")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                .SetFontSize(25)
                .SetBold();

            document.Add(header);

            List<Tour> T = db.ReadTours();
            foreach (var t in T)
            {
                var tourHeader = new iText.Layout.Element.Paragraph(t.Name)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                .SetFontSize(20)
                .SetBold();

                var description = new iText.Layout.Element.Paragraph(t.Description);
                var transportType = new iText.Layout.Element.Paragraph("Transport Type: " + CapitalizeFirstLetter(t.TransportType));
                var fromLocation = new iText.Layout.Element.Paragraph("From: " + t.FromLocation);
                var toLocation = new iText.Layout.Element.Paragraph("To: " + t.ToLocation);
                var tourDistance = new iText.Layout.Element.Paragraph("Distance: " + t.TourDistance + "km");
                var time = new iText.Layout.Element.Paragraph("Estimated Time: " + ConvertToHoursAndMinutes(t.EstimatedTime));

                document.Add(tourHeader);
                document.Add(description);
                document.Add(transportType);
                document.Add(fromLocation);
                document.Add(toLocation);
                document.Add(tourDistance);
                document.Add(time);
            }
            document.Close();
        }

        /// <summary>
        /// Converts seconds to a string like "x Hours and y Minutes"
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns>time string</returns>
        public static string ConvertToHoursAndMinutes(int seconds)
        {
            string timeString;
            int hours = (int)Math.Floor((decimal)(seconds / 3600));
            int minutes = (int)Math.Round((decimal)seconds % 3600 / 60);
            if (hours <= 0)
            {
                timeString = minutes.ToString() + " Minute";
            }
            else if (hours == 1)
            {
                timeString = hours.ToString() + " Hour and " + minutes.ToString() + " Minute";
            }
            else
            {
                timeString = hours.ToString() + " Hours and " + minutes.ToString() + " Minute";
            }
            if (minutes != 1)
            {
                timeString += "s";
            }
            return timeString;
        }

        /// <summary>
        /// Capitalizes the first letter in a string
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string with first letter capitalized</returns>
        private static string CapitalizeFirstLetter(string text)
        {
            return char.ToUpper(text[0]) + text[1..];
        }
    }
}
