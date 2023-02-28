using System;
using System.Collections.Generic;
using System.Text;

namespace Lodgify.Cinema.Domain.Contract
{
    public interface IImdbIdTranslatorService
    {
        string Get(int id);
        int Get(string id);
    }
}
