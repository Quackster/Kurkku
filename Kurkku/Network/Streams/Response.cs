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
        /// Get the message header
        /// </summary>
        public short Header
        {
            get { return m_Header; }
        }

        /// <summary>
        /// Get whether the length has been set
        /// </summary>
        public bool HasLength
        {
            get { return m_Buffer.GetInt(0) > -1; }
        }

        /// <summary>
        /// Get the message body with characters replaced
        /// </summary>
        public string MessageBody
        {
            get
            {
                string consoleText = m_Buffer.ToString(StringUtil.GetEncoding());

                for (int i = 0; i < 13; i++)
                    consoleText = consoleText.Replace(Convert.ToString((char)i), "[" + i + "]");

                return consoleText;
            }
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
            this.m_Buffer.WriteInt(0);
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
        /// Write short for clients
        /// </summary>
        /// <param name="obj">short value</param>
        public void writeShort(short obj)
        {
            m_Buffer.WriteShort(obj);
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
