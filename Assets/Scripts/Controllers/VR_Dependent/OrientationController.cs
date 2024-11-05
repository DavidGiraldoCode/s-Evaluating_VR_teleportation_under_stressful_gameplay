using UnityEngine;
using Oculus.Interaction.Input;
using Oculus.Interaction.Locomotion;
using Oculus.Interaction;

/// <summary>
/// This is a debugging class to read data form the controller
/// </summary>
public class OrientationController : MonoBehaviour
{

    [Tooltip("Reads the data from the right controller to trigger teleportation with Primary (A) button")]
    [SerializeField] private ControllerRef _rightControllerRef;

    [Tooltip("Reads data from left controller to change the orientation of the final teleportation destination")]
    [SerializeField] private ControllerRef _leftControllerRef;


    [Tooltip("The reference to the mesh that has the teleport interactable, and allows to set the orientation at destination")]
    [SerializeField] private TeleportInteractable _teleportInteractable;

    [Tooltip("The reference component that enables teleportation interactions, gives the initial orientation of the arc" +
                "and the state of the selection of a teleportation interactable candidate")]
    [SerializeField] private TeleportInteractor _teleportInteractor;

    [Tooltip("The transform of the future destination and orientation point")]
    [SerializeField] private Transform _orientationTransform;
    [Tooltip("The visual feedback for the player to see the future orientation")]
    [SerializeField] private GameObject _arrowVisualFeedback;

    /// <summary>
    /// Store the previous position
    /// </summary>
    private Transform _gameObjTransformPrevious;

    [Tooltip("The component that selects where to teleport, is trigger on when the primary A button is released")]
    [SerializeField] private ActiveStateSelector _activeStateSelector;

    [Tooltip("The component that enables the selectos to be ready for secting. It allow to preview the rotation")]
    [SerializeField] private ActiveStateSelector _openSelector;

    [Tooltip("A control member variables to enable or disable orientatio definition at runtime")]
    [SerializeField] private bool _canChangeOrientation = false;
    /// <summary>
    /// Use enable or disable the orientation based on the condition
    /// </summary>
    public bool CanChangeOrientation { get => _canChangeOrientation; set => _canChangeOrientation = value; }


    [Tooltip("Is the head/endpoint of the arc, with a target that is use to show the arrow in the future position")]
    [SerializeField] private Transform _arcVisualTarget;

    private void Awake()
    {
        _activeStateSelector.WhenSelected += OnSelected;
        _activeStateSelector.WhenUnselected += OnUnselected;

        _openSelector.WhenSelected += OnOpenSelected;
        _openSelector.WhenUnselected += OnOpenUnselected;

        _gameObjTransformPrevious = _orientationTransform;
        _orientationTransform.gameObject.SetActive(false);
        _arrowVisualFeedback.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        _activeStateSelector.WhenSelected -= OnSelected;
        _activeStateSelector.WhenUnselected -= OnUnselected;

        _openSelector.WhenSelected -= OnOpenSelected;
        _openSelector.WhenUnselected -= OnOpenUnselected;
    }


    private void OnOpenSelected()
    {
        _orientationTransform.gameObject.SetActive(true);
        _orientationTransform.forward = _teleportInteractor.ArcOrigin.forward;
        _gameObjTransformPrevious = _orientationTransform;

        _arrowVisualFeedback.gameObject.SetActive(_canChangeOrientation);
    }
    private void OnOpenUnselected()
    {
        _orientationTransform.gameObject.SetActive(false);
        _arrowVisualFeedback.gameObject.SetActive(false);
    }

    /// <summary>
    /// When a valid teleportationInteractable has been selected, the transform information (position and rotation)
    /// of the arrow is injected as the target point. As the destination has been selected, the visual feedback is hidden
    /// </summary>
    private void OnSelected()
    {
        Debug.Log("XXX _teleportInteractor.HasValidDestination() " + _teleportInteractor.HasValidDestination());
        if (_teleportInteractor.HasValidDestination())
        {
            _orientationTransform.position = _teleportInteractor.ArcEnd.Point;
            _teleportInteractable.InjectOptionalTargetPoint(_orientationTransform);
        }

        _arrowVisualFeedback.gameObject.SetActive(false);

    }

    /// <summary>
    /// When no interactables is selected, the arrow gets back to it initial position
    /// </summary>
    private void OnUnselected()
    {
        if (!_teleportInteractor.HasValidDestination())
        {
            _orientationTransform = _gameObjTransformPrevious;
        }
    }


    private void Update()
    {
        if (_canChangeOrientation)
            SetOrientationDestination();
    }

    /// <summary>
    /// While the primary button (A) on the right controller is pressed, the player can rotate
    /// the arrow that represents the orientation at the destination point.
    /// </summary>
    private void SetOrientationDestination()
    {
        if (_rightControllerRef.ControllerInput.PrimaryButton)
        {
            _orientationTransform.position = _arcVisualTarget.position;
            if (_leftControllerRef.ControllerInput.Primary2DAxis.magnitude > 0)
            {
                Vector3 joyStickRotation = new Vector3(_leftControllerRef.ControllerInput.Primary2DAxis.x, 0.0f, _leftControllerRef.ControllerInput.Primary2DAxis.y);
                _orientationTransform.rotation = Quaternion.LookRotation(joyStickRotation, Vector3.up);
            }

        }
    }

}

