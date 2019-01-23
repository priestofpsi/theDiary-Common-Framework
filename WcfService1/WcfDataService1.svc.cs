//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using System.Reflection;
using System.Data;
using System.Data.Entity;

namespace WcfService1
{
    public class WcfDataService1 : DataService<WahlEntities>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            //foreach(var prop in typeof(WahlEntities).GetProperties().Where(a=>a.PropertyType.IsGenericType(typeof(DbSet<>))))
            //    config.SetEntitySetAccessRule(prop.Name, EntitySetRights.All);
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
        }
    }
}
