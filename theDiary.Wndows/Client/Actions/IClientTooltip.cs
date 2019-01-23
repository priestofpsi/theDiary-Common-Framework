using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Actions
{
    public delegate string ClientTooltipProviderHandler(object sender, ClientActionEventArgs e);

    public delegate string ClientTooltipProviderHandler<T>(object sender, ClientActionEventArgs<T> e);

    public interface IClientTooltip
    {
        ClientTooltipProviderHandler GetTooltip { get; }
    }

    public interface IClientTooltip<T>
    {
        ClientTooltipProviderHandler<T> GetTooltip { get; }
    }
}
