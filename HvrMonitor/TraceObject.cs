using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HvrMonitor
{
    public class TraceObject
    {
        Int16 m_eventId;          // Event id of the trace (Information, Warning, Error).
        DateTime m_eventTime;     // Time of the trace event.
        String m_vmName;          // Name of the VM to which this trace is associated.
        Guid m_vmGuid;            // Guid of the VM to which this trace is associated.
        String m_functionName;    // Function name which has printed this trace.
        String m_fileName;        // File name which has this trace information.
        String m_traceMessage;    // Message of the trace.

        // Constructor
        public TraceObject()
        {
            m_eventId = 0;
            m_eventTime = DateTime.UtcNow;
            m_vmName = "";
            m_vmGuid = Guid.Empty;
            m_functionName = "";
            m_fileName = "";
            m_traceMessage = "";
        }

        #region Get & Set Methods

        // Get & Set methods for eventId.
        public Int16 EventId
        {
            get
            { 
                return m_eventId;
            }
            set
            {
                m_eventId = value;
            }
        }

        // Get & Set methods for eventTime.
        public DateTime EventTime
        {
            get
            {
                return m_eventTime;
            }
            set
            {
                m_eventTime = value;
            }
        }

        // Get & Set methods for vmName.
        public String VmName
        {
            get
            {
                return m_vmName;
            }
            set
            {
                m_vmName = value;
            }
        }

        // Get & Set methods for vmGuid.
        public Guid VmGuid
        {
            get
            {
                return m_vmGuid;
            }
            set
            {
                m_vmGuid = value;
            }
        }

        // Get & Set methods for functionName.
        public String FunctionName
        {
            get
            {
                return m_functionName;
            }
            set
            {
                m_functionName = value;
            }
        }

        // Get & Set methods for fileName.
        public String FileName
        {
            get
            {
                return m_fileName;
            }
            set
            {
                m_fileName = value;
            }
        }

        // Get & Set methods for traceMessage.
        public String TraceMessage
        {
            get
            {
                return m_traceMessage;
            }
            set
            {
                m_traceMessage = value;
            }
        }

        #endregion

        #region Temporary Functionality
        public void PrintTraceObject()
        {
            Console.WriteLine("\nEventId: {0}\nEventTime: {1}\nVmName: {2}\nVmGuid: {3}\nFunctionName: {4}\nFileName: {5}\nTraceMessage: {6}",
                m_eventId, m_eventTime, m_vmName, m_vmGuid, m_functionName, m_fileName, m_traceMessage);
        }
        #endregion
    }
}
