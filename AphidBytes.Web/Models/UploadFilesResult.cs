using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileUploadMVC4.Models
{
    public class UploadFilesResult
    {
        public string Name { get; set; }
        public string Length { get; set; }
        public string Type { get; set; }
        public string Duration { get; set; }
        public int Count { get; set; }
    }

    public class AudioFileName
    {
        public string name { get; set; }
    }
}
