using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;

public class CustomControllerDeactivateSelector : MonoBehaviour, IActiveState
{
    public ControllerRef _rightControllerRef;
    public InteractorActiveState _interactor;
    public bool Active 
    {
        get 
        { 
            Debug.Log("XXX Deactivating, InteractorActiveState " + _interactor.Active);
            return (_rightControllerRef.ControllerInput.Thumbrest && _interactor.Active == false); //Negate the state becase we are intrested when the button get relesed
        }
    }
}
