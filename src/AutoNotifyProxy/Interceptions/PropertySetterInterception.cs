using AutoNotifyProxy.Interceptors;
using Castle.Core.Interceptor;

namespace AutoNotifyProxy.Interceptions
{
    internal class PropertySetterInterception : PropertyChangedInterception, IInterception
    {
        public PropertySetterInterception(INotifyInvocation notifiedObject, IInvocation invocation) : base(notifiedObject, invocation)
        {
        }

        public void Intercept()
        {
            Invocation.Proceed();
            NotifiedObject.Notify(Invocation);
        }
    }
}