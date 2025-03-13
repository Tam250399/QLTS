using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Common
{
    public class MapObject
    {
        public ObjectMap ObjectMap { get; set; }

        public List<RowMap> ListMap { get; set; }
    }
    public class ObjectMap
    {
        public string nameObj1 { get; set; }
        public string nameObj2 { get; set; }
    }
    public class RowMap
    {
        public string name1 { get; set; }
        public string name2 { get; set; }
    }
}
