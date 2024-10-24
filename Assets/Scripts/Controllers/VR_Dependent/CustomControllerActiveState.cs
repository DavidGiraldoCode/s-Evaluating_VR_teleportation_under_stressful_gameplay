using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;

public class CustomControllerActiveState : MonoBehaviour, IActiveState
{
    public ControllerRef _rightControllerRef;
    private bool _readyForTeleporting = false;
    private bool _active = false;
    [SerializeField, Interface(typeof(ISelector))]
    private ActiveStateSelector _selector;
    public InteractorActiveState _interactor;
    public bool Active
    {
        get
        {
            return _rightControllerRef.ControllerInput.PrimaryButton == false;
            if (!_rightControllerRef.ControllerInput.PrimaryButton && _readyForTeleporting)
            {
                _active = true;
                _readyForTeleporting = false;
                Debug.Log("XXX A Button Released, _interactor.Active: " + _interactor.Active);
            }
            else if (_rightControllerRef.ControllerInput.PrimaryButton && !_readyForTeleporting)
            {
                Debug.Log("XXX A Button Pressed");
                _readyForTeleporting = true;
                _active = _interactor.Active == false;
                Debug.Log("XXX _interactor.Active: " + _interactor.Active);
            }
            //_active = false;
            

        }
    }//=> _rightControllerRef.ControllerInput.PrimaryButton == false; //Negate the state becase we are intrested when the button get relesed

    private void OnEnable()
    {
        _selector.WhenSelected += OnControllerSelected;
    }
    private void OnDisable()
    {
        _selector.WhenSelected -= OnControllerSelected;
    }

    private void OnControllerSelected()
    {
        Debug.Log("XXX Selection happens");
    }
}
