using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Input;
using UnityEngine;

/// <summary>
/// This is a utility class to forcely exit the task.
/// </summary>
public class ExitTasks : MonoBehaviour
{
    [SerializeField] private ControllerRef _rightControllerRef;
    [SerializeField] private ControllerRef _leftControllerRef;
    [SerializeField] private uint EXIT_TIME = 5;
    [SerializeField] private float _counter;

    private void Update()
    {
        if (_rightControllerRef.ControllerInput.PrimaryButton && _leftControllerRef.ControllerInput.PrimaryButton)
        {
            _counter += 1 * Time.deltaTime;
            if (_counter >= EXIT_TIME)
            {
                Debug.Log("XXX EXIT!");
                if (GameplayManager.Instance)
                    GameplayManager.Instance.ExitGameplay();
                if (ExperimentManager.Instance)
                    ExperimentManager.Instance.ResetExperiment();
            }

        }
        else
        {
            _counter = 0.0f;
        }
    }
}
