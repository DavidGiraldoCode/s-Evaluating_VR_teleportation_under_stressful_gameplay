using UnityEngine;
using Oculus.Interaction.Input;

/// <summary>
/// This is a debugging class to read data form the controller
/// </summary>
public class ControllerDataReader : MonoBehaviour
{
    ControllerRef _controllerRef;
    public Transform _gameObjTransform;

    private void Awake()
    {
        _controllerRef = GetComponent<ControllerRef>();
    }

    private void Update()
    {
        if (_controllerRef.ControllerInput.TriggerButton)
        {
            Vector3 joyStickRotation = new Vector3(_controllerRef.ControllerInput.Primary2DAxis.x, 0.0f, _controllerRef.ControllerInput.Primary2DAxis.y);
            _gameObjTransform.rotation = Quaternion.LookRotation(joyStickRotation, Vector3.up);
        }
    }

    private void PrinterOfInput()
    {
        Debug.Log("GripButton:..........:" + _controllerRef.ControllerInput.GripButton);
        Debug.Log("TriggerButton:.......:" + _controllerRef.ControllerInput.TriggerButton);
        Debug.Log("MenuButton:..........:" + _controllerRef.ControllerInput.MenuButton);
        Debug.Log("Thumbrest:...........:" + _controllerRef.ControllerInput.Thumbrest);
        Debug.Log("Primary2DAxis:.......:" + _controllerRef.ControllerInput.Primary2DAxis);
        Debug.Log("Primary2DAxisClick:..:" + _controllerRef.ControllerInput.Primary2DAxisClick);
        Debug.Log("Primary2DAxisTouch:..:" + _controllerRef.ControllerInput.Primary2DAxisTouch);
        Debug.Log("PrimaryButton:.......:" + _controllerRef.ControllerInput.PrimaryButton);
        Debug.Log("PrimaryTouch:........:" + _controllerRef.ControllerInput.PrimaryTouch);
        Debug.Log("SecondaryButton:.....:" + _controllerRef.ControllerInput.SecondaryButton);
        Debug.Log("Secondary2DAxis:.....:" + _controllerRef.ControllerInput.Secondary2DAxis);
        Debug.Log("SecondaryTouch:......:" + _controllerRef.ControllerInput.SecondaryTouch);
    }

}
