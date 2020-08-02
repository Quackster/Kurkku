using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    public class UserChangeMessageComposer : IMessageComposer
    {
        private int virtualId;
        private string figure;
        private string sex;
        private string motto;
        private int achievementPoints;

        public UserChangeMessageComposer(int v, string figure, string sex, string motto, int achievementPoints)
        {
            this.virtualId = v;
            this.figure = figure;
            this.sex = sex;
            this.motto = motto;
            this.achievementPoints = achievementPoints;
        }

        public override void Write()
        {
            m_Data.Add(virtualId);
            m_Data.Add(figure);
            m_Data.Add(sex);
            m_Data.Add(motto);
            m_Data.Add(achievementPoints);
        }
    }
}
