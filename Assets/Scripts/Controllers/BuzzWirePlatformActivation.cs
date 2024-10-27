using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

/// <summary>
/// This class subscribed to a SnapInteractable event `WhenStateChanged` to check if the final snap location of the 
/// buzz-wire game is `Select` to activate the platform on the gameplay.
/// It also checks if the Platform has `ActivationAllowed == true` to enable the activation of the platform
/// </summary>
public class BuzzWirePlatformActivation : MonoBehaviour
{
    [SerializeField] private PlatformState m_platformState;
    public PlatformState PlatformStateRef { set { m_platformState = value; } }
    [SerializeField] private SnapInteractable m_snapInteractable;

    private void Awake()
    {
        m_snapInteractable = GetComponent<SnapInteractable>();
    }

    private void OnEnable()
    {
        m_snapInteractable.WhenStateChanged += OnSnapChange;
    }
    private void OnDisable()
    {
        m_snapInteractable.WhenStateChanged -= OnSnapChange;
    }

    private void OnSnapChange(InteractableStateChangeArgs args)
    {
        /* RECALL THE InteractableState(s)
        public enum InteractableState
            {
                Normal,
                Hover,
                Select,
                Disabled
            }
        */
        if (args.NewState == InteractableState.Select)
            ActivatePlatform();
    }

    /// <summary>
    /// Activates the platform depending on whether it has ActivationAllowed == true
    /// </summary>
    private void ActivatePlatform()
    {
        if (m_platformState.CurrentState == PlatformState.state.FOCUSSED && m_platformState.ActivationAllowed)
        {
            m_platformState.ChangeState(PlatformState.state.ACTIVATED);
            Debug.Log("YYY Platfrom Activated!!!");
        }
    }

}
