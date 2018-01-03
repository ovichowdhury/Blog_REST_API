using Injection;
using Injection.Interfaces;
using PWService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog_REST_API
{
    public static class Injector
    {
        public static IInjectionContainer container { get; set; }

        static Injector()
        {
            container = new Container();
        }

        public static void Configure()
        {
            container.Register<ServiceFactory, ServiceFactory>();
        }
    }
}