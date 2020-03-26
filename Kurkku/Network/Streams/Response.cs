using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Network.Streams
{
    public class Response
    {
        private string _header;
        private bool _finalised;

        private StringBuilder _buffer;

        /// <summary>
        /// Get the message header
        /// </summary>
        public string Header
        {
            get { return _header; }
        }

        /// <summary>
        /// Get the message buffer
        /// </summary>
        public StringBuilder Buffer
        {
            get { return _buffer; }
        }

        /// <summary>
        /// Get the message body
        /// </summary>
        public string Body
        {
            get
            {
                string consoleBody = _buffer.ToString();

                for (int i = 0; i < 14; i++)
                    consoleBody = consoleBody.Replace("" + (char)i, "{" + i + "}");

                return consoleBody;
            }
        }

        /// <summary>
        /// Constructor for request class.
        /// </summary>
        /// <param name="messageHeader">the header requested</param>
        /// <param name="messageData">the data sent from client</param>
        public Response(string header)
        {
            _header = header;
            _buffer = new StringBuilder();
        }

        /// <summary>
        /// Static method for creating response.
        /// </summary>
        /// <param name="header">the header for the response</param>
        /// <returns></returns>
        public static Response Init(string header)
        {
            return new Response(header);
        }

        /// <summary>
        /// Append a raw object to the response
        /// </summary>
        /// <param name="data">the data to send</param>
        public void Append(object data)
        {
            string value = data.ToString();
            value = value.Replace("#", "*");

            _buffer.Append(value);
        }

        /// <summary>
        /// Append an argument with the default delimeter.
        /// </summary>
        /// <param name="arg">the argument to append</param>
        public void AppendArgument(object arg) =>
            AppendArgument(arg, ' ');

        /// <summary>
        /// Append an argument with the breakline delimeter.
        /// </summary>
        /// <param name="arg">the argument to append</param>
        public void AppendNewArgument(object arg) =>
            AppendArgument(arg, (char)13);

        /// <summary>
        /// Append an argument with the slash delimeter.
        /// </summary>
        /// <param name="arg">the argument to append</param>
        public void AppendPartArgument(object arg) =>
            AppendArgument(arg, '/');

        /// <summary>
        /// Append an argument with the tab delimeter.
        /// </summary>
        /// <param name="arg">the argument to append</param>
        public void AppendTabArgument(object arg) =>
            AppendArgument(arg, (char)9);

        /// <summary>
        /// Append a key value argument with the equals sign.
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value">the value</param>
        public void AppendKVArgument(object key, object value)
        {
            this.Append((char)13);
            this.Append(key);
            this.Append('=');
            this.Append(value);
        }

        /// <summary>
        /// Append a key value argument with the colon sign.
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value">the value</param>
        public void AppendKV2Argument(string key, string value)
        {
            this.Append((char)13);
            this.Append(key);
            this.Append(':');
            this.Append(value);
        }

        /// <summary>
        /// Append argument by custom delimeter
        /// </summary>
        /// <param name="arg">the argument</param>
        /// <param name="delimiter">the delimeter</param>
        public void AppendArgument(object arg, char delimiter)
        {
            this.Append(delimiter);
            this.Append(arg);
        }

        /// <summary>
        /// Get the finalised message
        /// </summary>
        /// <returns>the created message</returns>
        public string GetMessage()
        {
            if (!_finalised)
            {
                _finalised = true;
            }

            return string.Format("#{0}{1}##", _header, _buffer.ToString());
        }
    }
}
