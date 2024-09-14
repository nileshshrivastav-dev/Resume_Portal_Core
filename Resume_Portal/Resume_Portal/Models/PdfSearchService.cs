using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Resume_Portal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Resum_Portal.Models
{

    public class PdfSearchService
    {
        private readonly ResumePortalArivaniContext _context;

        public PdfSearchService(ResumePortalArivaniContext context)
        {
            _context = context;
        }

        public List<TblResume> SearchPdfDocuments(string keyword)
        {
            var pdfDocuments = _context.TblResumes.ToList(); // Retrieve all PDF documents

            List<TblResume> results = new List<TblResume>();

            foreach (var document in pdfDocuments)
            {
                if (SearchPdfDocumentForKeyword(document.FilePath, keyword))
                {
                    results.Add(document);
                }
            }

            return results;
        }

        private bool SearchPdfDocumentForKeyword(string filePath, string keyword)
        {
            using (PdfReader pdfReader = new PdfReader(filePath))
            {
                using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
                {
                    for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                    {
                        LocationTextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                        string text = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i), strategy);

                        if (keyword != null)
                        {
                            if (text.ToLower().Contains(keyword.ToLower()))
                            {
                                return true; // Found the keyword in the document
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            return false; // Keyword not found in the document
        }
    }


}

