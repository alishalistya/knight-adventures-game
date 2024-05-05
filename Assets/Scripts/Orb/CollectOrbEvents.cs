using UnityEngine;

public class CollectOrbEvents : MonoBehaviour
{
    public delegate void CollectEventHandler(string orbName);
    public static event CollectEventHandler OnOrbCollected;

    public static void OrbCollected(string orbName)
    {
        OnOrbCollected?.Invoke(orbName);
    }
}