using System;
using System.ComponentModel;
using System.Diagnostics;
using AutoNotifyProxy.Interceptors;
using Castle.DynamicProxy;

namespace AutoNotifyProxy
{
    public enum NotifyMode
    {
        Always,
        OnlyOnChange
    }

    public interface INotifyProxyGenerator
    {
        T Make<T>(NotifyMode mode, params object[] ctorArgs) where T : class;
    }

    public class NotifyProxyGenerator : INotifyProxyGenerator
    {
        private readonly ProxyGenerator _generator = new ProxyGenerator();

        public T Make<T>(NotifyMode mode = NotifyMode.OnlyOnChange, params object[] ctorArgs) where T : class =>
            typeof(T) switch
            {
                Type type when type.IsClass => MakeForClass<T>(mode, ctorArgs),
                _ => throw new NotImplementedException()
            };

        private T MakeForClass<T>(NotifyMode mode = NotifyMode.OnlyOnChange, params object[] ctorArgs) 
            where T : class
        {
            return _generator.CreateClassProxy(typeof(T),
                new[] {typeof(INotifyPropertyChanged)},
                ProxyGenerationOptions.Default,
                ctorArgs,
                new PropertyChangedInterceptor(mode)
            ) as T;
        }
    }
}