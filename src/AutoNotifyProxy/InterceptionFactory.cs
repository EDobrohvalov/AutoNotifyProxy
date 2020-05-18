using AutoNotifyProxy.Interceptions;
using AutoNotifyProxy.Interceptors;
using Castle.Core.Interceptor;

namespace AutoNotifyProxy
{
    internal static class InterceptionFactory
    {
        public static IInterception Create(
            PropertyChangedInterceptor propertyChangedInterceptor,
            IInvocation invocation, 
            NotifyMode fireOption)
           
        {
            switch (invocation)
            {
                case { } inv when inv.IsPropertyChangedAdd(): 
                    return new PropertyChangedAddInterception(propertyChangedInterceptor, inv);
                case { } inv when inv.IsPropertyChangedRemove() : 
                    return new PropertyChangedRemoveInterception(propertyChangedInterceptor, inv);
                case { } inv when inv.IsPropertySetter() && NotifyMode.OnlyOnChange == fireOption:
                    return new OnlyOnChangePropertySetterInterception(propertyChangedInterceptor, invocation)
                        .WrapWith(new PropertyIsINotifyInterception(propertyChangedInterceptor, invocation));
                case { } inv when inv.IsPropertySetter():
                    return new PropertySetterInterception(propertyChangedInterceptor, invocation)
                        .WrapWith(new PropertyIsINotifyInterception(propertyChangedInterceptor, invocation));
                default:
                    return new InvocationInterception(invocation);       
            }
        }


        private static IInterception WrapWith(this IInterception target, IWrappingInterception wrapper)
        {
            return new WrappingInterception(wrapper, target);
        }
    }
}