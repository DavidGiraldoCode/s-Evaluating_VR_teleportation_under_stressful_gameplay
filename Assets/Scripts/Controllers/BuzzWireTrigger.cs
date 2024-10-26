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
}
