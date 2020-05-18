namespace AutoNotifyProxy.Interceptions
{
    internal interface IWrappingInterception
    {
        void Before();
        void After();
    }
}