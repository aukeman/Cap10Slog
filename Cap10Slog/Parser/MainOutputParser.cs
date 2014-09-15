using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.IO;

using Cap10Slog.Model;

namespace Cap10Slog.Parser
{
    class MainOutputParser
    {
        public static LogFile Parse(string filepath)
        {
            var result = new LogFile();
            result.FilePath = filepath;

            using (var r = new StreamReader(filepath))
            {
                var threads = new Dictionary<string, LogThread>();
                var startOfRecordRegex = new Regex(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}.\d{3}\|");
                var recordRegex = new Regex(@"^(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}.\d{3})\|(.*)\|(.*)$", RegexOptions.Multiline);

                var currentRecordBuffer = new StringBuilder();

                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();

                    if ( startOfRecordRegex.IsMatch(line) && 0 < currentRecordBuffer.Length )
                    {
                        string currentRecordString = currentRecordBuffer.ToString();

                        Match m = recordRegex.Match(currentRecordString);

                        if ( m.Success )
                        {
                            Group dateGroup = m.Groups[1];
                            Group dataGroup = m.Groups[2];
                            Group threadGroup = m.Groups[3];

                            var currentRecord = new LogRecord(currentRecordString, DateTime.Parse(dateGroup.Value), dataGroup.Index, dataGroup.Length);
                                
                            string threadID = threadGroup.Value;

                            if ( !threads.ContainsKey(threadID) )
                            {
                                var logThread = new LogThread();
                                                        
                                logThread.ThreadID = threadID;
                                logThread.LogRecords = new List<LogRecord>();

                                threads.Add(threadID, logThread);
                            }

                            threads[threadID].LogRecords.Add(currentRecord);
                        }
                        else
                        {

                        }

                        currentRecordBuffer.Clear();
                    }

                    currentRecordBuffer.Append(line);

                }

                result.Threads = new List<LogThread>();
                foreach (KeyValuePair<string,LogThread> kvp in threads)
                {
                    result.Threads.Add(kvp.Value);
                }              
            }

            return result;
        }
    }
}
