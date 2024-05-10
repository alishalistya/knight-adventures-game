public class CheatEvents
{
    public delegate void CheatEventHandler(StatusCheats cheat);
    public static event CheatEventHandler OnCheatActivated;

    // public static void CheatOrbActivated(string cheatName)
    // {
    //     OnCheatActivated?.Invoke(cheat);
    // }
}