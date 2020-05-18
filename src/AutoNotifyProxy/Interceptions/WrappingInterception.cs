namespace AutoNotifyProxy.Interceptions
{
    internal class WrappingInterception : IInterception
    {
        private readonly IWrappingInterception _wrapper;
        private readonly IInterception _toWrap;

        public WrappingInterception(IWrappingInterception wrapper, IInterception toWrap)
        {
            _wrapper = wrapper;
            _toWrap = toWrap;
        }

        public void Intercept()
        {
            _wrapper.Before();
            _toWrap.Intercept();
            _wrapper.After();
        }
    }
}