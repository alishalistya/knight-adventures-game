public class ShopEvents
{
    public delegate void ShopEventHandler();
    public static event ShopEventHandler OnTimerStarted;

    public static void ShopTimerStarted()
    {
        OnTimerStarted?.Invoke();
    }
}