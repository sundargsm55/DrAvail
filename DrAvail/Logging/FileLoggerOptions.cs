using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Logging
{
    public class FileLoggerOptions
    {
        public virtual string FilePath { get; set; }

        public virtual string FolderPath { get; set; }
    }
}
