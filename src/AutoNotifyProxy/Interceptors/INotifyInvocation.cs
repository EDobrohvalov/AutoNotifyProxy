using System.ComponentModel;
using Castle.Core.Interceptor;

namespace AutoNotifyProxy.Interceptors
{
    interface INotifyInvocation : INotifyPropertyChanged
    {
        void Notify(IInvocation invocation);
    }
}