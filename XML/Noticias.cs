using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XML
{
    [XmlRoot("noticias")]
    public class Noticias
    {
        [XmlElement("noticia")]
        public List<Noticia> ListaNoticias { get; set; }
    }

    public class Noticia
    {
        [XmlAttribute("tipo")]
        public string Tipo { get; set; }

        [XmlAttribute("libre")]
        public string Libre { get; set; }

        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("categoria")]
        public string Categoria { get; set; }

        [XmlElement("titulo")]
        public string Titulo { get; set; }

        [XmlElement("resumen")]
        public string Resumen { get; set; }

        [XmlElement("prevurl")]
        public string Prevurl { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("is3idfp")]
        public string Is3idfp { get; set; }

        [XmlElement("idaudio")]
        public string Idaudio { get; set; }
    }
}
