using FD.Core;

namespace FD.Logging
{
    /// <summary>
    /// The config for <see cref="Logger" />
    /// </summary>
    public sealed class LoggerConfig
    {
        /// <summary>
        /// The underlying stream will be permit to do buffered writes.
        /// </summary>
        public bool bufferedFileWrite = true;

        /// <summary>
        /// The directory to log files to.
        /// </summary>
        public string logDirectory = Game.GetGameExecutePath() + "/Logs/";

        /// <summary>
        /// The format the the files will use.
        /// </summary>
        public string logFileDateTimeFormat = "yyyy-MM-dd-HH-mm-ss";
    }
}
