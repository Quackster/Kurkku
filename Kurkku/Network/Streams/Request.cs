using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Network.Streams
{
    public class Request
    {
        private string _header;
        private string _data;

        /// <summary>
        /// Get the message header
        /// </summary>
        public string Header
        {
            get { return _header; }
        }
        
        /// <summary>
        /// Get the message body
        /// </summary>
        public string Body
        {
            get { return _data; }
        }

        /// <summary>
        /// Constructor for request class.
        /// </summary>
        /// <param name="messageHeader">the header requested</param>
        /// <param name="messageData">the data sent from client</param>
        public Request(string messageHeader, string messageData)
        {
            _header = messageHeader;
            _data = messageData;
        }

        /// <summary>
        /// Count the number of arguments by a given delimeter.
        /// </summary>
        /// <param name="delimeter">the delimeter</param>
        /// <returns>the number of counted arguments</returns>
        public int CountArguments(char delimeter = ' ')
        {
            return _data.Split(delimeter).Length;
        }

        /// <summary>
        /// Get an argument by a specified delimeter and index
        /// </summary>
        /// <param name="index">the index of the argument</param>
        /// <param name="delimeter">the delimeter to separate the arguments</param>
        /// <returns>the argument</returns>
        public string GetArgument(int index, char delimeter = ' ')
        {
            var delimetered = _data.Split(delimeter);

            if (delimetered.Length > index)
                return delimetered[index];

            return null;
        }
    }
}
