using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    class MannequinExtraData
    {
        public string Gender { get; set; }
        public string Figure { get; set; }
        public string OutfitName { get; set; }

        public MannequinExtraData()
        {
            Gender = string.Empty;
            Figure = string.Empty;
            OutfitName = string.Empty;
        }
    }
}
