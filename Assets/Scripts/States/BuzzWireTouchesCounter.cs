using UnityEngine;

/// <summary>
/// Keeps track of all the touches during a current buzz-wire interaction in a given platform.
/// Gets reset every time the player start the game in another platfrom.
/// </summary>

[CreateAssetMenu(fileName = "BuzzWireTouchesCounter", menuName = "States/BuzzWireTouchesCounter", order = 0)]
public class BuzzWireTouchesCounter : ScriptableObject
{
    public uint touchesCount;
    public PlatformState currentPlatfrom;
}
