using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFH.Models
{
    public class File
    {
        public int FileId { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public string Type { get; set; }
    }
}
