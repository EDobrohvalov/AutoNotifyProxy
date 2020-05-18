using System.ComponentModel;
using AutoNotifyProxy.Interceptors;
using Castle.Core.Interceptor;

namespace AutoNotifyProxy.Interceptions
{
    internal abstract class PropertyChangedInterception 
    {
        protected readonly INotifyInvocation NotifiedObject;
        protected readonly IInvocation Invocation;

        protected PropertyChangedInterception(INotifyInvocation notifiedObject, IInvocation invocation)
        {
            NotifiedObject = notifiedObject;
            Invocation = invocation;
        }
    }
}