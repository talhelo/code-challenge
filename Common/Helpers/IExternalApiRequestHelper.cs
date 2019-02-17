﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public interface IExternalApiRequestHelper
    {
        IList<T> Get<T>(string url, string query = "");
    }
}
