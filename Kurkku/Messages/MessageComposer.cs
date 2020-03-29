using Kurkku.Game.Player;
using Kurkku.Network.Streams;
using System.Collections.Generic;

namespace Kurkku.Messages
{
    public abstract class MessageComposer
    {
        protected List<object> m_Data;
        protected bool m_Composed;

        /// <summary>
        /// Get the data appended
        /// </summary>
        public List<object> Data
        {
            get { return m_Data; }
        }

        /// <summary>
        /// Get whether the packet is composed
        /// </summary>
        public bool Composed
        {
            get { return m_Composed; }
            set { m_Composed = value; }
        }

        /// <summary>
        /// Get the header for the message
        /// </summary>
        public virtual short Header
        {
            get; set;
        }

        public MessageComposer()
        {
            m_Data = new List<object>();
        }

        public abstract void Write();

        public object[] GetMessageArray()
        {
            return m_Data.ToArray();
        }
    }
}