using MigraDoc.DocumentObjectModel;
using System.Collections.Generic;
using TourFirmBusinessLogic.HelperModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    class TouristSaveToPdf
    {
        public static void CreateDoc(TouristPdfInfo info)
        {
            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            paragraph = section.AddParagraph($"с {info.DateStart.ToShortDateString()} по { info.DateEnd.ToShortDateString()}");
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "Normal";
            var table = document.LastSection.AddTable();
            List<string> columns = new List<string> { "4cm", "3cm", "5cm", "5cm" };
            foreach (var elem in columns)
            {
                table.AddColumn(elem);
            }
            CreateRow(new PdfRowParameters
            {
                Table = table,
                Texts = new List<string> { "Путешествие", "Дата начала", "Дата окончания", "Экскурсии", "Гиды" },
                Style = "NormalTitle",
                ParagraphAlignment = ParagraphAlignment.Center
            });

          
    }
}
