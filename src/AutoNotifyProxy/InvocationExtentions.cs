using Castle.Core.Interceptor;

namespace AutoNotifyProxy
{
    internal static class InvocationExtentions
    {
        private const string SetPrefix = "set_";
        private const string AddPropertyChanged = "add_PropertyChanged";
        private const string RemovePropertyChanged = "remove_PropertyChanged";

        public static bool IsPropertyChangedAdd(this IInvocation invocation)
        {
            return invocation.Method.Name == AddPropertyChanged;
        }

        public static bool IsPropertyChangedRemove(this IInvocation invocation)
        {
            return invocation.Method.Name == RemovePropertyChanged;
        }

        public static bool IsPropertySetter(this IInvocation invocation)
        {
            return invocation.Method.IsSpecialName && invocation.Method.Name.StartsWith(SetPrefix);
        }

        public static string PropertyName(this IInvocation invocation)
        {
            return invocation.Method.Name.Substring(SetPrefix.Length);
        }

        public static object GetCurrentValue(this IInvocation propertySetInvocation)
        {
            return propertySetInvocation.InvocationTarget
                .GetType()
                .GetProperty(propertySetInvocation.PropertyName())
                ?.GetValue(propertySetInvocation.InvocationTarget, new object[0]);
        }
    }
}