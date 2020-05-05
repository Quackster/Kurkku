using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    class MoodlightExtraData
    {
        public bool Enabled { get; set; }
        public int CurrentPreset { get; set; }
        public List<MoodlightPresetData> Presets { get; set; }
    }

    public class MoodlightPresetData
    {
        public bool IsBackground;
        public string ColorCode;
        public int ColorIntensity;

        public MoodlightPresetData()
        {
            ColorCode = "#000000";
            ColorIntensity = 255;
        }
    }
}
