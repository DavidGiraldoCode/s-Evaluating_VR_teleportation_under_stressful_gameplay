using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a sphere sneakily placed in the ring to trigger the buzz-wire touches on the path. 
/// </summary>
public class BuzzWireProbeTrigger : MonoBehaviour
{
    public delegate void BuzzWireTouchesCounterDelegate();
    public event BuzzWireTouchesCounterDelegate OnBuzzWireTouch;

    //private PlatformState _platformState;
    //public PlatformState CurrentPlatformState { get => _platformState; }

    private void OnEnable()
    {
        //_platformState = GetComponentInParent<CheatingController>().CurrentPlatformState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BuzzWireTouches>(out BuzzWireTouches path))
        {
            //Debug.Log("XXX: " + path);
            path.SetColor(Color.red);
            OnBuzzWireTouch?.Invoke();
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
