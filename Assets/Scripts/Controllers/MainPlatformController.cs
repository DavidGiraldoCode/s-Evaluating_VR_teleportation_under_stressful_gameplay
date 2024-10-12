using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlatformController : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        gameObject.SetActive(false);
    }
}
