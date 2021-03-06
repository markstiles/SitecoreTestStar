﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities.Interfaces {
    public interface ITestEnvironment {

        string ID { get; set; }
        string Name { get; set; }
        string DomainPrefix { get; set; }
        string IPAddress { get; set; }
    }
}
