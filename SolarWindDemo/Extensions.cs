using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SolarWindDemo
{
    public static class Extensions
    {
        public static IEnumerable<XElement> ElementsAnyNS(this XElement source, string localName)
        {
            return source.Elements().Where(e => e.Name.LocalName == localName);
        }

        public static XElement ElementAnyNS(this XElement source, string localName)
        {
            return source.Elements().FirstOrDefault(e => e.Name.LocalName == localName);
        }

    }
}
