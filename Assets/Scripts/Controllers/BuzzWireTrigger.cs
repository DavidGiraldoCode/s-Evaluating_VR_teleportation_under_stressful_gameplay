using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzWireTrigger : MonoBehaviour
{
    private PlatformState _platformState;
    public PlatformState CurrentPlatformState { get => _platformState; }

    private void OnEnable()
    {
        _platformState = GetComponentInParent<CheatingController>().CurrentPlatformState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BuzzWireTouches>(out BuzzWireTouches path))
        {
            //Debug.Log("XXX: " + path);
            path.SetColor(Color.red);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<BuzzWireTouches>(out BuzzWireTouches path))
        {
            //Debug.Log("XXX: " + path);
            path.SetColor(Color.white);
        }
    }
}
