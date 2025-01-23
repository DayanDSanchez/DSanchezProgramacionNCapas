using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XML
{
    [XmlRoot("info")]
    public class Info
    {
        [XmlElement("podcast")]
        public List<Podcast> ListaPodcasts { get; set; }
    }

    public class Podcast
    {
        [XmlAttribute("tipo")]
        public string Tipo { get; set; }

        [XmlAttribute("libre")]
        public string Libre { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("is3idfp")]
        public string Is3idfp { get; set; }

        [XmlAttribute("idaudio")]
        public string Idaudio { get; set; }

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
    }
}
