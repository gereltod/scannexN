using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Scannex
{
    public static class FileLogger
    {
        private static String _LineSeparator = "\r\n";

        private static String _BaseLogFilePath;
        private static String _BaseLogFileName;
        private static String _CurrentLogFileDay = String.Empty;
        private static String _CurrentLogFileName;

        private static FileStream _CurrentLogFileStream;
        private static Object _FileWriteLock = new Object();

        public static String _ExecutionFilePath;

        public static void InitLog(String logFilePath, String logFileName)
        {
            _BaseLogFilePath = logFilePath;
            _BaseLogFileName = logFileName;
        }

        public static void LogExceptionInFile(Exception exc)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("A New Exception was generated on '" + System.DateTime.Now + "'");
            str.AppendLine("Exception Message : '" + exc.Message);
            if (exc.InnerException != null && exc.InnerException.StackTrace != String.Empty)
                str.AppendLine("Stack Trace : '" + exc.InnerException.StackTrace);
            LogStringInFile(str.ToString());
        }

        public static void LogStringInFile(String message, int logLevel)
        {
            if ((int)logLevel >= (int)0)
            {
                lock (_FileWriteLock)
                {
                    // Locking the Log File Stream, to prevent parallel threads accesses
                    if (_CurrentLogFileStream == null || !_CurrentLogFileStream.CanWrite
                        || !_CurrentLogFileDay.Equals(System.DateTime.Now.ToShortDateString()))
                        OpenLogFileStream();

                    try
                    {
                        //Every line has its created date.
                        byte[] bytes = new UTF8Encoding(true).GetBytes(System.DateTime.Now.ToString("HH:mm:ss ff") + " -> " + message + _LineSeparator);
                        _CurrentLogFileStream.Write(bytes, 0, bytes.Length);
                        _CurrentLogFileStream.Flush();
                        _CurrentLogFileStream.Close();
                        throw new IOException();
                    }
                    catch (IOException ex)
                    {
                        try
                        {
                            _CurrentLogFileStream.Close();
                        }
                        catch (ObjectDisposedException)
                        {
                            EventLog.WriteEntry("Genius bank", "Exception occurred while writing text in log file.", EventLogEntryType.Warning);
                        }
                    }
                }
            }
        }

        public static void LogStringInFile(String message)
        {
            LogStringInFile(message, 0);
        }


        private static void OpenLogFileStream()
        {
            try
            {
                // Checking if the date changed since last time we logged something in the Log File
                if (!_CurrentLogFileDay.Equals(System.DateTime.Now.ToShortDateString()))
                {
                    // Building the log file's name: we take the name specified in the Global Configuration, and
                    // we had today's date, either after the last ".", or at the very end.
                    String fileName = _BaseLogFileName;
                    String filePath = _BaseLogFilePath;

                    Directory.CreateDirectory(filePath);
                    String suffix = String.Empty;
                    int dotIndex = fileName.LastIndexOf('.');

                    // If there is a "." in the log file name, we add the date after the dot:
                    // SampleName.log => SampleName23112008.log.
                    // Otherwise, we just add it at the end: SampleName => SampleName23112008
                    if (dotIndex != -1 && dotIndex > fileName.LastIndexOfAny("\\/".ToCharArray()))
                    {
                        suffix = fileName.Substring(dotIndex);
                        fileName = fileName.Substring(0, dotIndex);
                    }

                    _CurrentLogFileDay = System.DateTime.Now.ToShortDateString();
                    _CurrentLogFileName = filePath + fileName + "_" + _CurrentLogFileDay.Replace('/', '_') + suffix;
                }

                // If a file with today's date do not exist yet, we create it
                if (!File.Exists(_CurrentLogFileName))
                {

                    _CurrentLogFileStream = File.Open(_CurrentLogFileName, FileMode.OpenOrCreate);
                    byte[] bytes = new UTF8Encoding(true).GetBytes("\r\n=================" + System.DateTime.Now + " -> New Log File Created =================" + _LineSeparator);
                    _CurrentLogFileStream.Write(bytes, 0, bytes.Length);
                }
                // If a file with today's date does exist, we will append the new messages to it
                else
                {
                    _CurrentLogFileStream = File.Open(_CurrentLogFileName, FileMode.Append);
                }
            }
            catch (IOException ex)
            {
                // Nothing we can do here, since we cannot even access the Exception Log file
            }
        }

    }
}
