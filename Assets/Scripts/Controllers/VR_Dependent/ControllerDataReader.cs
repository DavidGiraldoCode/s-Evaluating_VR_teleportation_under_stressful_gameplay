using UnityEngine;
using Oculus.Interaction.Input;

/// <summary>
/// This is a debugging class to read data form the controller
/// </summary>
public class ControllerDataReader : MonoBehaviour
{
    public ControllerRef _rightControllerRef;
    public ControllerRef _leftControllerRef;

    public Transform _gameObjTransform;

    private void Awake()
    {
    }

    private void Update()
    {
        if (_rightControllerRef.ControllerInput.PrimaryButton)
        {
            if (_leftControllerRef.ControllerInput.Primary2DAxis.magnitude > 0)
            {
                Vector3 joyStickRotation = new Vector3(_leftControllerRef.ControllerInput.Primary2DAxis.x, 0.0f, _leftControllerRef.ControllerInput.Primary2DAxis.y);
                _gameObjTransform.rotation = Quaternion.LookRotation(joyStickRotation, Vector3.up);
            }

        }
    }

    private void PrinterOfInput()
    {
        Debug.Log("GripButton:..........:" + _rightControllerRef.ControllerInput.GripButton);
        Debug.Log("TriggerButton:.......:" + _rightControllerRef.ControllerInput.TriggerButton);
        Debug.Log("MenuButton:..........:" + _rightControllerRef.ControllerInput.MenuButton);
        Debug.Log("Thumbrest:...........:" + _rightControllerRef.ControllerInput.Thumbrest);
        Debug.Log("Primary2DAxis:.......:" + _rightControllerRef.ControllerInput.Primary2DAxis);
        Debug.Log("Primary2DAxisClick:..:" + _rightControllerRef.ControllerInput.Primary2DAxisClick);
        Debug.Log("Primary2DAxisTouch:..:" + _rightControllerRef.ControllerInput.Primary2DAxisTouch);
        Debug.Log("PrimaryButton:.......:" + _rightControllerRef.ControllerInput.PrimaryButton);
        Debug.Log("PrimaryTouch:........:" + _rightControllerRef.ControllerInput.PrimaryTouch);
        Debug.Log("SecondaryButton:.....:" + _rightControllerRef.ControllerInput.SecondaryButton);
        Debug.Log("Secondary2DAxis:.....:" + _rightControllerRef.ControllerInput.Secondary2DAxis);
        Debug.Log("SecondaryTouch:......:" + _rightControllerRef.ControllerInput.SecondaryTouch);
    }

}

