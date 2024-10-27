using UnityEngine;

/// <summary>
/// This component tells the CheatingController when the player has enter or left the buzzwire designated play zone to 
/// reset the location of the ring
/// </summary>
public class ResetBuzzWirePosition : MonoBehaviour
{
    public delegate void PlayerGameZoneTracker();
    public event PlayerGameZoneTracker OnPlayerLeftTheGameZone;
    public event PlayerGameZoneTracker OnPlayerEnterTheGameZone;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("XXX - Player just left the Buzz-Wire playing area, reset game");
            //Triggger events
            OnPlayerLeftTheGameZone?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("XXX - Player just enter the Buzz-Wire playing area, reset game");
            //Triggger events
            OnPlayerEnterTheGameZone?.Invoke();
        }
    }
}
