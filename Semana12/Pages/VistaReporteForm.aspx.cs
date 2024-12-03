using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationReports.Clases;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;


namespace Semana12.Pages
{
    public partial class VistaReporteForm : System.Web.UI.Page
    {
        // Clase para representar los datos del JSON
        public Persona persona = new Persona();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Simulación de datos JSON
                var data = new List<Persona>
                {
                    new Persona { ID = 1, Nombre = "Karla Brenes", Edad = 80 },
                    new Persona { ID = 2, Nombre = "Joshua Andrey", Edad = 30 },
                    new Persona { ID = 3, Nombre = "Johel Perez", Edad = 50 }
                };

                gvData.DataSource = data;
                gvData.DataBind();
            }
        }

        protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            // Simulación de datos JSON
            var data = new List<Persona>
                {
                    new Persona { ID = 1, Nombre = "Karla Brenes", Edad = 80 },
                    new Persona { ID = 2, Nombre = "Joshua Andrey", Edad = 30 },
                    new Persona { ID = 3, Nombre = "Johel Perez", Edad = 50 }
                };

            //lambda
            //var listaOrdenada = data.OrderBy(a => a.ID).ToList();
            var listaOrdenada = data.OrderBy(orden => orden.Edad).ToList();

            //Ruta temporal donde yo voy a guardar el PDF 
            string filePath = Server.MapPath("~/Reportes/Reporte.pdf");

            //Crear el archivo pdf 
            using (var escribir = new PdfWriter(filePath))
            {
                using (var pdf = new PdfDocument(escribir))
                {
                    var documento = new Document(pdf);
                    documento.Add(new Paragraph("Reporte de personas"));

                    foreach (var persona in listaOrdenada)
                    {
                        documento.Add(new Paragraph("Identificación: " + persona.ID));
                        documento.Add(new Paragraph("Nombre: " + persona.Nombre));
                        documento.Add(new Paragraph("Edad: " + persona.Edad));
                        documento.Add(new Paragraph("----------"));
                    }
                }
            }

            // Descargar el archivo PDF
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Reporte.pdf");
            Response.TransmitFile(filePath);
            Response.End();
        }
    }
    
}