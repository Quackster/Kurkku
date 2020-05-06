using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;

namespace Kurkku.Messages.Outgoing
{
    class MoodlightConfigComposer : IMessageComposer
    {
        private MoodlightExtraData moodlightData;

        public MoodlightConfigComposer(MoodlightExtraData moodlightData)
        {
            this.moodlightData = moodlightData;
        }

        public override void Write()
        {
            m_Data.Add(moodlightData.Presets.Count);
            m_Data.Add(moodlightData.CurrentPreset);

            int i = 1;
            foreach (var preset in moodlightData.Presets)
            {
                m_Data.Add(i);
                m_Data.Add(preset.IsBackground ? 2 : 1);
                m_Data.Add(preset.ColorCode);
                m_Data.Add(preset.ColorIntensity);
                i++;
            }
        }
    }
}
