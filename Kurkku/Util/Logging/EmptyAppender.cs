using log4net.Appender;
using log4net.Core;
using System;
using System.Text.RegularExpressions;

namespace Kurkku.Util.Logging
{
    public class EmptyAppender : ConsoleAppender
    {

        protected override void Append(LoggingEvent loggingEvent)
        {
            // Do nothing
        }
    }
}
