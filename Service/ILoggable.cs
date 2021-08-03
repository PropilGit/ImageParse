using System;
using System.Collections.Generic;
using System.Text;

namespace ImageParse.Service
{
    interface ILoggable
    {
        public delegate void AddLog(string message, bool isError = true);
        public event AddLog onAddLog;
    }
}
