using System.Collections.Generic;
using System.ComponentModel;
using AutoNotifyProxy.Interceptors;
using Castle.Core.Interceptor;

namespace AutoNotifyProxy.Interceptions
{
    internal class PropertyIsINotifyInterception : PropertyChangedInterception, IWrappingInterception
    {
        private static readonly Dictionary<object, Dictionary<string, PropertyChangedEventHandler>> Handlers =
            new Dictionary<object, Dictionary<string, PropertyChangedEventHandler>>();

        public PropertyIsINotifyInterception(INotifyInvocation notifiedObject, IInvocation invocation) : base(
            notifiedObject, invocation)
        {
        }

        public void Before()
        {
            if (!(Invocation.GetCurrentValue() is INotifyPropertyChanged))
                return;
            if (Invocation.GetCurrentValue() == null)
                return;

            RemoveHandler(Invocation);
        }

        public void After()
        {
            if (!(Invocation.GetArgumentValue(0) is INotifyPropertyChanged))
                return;
            if (Invocation.GetArgumentValue(0) == null)
                return;

            AddHandler(Invocation, NotifiedObject);
        }

        private static void AddHandler(IInvocation invocation, INotifyInvocation propertyChangedInterceptor)
        {
            if (!Handlers.ContainsKey(invocation.InvocationTarget))
                Handlers.Add(invocation.InvocationTarget, new Dictionary<string, PropertyChangedEventHandler>());

            Handlers[invocation.InvocationTarget].Add(invocation.PropertyName(),
                (o, e) => { propertyChangedInterceptor.Notify(invocation); });

            ((INotifyPropertyChanged) invocation.GetArgumentValue(0)).PropertyChanged +=
                Handlers[invocation.InvocationTarget][invocation.PropertyName()];
        }

        private static void RemoveHandler(IInvocation invocation)
        {
            ((INotifyPropertyChanged) invocation.GetCurrentValue()).PropertyChanged -=
                Handlers[invocation.InvocationTarget][invocation.PropertyName()];
            Handlers.Remove(invocation.InvocationTarget);
        }
    }
}