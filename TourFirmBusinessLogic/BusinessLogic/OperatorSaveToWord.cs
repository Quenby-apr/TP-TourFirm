using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using TourFirmBusinessLogic.HelperModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    static class OperatorSaveToWord
    {
        public static void CreateDoc(OperatorWordInfo info)
        {
            using (WordprocessingDocument wordDocument =
           WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationValues = (DocumentFormat.OpenXml.Math.JustificationValues)JustificationValues.Center
                    }
                }));

                int i = 0;
                foreach (var tour in info.TourExcursions)
                {
                    docBody.AppendChild(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<(string, WordTextProperties)> {
                        (tour.TourName + " :", new WordTextProperties {Bold = true, Size = "24", }
                            ) },
                        TextProperties = new WordTextProperties
                        {
                            Size = "24",
                            JustificationValues = (DocumentFormat.OpenXml.Math.JustificationValues)JustificationValues.Both
                        }
                    }));
                    foreach (var excursion in info.TourExcursions[i].Excursions)
                    {
                        docBody.AppendChild(CreateParagraph(new WordParagraph
                        {
                            Texts = new List<(string, WordTextProperties)> {
                        (excursion.Name + ", " + excursion.Price.ToString() + " рублей, " + excursion.Duration +" часов", new WordTextProperties {Bold = true, Size = "16", }
                            ) },
                            TextProperties = new WordTextProperties
                            {
                                Size = "16",
                                JustificationValues = (DocumentFormat.OpenXml.Math.JustificationValues)JustificationValues.Both
                            }
                        }));
                    }
                    i++;
                }
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        // Настройки страницы
        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }

        // Создание абзаца с текстом
        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();

                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                foreach (var run in paragraph.Texts)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = run.Item2.Size });
                    if (run.Item2.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text
                    {
                        Text = run.Item1,
                        Space =
                   SpaceProcessingModeValues.Preserve
                    });
                    docParagraph.AppendChild(docRun);
                }
                return docParagraph;
            }
            return null;
        }

        // Задание форматирования для абзаца
        private static ParagraphProperties CreateParagraphProperties(WordTextProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = (JustificationValues)paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new
               ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize
                    {
                        Val =
                   paragraphProperties.Size
                    });
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }
    }
}