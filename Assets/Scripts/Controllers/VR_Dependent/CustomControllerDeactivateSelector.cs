using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;

public class CustomControllerDeactivateSelector : MonoBehaviour, IActiveState
{
    public ControllerRef _rightControllerRef;
    public bool Active => _rightControllerRef.ControllerInput.PrimaryButton == false; //Negate the state becase we are intrested when the button get relesed
}
