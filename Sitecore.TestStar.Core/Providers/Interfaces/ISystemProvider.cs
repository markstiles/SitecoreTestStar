﻿using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Providers.Interfaces {
    public interface ISystemProvider {

        IEnumerable<TestSystem> GetSystems();

        TestSystem FillTestSystem(Item i);
    }
}
