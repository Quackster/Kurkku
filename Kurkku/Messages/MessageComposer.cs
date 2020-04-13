using System.Collections.Generic;

namespace Kurkku.Messages
{
    public abstract class IMessageComposer
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

        public IMessageComposer()
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