using DotNetty.Buffers;
using Kurkku.Util;
using System;

namespace Kurkku.Network.Streams
{
    public class Response
    {
        #region Fields

        private short m_Header;
        private IByteBuffer m_Buffer;

        #endregion

        #region Properties

        /// <summary>
        /// Get whether the length has been set
        /// </summary>
        public bool HasLength
        {
            get { return m_Buffer.GetInt(0) > -1; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for response
        /// </summary>
        /// <param name="header"></param>
        /// <param name="buffer"></param>
        public Response(short header, IByteBuffer buffer)
        {
            this.m_Header = header;
            this.m_Buffer = buffer;
            this.m_Buffer.WriteInt(-1);
            this.m_Buffer.WriteShort(m_Header);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Write string for client
        /// </summary>
        /// <param name="obj"></param>
        public void writeString(object obj)
        {
            m_Buffer.WriteShort(obj.ToString().Length);
            m_Buffer.WriteBytes(StringUtil.GetEncoding().GetBytes(obj.ToString()));
        }

        /// <summary>
        /// Write int for client
        /// </summary>
        /// <param name="obj"></param>
        public void writeInt(int obj)
        {
            m_Buffer.WriteInt(obj);
        }

        /// <summary>
        /// Write boolean for client
        /// </summary>
        /// <param name="obj"></param>
        public void writeBool(bool obj)
        {
            m_Buffer.WriteBoolean(obj);
        }

        #endregion
    }
}
