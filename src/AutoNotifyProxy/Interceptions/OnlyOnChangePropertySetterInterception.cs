using AutoNotifyProxy.Interceptors;
using Castle.Core.Interceptor;

namespace AutoNotifyProxy.Interceptions
{
    internal class OnlyOnChangePropertySetterInterception : PropertyChangedInterception, IInterception
    {
        public OnlyOnChangePropertySetterInterception(INotifyInvocation notifiedObject, IInvocation invocation)
            : base(notifiedObject, invocation)
        {
        }

        public void Intercept()
        {
            var oldValue = Invocation.GetCurrentValue();
            Invocation.Proceed();
            var newValue = Invocation.GetArgumentValue(0);

            if (AreEqual(oldValue, newValue))
            {
                return;
            }

            NotifiedObject.Notify(Invocation);
        }

        private static bool AreEqual(object oldValue, object newValue)
        {
            return (oldValue == null && newValue == null)
                   || (oldValue != null && oldValue.Equals(newValue))
                   || (newValue != null && newValue.Equals(oldValue));
        }
    }
}