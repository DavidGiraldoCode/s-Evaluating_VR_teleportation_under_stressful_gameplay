using UnityEngine;

public class ResetBuzzWirePosition : MonoBehaviour
{
    public delegate void PlayerLeftTheGameZone();
    public event PlayerLeftTheGameZone OnPlayerLeftTheGameZone;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("XXX - Player just left the Buzz-Wire playing area, reset game");
            //Triggger events
            OnPlayerLeftTheGameZone?.Invoke();
        }
    }
}
