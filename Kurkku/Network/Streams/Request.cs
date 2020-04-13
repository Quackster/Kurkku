using DotNetty.Buffers;
using Kurkku.Util;
using System;

namespace Kurkku.Network.Streams
{
    public class Request
    {
        #region Fields

        private short m_Header;
        private int m_Length;
        private IByteBuffer m_Buffer;

        #endregion

        /// <summary>
        /// Get the message header
        /// </summary>
        public short Header
        {
            get { return m_Header; }
        }

        /// <summary>
        /// Get the message length
        /// </summary>
        public int Length
        {
            get { return m_Length; }
        }

        /// <summary>
        /// Get the message buffer
        /// </summary>
        public IByteBuffer Buffer
        {
            get { return m_Buffer; }
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
                {
                    consoleText = consoleText.Replace(Convert.ToString((char)i), "[" + i + "]");
                }

                return consoleText;
            }
        }

        /// <summary>
        /// Get the readable bytes left
        /// </summary>
        public byte[] ReadableBytes
        {
            get
            {
                m_Buffer.MarkReaderIndex();
                byte[] bytes = new byte[m_Buffer.ReadableBytes];
                m_Buffer.ReadBytes(bytes);
                m_Buffer.ResetReaderIndex();
                return bytes;
            }
        }

        #region Constructors

        /// <summary>
        /// Constructor for request
        /// </summary>
        /// <param name="length"></param>
        /// <param name="buffer"></param>
        public Request(int length, IByteBuffer buffer)
        {
            this.m_Length = length;
            this.m_Buffer = buffer;
            this.m_Header = buffer.ReadShort();
        }

        #endregion

        /// <summary>
        /// Read integer
        /// </summary>
        /// <returns>the integer from client</returns>
        public int ReadInt()
        {
            try
            {
                return this.m_Buffer.ReadInt();
            }
            catch 
            {
                return 0;
            }
        }

        /// <summary>
        /// Read integer as boolean
        /// </summary>
        /// <returns>the boolean from client</returns>
        public bool ReadIntAsBool()
        {
            try
            {
                return this.m_Buffer.ReadInt() == 1;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Read boolean
        /// </summary>
        /// <returns>the boolean
        /// from client</returns>
        public bool ReadBoolean()
        {

            try
            {
                return m_Buffer.ReadByte() == 1;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Read string
        /// </summary>
        /// <returns>the string from client</returns>
        public string ReadString()
        {
            try
            {
                int length = m_Buffer.ReadShort();
                byte[] data = this.ReadBytes(length);

                return StringUtil.GetEncoding().GetString(data);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read bytes
        /// </summary>
        /// <returns>the bytes from client</returns>
        public byte[] ReadBytes(int len)
        {
            try
            {
                byte[] payload = new byte[len];
                m_Buffer.ReadBytes(payload);
                return payload;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Dispose handler of request
        /// </summary>
        public void Dispose()
        {
            m_Buffer.Release();
        }
    }
}
