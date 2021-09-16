using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;


namespace LabReportAPI
{
   public class ExceptionHandler
    {

        /// <summary>
        /// Function to log the exceptions into APPLICATION-EVENT-VIWER.
        /// </summary>
        /// <param name="ParamException"></param>
        /// <param name="FunctionName"></param>
        public void WriteEventLogToFile(Exception ParamException, string FunctionName = "")
        {
            try
            {
                string strErrMsg = "Unhandled Exception at function : " + FunctionName + Environment.NewLine; 
                if (ParamException.Message != null)
                     strErrMsg += ParamException.Message.ToString() + Environment.NewLine;
                if (ParamException.StackTrace != null)
                    strErrMsg += ParamException.StackTrace.ToString() + Environment.NewLine;
                using (EventLog ExceptionEventLog = new EventLog("Application"))
                {
                    ExceptionEventLog.Source = "Application";
                    ExceptionEventLog.WriteEntry(strErrMsg, EventLogEntryType.Error);
                }
            }
            catch (Exception ex)
            {
                WriteEventLogToFile(ex);
            }
        }

    }
}
