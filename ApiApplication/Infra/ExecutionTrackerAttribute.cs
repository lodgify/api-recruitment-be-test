using System;

using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Infra {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ExecutionTrackerAttribute : TypeFilterAttribute {
        public ExecutionTrackerAttribute() : base(typeof(ExecutionTrackerActionFilter)) {
        }
    }
}
