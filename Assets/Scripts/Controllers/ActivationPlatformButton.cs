using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationPlatformButton : MonoBehaviour
{
    [SerializeField] private PlatformState m_platformState;
    public PlatformState PlatformStateRef { set { m_platformState = value; } }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        if (m_platformState.CurrentState == PlatformState.state.FOCUSSED && m_platformState.AvitationAllowed)
        {
            m_platformState.ChangeState(PlatformState.state.ACTIVATED);
            Debug.Log("Platfrom Activated!!!");
        }
    }
}
