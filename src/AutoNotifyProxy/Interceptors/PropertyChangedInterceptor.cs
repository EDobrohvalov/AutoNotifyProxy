using System.ComponentModel;
using Castle.Core.Interceptor;

namespace AutoNotifyProxy.Interceptors
{
    public class PropertyChangedInterceptor : IInterceptor, INotifyInvocation
    {
        readonly NotifyMode _fireOption;

        private event PropertyChangedEventHandler PropertyChangedBacking = (o, e) => { };
        public event PropertyChangedEventHandler PropertyChanged
        {
            add => PropertyChangedBacking += value;
            remove => PropertyChangedBacking -= value;
        }
        
        public PropertyChangedInterceptor(NotifyMode fireOption)
        {
            _fireOption = fireOption;
        }

        public void Intercept(IInvocation invocation)
        {
            InterceptionFactory.Create(this, invocation, _fireOption).Intercept();
        }

        public void Notify(IInvocation invocation)
        {
            Notify(invocation.PropertyName(), invocation.InvocationTarget);
        }

        private void Notify(string propertyName, object target)
        {
            PropertyChangedBacking(target, new PropertyChangedEventArgs(propertyName));
        }
    }
}