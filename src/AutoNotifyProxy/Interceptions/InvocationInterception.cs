using Castle.Core.Interceptor;

namespace AutoNotifyProxy.Interceptions
{
    internal class InvocationInterception : IInterception
    {
        readonly IInvocation _invocation;

        public InvocationInterception(IInvocation invocation)
        {
            _invocation = invocation;
        }

        public void Intercept()
        {
            _invocation.Proceed();
        }
    }
}