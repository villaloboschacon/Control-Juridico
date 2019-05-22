using BackEnd.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SistemaControl.Models
{
    public class ReporteModel
    {

        #region Declaration

        int _totalColumn;
        Document _document;
        Font _fontStyle;

        string _fecha;

        PdfPTable _pdfTable;
        PdfPCell _pdfCell;
        MemoryStream _memoryStream = new MemoryStream();

        // lista de los resportes
        List<Documento> _referencias = new List<Documento>(); // lista de los documentos 
        List<Caso> _casos = new List<Caso>(); // lista de los procesos administrativos 
        List<Caso> _casosJudicial = new List<Caso>(); //lista de los procesos judiciales 


        #endregion


        //  Reporte de documentos referencias 
        public byte[] PrepareReport(List<Documento> referencias, string fecha)
        {
            _pdfTable = new PdfPTable(6);
            _totalColumn = 6;

            _referencias = referencias;
            _fecha = fecha;

            #region

            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);


            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_BOTTOM;

            _fontStyle = FontFactory.GetFont("HELVETICA", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();


            _pdfTable.SetWidths(new float[] { 30f, 30f, 15f, 30f, 30f, 15f });

            #endregion

            this.ReportHeader();
            this.ReportBody();
            _pdfTable.HeaderRows = 2;


            _document.Add(_pdfTable);
            _document.Close();
            return _memoryStream.ToArray();

        }

        private void ReportHeader()
        {
            string direccion = HttpContext.Current.Server.MapPath("~/Content/img/");
            Image tif = Image.GetInstance(direccion + "muni.gif");
            tif.ScalePercent(24f);
            _pdfCell = new PdfPCell(tif);
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.Rowspan = 1;
            _pdfCell.Border = 0;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 20f, 1);
            _pdfCell = new PdfPCell(new Phrase("Municipalidad de Alajuela", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();


            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 20f, 1);
            _pdfCell = new PdfPCell(new Phrase("Procesos de Servicos Juridicos", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 20f, 1);
            _pdfCell = new PdfPCell(new Phrase("Reporte de Documentos", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();


            //_fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 15f, 1);
            //_pdfCell = new PdfPCell(new Phrase("Fuente: Sistema Control Juridico", _fontStyle));
            //_pdfCell.Colspan = 6;
            //_pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            //_pdfCell.Border = 0;
            //_pdfCell.BackgroundColor = BaseColor.WHITE;
            //_pdfCell.Padding = 20f;
            //_pdfCell.ExtraParagraphSpace = 0;
            // _pdfTable.AddCell(_pdfCell);
            //_pdfTable.CompleteRow();


            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 15f);
            _pdfCell = new PdfPCell();
            Phrase frase1 = new Phrase("Fuente: Sistema de Control Juridico", _fontStyle);
            Phrase frase2 = new Phrase("Fecha: " + _fecha, _fontStyle);
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.Border = 0;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.BorderWidthBottom = 0f;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfCell.AddElement(frase1);
            _pdfCell.AddElement(frase2);
            _pdfCell.Padding = 20f;
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();

        }

        private void ReportBody()
        {

            #region

            // imagen 

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("Número Oficio", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Número Ingreso", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Fecha", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Tipo Origen", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Origen", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Estado", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();

            #endregion

            #region Table Body

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 10f, 0);
            _fontStyle.SetColor(0, 0, 0); // color de tabla contenido



            foreach (Documento documento in _referencias)
            {

                _pdfCell = new PdfPCell(new Phrase(documento.numeroDocumento.ToString(), _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(documento.numeroIngreso, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(documento.fecha.ToString("dd/MM/yyyy"), _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(documento.TablaGeneral3.descripcion, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(documento.TablaGeneral1.descripcion, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(documento.TablaGeneral.descripcion, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfTable.CompleteRow();

            }

            #endregion

        }


        // Reporte de los procesos administrativos 
        public byte[] PrepareReportCaso(List<Caso> casos, string fecha)
        {
            _casos = casos;
            _pdfTable = new PdfPTable(7);
            _totalColumn = 7;
            _fecha = fecha;


            #region

            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);


            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_BOTTOM;

            _fontStyle = FontFactory.GetFont("HELVETICA", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();


            _pdfTable.SetWidths(new float[] { 25f, 25f, 22f, 32f, 26f, 20f, 15f });

            #endregion

            this.ReportHeaderCaso();
            this.ReportBodyCasos();
            _pdfTable.HeaderRows = 2;


            _document.Add(_pdfTable);
            _document.Close();
            return _memoryStream.ToArray();

        }

        private void ReportHeaderCaso()
        {
            string direccion = HttpContext.Current.Server.MapPath("~/Content/img/");
            Image tif = Image.GetInstance(direccion + "muni.gif");
            tif.ScalePercent(24f);
            _pdfCell = new PdfPCell(tif);
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.Rowspan = 1;
            _pdfCell.Border = 0;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 20f, 1);
            _pdfCell = new PdfPCell(new Phrase("Municipalidad de Alajuela", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();


            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 20f, 1);
            _pdfCell = new PdfPCell(new Phrase("Procesos de Servicos Juridicos", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 20f, 1);
            _pdfCell = new PdfPCell(new Phrase("Reporte de Procesos Administrativos", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();


            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 15f);
            _pdfCell = new PdfPCell();
            Phrase frase1 = new Phrase("Fuente: Sistema de Control Juridico", _fontStyle);
            Phrase frase2 = new Phrase("Fecha: " + _fecha, _fontStyle);
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.Border = 0;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.BorderWidthBottom = 0f;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfCell.AddElement(frase1);
            _pdfCell.AddElement(frase2);
            _pdfCell.Padding = 20f;
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();

        }

        private void ReportBodyCasos()
        {

            #region

            // imagen 
            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("Número Proceso", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Abogado", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Materia", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Persona", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Tipo de Proceso", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Tipo de Litigio", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Estado", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);


            _pdfTable.CompleteRow();

            #endregion

            #region Table Body

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 10f, 0);
            _fontStyle.SetColor(0, 0, 0); // color de tabla contenido

            foreach (Caso caso in _casos)
            {

                _pdfCell = new PdfPCell(new Phrase(caso.numeroCaso, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(caso.Usuario.nombre, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(caso.materia, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(caso.Persona.nombreCompleto, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(caso.TablaGeneral1.descripcion, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(caso.TablaGeneral2.descripcion, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(caso.TablaGeneral.descripcion, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfTable.CompleteRow();

            }

            #endregion

        }


        //Reporte de los procesos  judiciales 
        public byte[] PrepareReportCasoJudicial(List<Caso> casosJudicial, string fecha)
        {
            _casosJudicial = casosJudicial;
            _pdfTable = new PdfPTable(7);
            _totalColumn = 7;
            _fecha = fecha;


            #region

            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);


            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_BOTTOM;

            _fontStyle = FontFactory.GetFont("HELVETICA", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();


            _pdfTable.SetWidths(new float[] { 25f, 25f, 22f, 32f, 26f, 20f, 15f });

            #endregion

            this.ReportHeaderCasoJudicial();
            this.ReportBodyCasosJudicial();
            _pdfTable.HeaderRows = 2;


            _document.Add(_pdfTable);
            _document.Close();
            return _memoryStream.ToArray();

        }

        private void ReportHeaderCasoJudicial()
        {
            string direccion = HttpContext.Current.Server.MapPath("~/Content/img/");
            Image tif = Image.GetInstance(direccion + "muni.gif");
            tif.ScalePercent(24f);
            _pdfCell = new PdfPCell(tif);
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.Rowspan = 1;
            _pdfCell.Border = 0;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 20f, 1);
            _pdfCell = new PdfPCell(new Phrase("Municipalidad de Alajuela", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();


            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 20f, 1);
            _pdfCell = new PdfPCell(new Phrase("Procesos de Servicos Juridicos", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 20f, 1);
            _pdfCell = new PdfPCell(new Phrase("Reporte de Procesos Judiciales", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();


            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 15f);
            _pdfCell = new PdfPCell();
            Phrase frase1 = new Phrase("Fuente: Sistema de Control Juridico", _fontStyle);
            Phrase frase2 = new Phrase("Fecha: " + _fecha, _fontStyle);
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.Border = 0;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.BorderWidthBottom = 0f;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfCell.AddElement(frase1);
            _pdfCell.AddElement(frase2);
            _pdfCell.Padding = 20f;
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();

        }

        private void ReportBodyCasosJudicial()
        {

            #region

            // imagen 


            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("Número Proceso", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Abogado", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Materia", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Persona", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Tipo de Proceso", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Tipo de Litigio", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 8f, Font.BOLDITALIC);
            _pdfCell = new PdfPCell(new Phrase("Estado", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);


            _pdfTable.CompleteRow();

            #endregion

            #region Table Body

            _fontStyle = FontFactory.GetFont(@"C:\Windows\Fonts\times.ttf", 10f, 0);
            _fontStyle.SetColor(0, 0, 0); // color de tabla contenido

            foreach (Caso caso in _casosJudicial)
            {

                _pdfCell = new PdfPCell(new Phrase(caso.numeroCaso, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(caso.Usuario.nombre, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(caso.materia, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(caso.Persona.nombreCompleto, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(caso.TablaGeneral1.descripcion, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(caso.TablaGeneral2.descripcion, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(caso.TablaGeneral.descripcion, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfTable.CompleteRow();

            }

            #endregion

        }


    }
}
