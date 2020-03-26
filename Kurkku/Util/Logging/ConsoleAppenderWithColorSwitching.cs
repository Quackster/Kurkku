using log4net.Appender;
using log4net.Core;
using System;
using System.Text.RegularExpressions;

namespace Kurkku.Util.Logging
{
    public class ConsoleAppenderWithColorSwitching : ConsoleAppender
    {
        private string ReplaceLogLevel(string renderedLayout)
        {
            renderedLayout = renderedLayout.Replace(" INFO ", " |Cyan|INFO|Gray| ");
            renderedLayout = renderedLayout.Replace(" DEBUG ", " |DarkYellow|DEBUG|Gray|  ");
            renderedLayout = renderedLayout.Replace(" WARN ", " |Yellow|WARN|Gray|  ");
            renderedLayout = renderedLayout.Replace(" ERROR ", " |DarkYellow|ERROR|Gray|  ");
            renderedLayout = renderedLayout.Replace(" FATAL ", " |DarkRed|FATAL|Gray|  ");
            return renderedLayout;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            var regex = new Regex(@"(\|\w+\|)");
            var renderedLayout = base.RenderLoggingEvent(loggingEvent);
            renderedLayout = ReplaceLogLevel(renderedLayout);

            var chunks = regex.Split(renderedLayout);

            foreach (var chunk in chunks)
            {
                if (chunk.StartsWith("|") && chunk.EndsWith("|"))
                {
                    var consoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), chunk.Substring(1, chunk.Length - 2));
                    Console.ForegroundColor = consoleColor;
                }
                else
                {
                    Console.Write(chunk);
                }
            }
        }
    }
}
