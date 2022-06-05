using System;
using System.Collections.Generic;
using System.Text;

namespace TopDownRacer.Models
{
    class Sound
    {
        public string Key { get; set; }
        public string Filename { get; set; }
        public float DefaultVolume { get; set; }
        public float DefaultPitch { get; set; }
    }
}
