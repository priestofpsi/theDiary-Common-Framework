﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Actions
{
    public interface IClientExecutionAction
        : IClientAction
    {
        ActionEventHandler Execute { get; }
    }
}
