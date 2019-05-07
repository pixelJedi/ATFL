using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ATFL
{
    class LogWriter : TextWriter
    {
        StreamWriter testLog;
        public LogWriter(string path)
        {
            testLog = new StreamWriter(path);
        }
        public override void Write(char value)
        {
            base.Write(value);
            testLog.Write(value);
        }
        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
