using UnityEngine;

public class MainPlatformController : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        gameObject.SetActive(false);
    }
}
