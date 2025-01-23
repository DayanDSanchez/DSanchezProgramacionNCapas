using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ruta del archivo XML original
            string archivoRuta = @"C:\Users\digis\Downloads\10.- DatosXML.xml";

            // Deserializar el XML a un objeto de la clase Noticias
            XmlSerializer serializer = new XmlSerializer(typeof(Noticias));
            Noticias noticias;

            using (FileStream fs = new FileStream(archivoRuta, FileMode.Open))
            {
                noticias = (Noticias)serializer.Deserialize(fs);
            }

            // Transformar la información al nuevo formato
            Info info = new Info
            {
                ListaPodcasts = noticias.ListaNoticias.Select(n => new Podcast
                {
                    Tipo = n.Tipo,
                    Libre = n.Libre,
                    Id = n.Id,
                    Is3idfp = n.Is3idfp,
                    Idaudio = n.Idaudio,
                    Categoria = n.Categoria,
                    Titulo = n.Titulo,
                    Resumen = n.Resumen,
                    Prevurl = n.Prevurl,
                    Url = n.Url
                }).ToList()
            };

            // Serializar el nuevo objeto Info al XML
            XmlSerializer serializerNuevo = new XmlSerializer(typeof(Info));
            string nuevaRutaArchivo = @"C:\Users\digis\Downloads\DatosXML.xml";

            using (FileStream fs = new FileStream(nuevaRutaArchivo, FileMode.Create))
            {
                serializerNuevo.Serialize(fs, info);
            }

            Console.WriteLine("Archivo procesado y guardado en: " + nuevaRutaArchivo);
        }
    }
}
