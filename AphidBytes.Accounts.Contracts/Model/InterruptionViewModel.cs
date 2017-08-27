using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class InterruptionViewModel
    {
    }

    public class AudioFileModel
    {
        //public Guid UserId { get; set; }
        public string AudioFile { get; set; }
        public string AudioFileName { get; set; }
        public bool? IsActive { get; set; } 
 
    }

    public class VideoFileModel
    {
        //public Guid UserId { get; set; }
        public byte[] VideoFile { get; set; }
        public string VideoFileName { get; set; }
        public bool IsActive { get; set; }
    }

    public class ImageFileModel
    {
        //public Guid UserId { get; set; }
        public string ImageFile { get; set; }
        public string ImageFileName { get; set; }
        public bool? IsActive { get; set; }
    }


    public class ImageAudioBytesModel
    {
        public static byte[] Image1 { get; set; }
        public static byte[] Image2 { get; set; }
        public static byte[] Image3 { get; set; }
        public string Image1Name { get; set; }
        public string Image2Name { get; set; }
        public string Image3Name { get; set; }
    }

    public class AudioByteModel
    {
        public static byte[] Audio1 { get; set; }
        public static byte[] Audio2 { get; set; }
        public static byte[] Audio3 { get; set; }
        public string Audio1Name { get; set; }
        public string Audio2Name { get; set; }
        public string Audio3Name { get; set; }
    }

}
