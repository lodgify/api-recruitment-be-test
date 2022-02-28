using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Business.ConfigurationHelper
{
    public interface IAppConfiguration
    {
        string BaseUrl { get; }
        string Key { get; }
    }
}
