using System.ComponentModel;
using AutoNotifyProxy.Interceptors;
using Castle.Core.Interceptor;

namespace AutoNotifyProxy.Interceptions
{
    internal class PropertyChangedRemoveInterception : PropertyChangedInterception, IInterception
    {
        public PropertyChangedRemoveInterception(INotifyInvocation notifiedObject, IInvocation invocation)
            :base(notifiedObject, invocation){ }

        public void Intercept()
        {
            var onPropertyChanged = (PropertyChangedEventHandler)Invocation.GetArgumentValue(0);
            NotifiedObject.PropertyChanged -= onPropertyChanged;
        }
    }
}