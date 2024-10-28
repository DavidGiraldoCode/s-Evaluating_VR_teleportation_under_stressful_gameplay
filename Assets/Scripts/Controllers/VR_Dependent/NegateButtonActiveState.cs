using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;

/// <summary>
/// This component just allows the selector to be active once the primary button (A) is released
/// </summary>
public class NegateButtonActiveState : MonoBehaviour, IActiveState
{
    [SerializeField] private ControllerRef _rightControllerRef;
    public bool Active => _rightControllerRef.ControllerInput.PrimaryButton == false;
}
