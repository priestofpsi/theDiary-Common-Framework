﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    public class FileEventArgs
        : EventArgs
    {
        public FileEventArgs()
            : base()
        {
        }
        
        public string FilePath
        {
            get;
        }

        public FileEvent Event
        {
            get;
        }
    }
}
