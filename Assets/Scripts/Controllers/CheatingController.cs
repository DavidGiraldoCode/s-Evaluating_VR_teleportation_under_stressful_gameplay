using Oculus.Interaction;
using UnityEngine;

/// <summary>
/// The CheatingController keep track of two colliders, a sphere between the two points of the buzz-wire, and a bouding box
/// that represents the playable area. If the ring touches the sphere or leaves the bounding box, it will return no the default position.
/// </summary>
public class CheatingController : MonoBehaviour
{
    [Tooltip("This is the platform the cheating controller is protecting," +
            "and is then use to track data. The PlatfromStateController passes down to the CheatingController," +
            " And this passes down the PlatformState to the BuzzWireProbeTrigger")]
    [SerializeField] private PlatformState _platformState;
    /// <summary>
    /// This is the platform the cheating controller is protecting,
    /// and is then use to track data. The PlatfromStateController passes down to the CheatingController,
    /// And this passes down the PlatformState to the BuzzWireProbeTrigger
    /// </summary>
    public PlatformState PlatformStateRef { get => _platformState; set => _platformState = value; }
    [Tooltip("References the trigger collider of the ring")]
    [SerializeField] private Collider _ringCollider;
    [Tooltip("References an invisible sphere in the middle of the ring to avoid player to pass the ring directly to the other location")]
    [SerializeField] private SphereCollider _sphereCollider;
    [Tooltip("References an insivible bounding box where the ring should move, if the player takes the ring out, the component will bring the ring back to the default position")]
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Transform _defaultLocation;
    [SerializeField] private InteractorActiveState _grabInteractorState;
    [SerializeField] private GameObject _ISDK_HandGrabInteraction;
    [SerializeField] private ResetBuzzWirePosition _resetBuzzWirePosition;
    private BuzzWireProbeTrigger[] _buzzWireProbeTriggers = new BuzzWireProbeTrigger[6];

    [SerializeField] private InteractorActiveState _snapIneractorActiveState;

    [Tooltip("The second/end location of the BuzzWire snap location")]
    [SerializeField] private SnapInteractable _snapInteractable;
    [SerializeField] private InteractableState _snapIneractableState;

    private void Awake()
    {
        if (!_platformState)
            throw new System.NullReferenceException("The PlatformState is missing");

        _buzzWireProbeTriggers = GetComponentsInChildren<BuzzWireProbeTrigger>();
        
        SetActiveBizzWireProbes(false);
        ResetBuzzWirePosition();

    }
    private void OnEnable()
    {
        _resetBuzzWirePosition.OnPlayerLeftTheGameZone += OnPlayerLeftTheGameZone;
        _resetBuzzWirePosition.OnPlayerEnterTheGameZone += OnPlayerEnterTheGameZone;

        //_snapInteractable.WhenStateChanged += OnSnapChange;
    }

    private void OnDisable()
    {
        _resetBuzzWirePosition.OnPlayerLeftTheGameZone -= OnPlayerLeftTheGameZone;
        _resetBuzzWirePosition.OnPlayerEnterTheGameZone -= OnPlayerEnterTheGameZone;

        //_snapInteractable.WhenStateChanged -= OnSnapChange;
    }

    //TODO remove, it is now on BuzzWirePlatformActivation
    // private void OnSnapChange(InteractableStateChangeArgs args)
    // {
    //     /* RECALL THE InteractableState(s)
    //     public enum InteractableState
    //         {
    //             Normal,
    //             Hover,
    //             Select,
    //             Disabled
    //         }
    //     */
    //     //Debug.Log("XXX OnSnapChange " + args.NewState);
    //     if (args.NewState == InteractableState.Select)
    //     {
    //         Debug.Log("YYY Activate Platform!");
    //     }
    // }

    private void OnPlayerEnterTheGameZone()
    {
        SetActiveBizzWireProbes(true);
    }

    private void OnPlayerLeftTheGameZone()
    {
        ResetBuzzWirePosition();
        SetActiveBizzWireProbes(false);
    }

    private void Update()
    {
        //Debug.Log("XXX _grabInteractorState " + _grabInteractorState.Active);
#if UNITY_EDITOR
        //Debug.Log("XXX _snapIneractableActiveState " + _snapIneractableState);
        //        Debug.Log("XXX _snapInteractable" + _snapInteractable);
#endif

    }
    private void OnTriggerEnter(Collider other)
    {
        CheckAntiCheatingVolumes(other, true);
        /**
         If the player tries to pass the ring directly to the other side and hits the anticheating sphere, 
         the ring will be set back to the default position
        */
        // if (other == _sphereCollider)
        // {
        //     //Debug.Log("XXX OnTriggerEnter: " + other.gameObject.name);
        //     ResetBuzzWirePosition();
        // }
    }
    private void OnTriggerExit(Collider other)
    {
        CheckAntiCheatingVolumes(other, false);
        /**
         If the player tries to take the ring outside anticheating bounding volume, 
         the ring will be set back to the default position
        */
        // if (other == _boxCollider)
        // {
        //     //Debug.Log("XXX OnTriggerExit Other is: " + other.gameObject.name + " trigger by " + name);
        //     ResetBuzzWirePosition();
        // }

    }
    /// <summary>
    /// This validation checks two scenarios,
    /// (1) If the player tries to pass the ring directly to the other side and hits the anticheating sphere, 
    /// the ring will be set back to the default position.
    /// And (2) If the player tries to take the ring outside anticheating bounding volume,
    /// the ring will be set back to the default position
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="isEntering"></param>
    private void CheckAntiCheatingVolumes(Collider collider, bool isEntering)
    {
        if (collider == _sphereCollider && isEntering)
            ResetBuzzWirePosition();
        else if (collider == _boxCollider && !isEntering)
            ResetBuzzWirePosition();
    }

    /// <summary>
    /// Takes the GrabbableRing GameObject and disables the GrabInteractor for a moment to change 
    /// the position and orientation back to the default state. Then, enables the GrabInteractor again.
    /// </summary>
    private void ResetBuzzWirePosition()
    {
        _ISDK_HandGrabInteraction.SetActive(false); // Disable the grabbing by force
        gameObject.transform.position = _defaultLocation.position;
        gameObject.transform.rotation = _defaultLocation.rotation;
        _ISDK_HandGrabInteraction.SetActive(true);
    }

    /// <summary>
    /// Enables and disables the probes that trigger the buzzwire errors so they do not count in the 
    /// physics collision checking loop when the user is not in the platform at that moment
    /// <param name="activeState"></param>
    /// </summary>
    private void SetActiveBizzWireProbes(bool activeState)
    {
        foreach (var probe in _buzzWireProbeTriggers)
        {
            probe.gameObject.SetActive(activeState);
        }
    }
}

