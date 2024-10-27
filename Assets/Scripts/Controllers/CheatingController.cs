using Oculus.Interaction;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// The CheatingController keep track of two colliders, a sphere between the two points of the buzz-wire, and a bouding box
/// that represents the playable area. If the ring touches the sphere or leaves the bounding box, it will return no the default position.
/// </summary>
public class CheatingController : MonoBehaviour
{
    [Tooltip("This is the platform the cheating controller is protecting, and is then use to track data. Gets pass down to the BuzzWireTrigger")]
    [SerializeField] private PlatformState _platformState;
    public PlatformState CurrentPlatformState { get => _platformState; }
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

    private void Awake()
    {
        if (!_platformState)
            throw new System.NullReferenceException("The PlatformState is missing");

        _buzzWireProbeTriggers = GetComponentsInChildren<BuzzWireProbeTrigger>();
        SetActiveBizzWireProbes(false);

    }
    private void OnEnable()
    {
        _resetBuzzWirePosition.OnPlayerLeftTheGameZone += OnPlayerLeftTheGameZone;
        _resetBuzzWirePosition.OnPlayerEnterTheGameZone += OnPlayerEnterTheGameZone;


    }

    private void OnDisable()
    {
        _resetBuzzWirePosition.OnPlayerLeftTheGameZone -= OnPlayerLeftTheGameZone;
        _resetBuzzWirePosition.OnPlayerEnterTheGameZone -= OnPlayerEnterTheGameZone;
    }

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
        Debug.Log("XXX _grabInteractorState " + _grabInteractorState.Active);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("XXX OnTriggerEnter: " + other);
        /**
         If the player tries to pass the ring directly to the other side and hits the anticheating sphere, the ring will be set back to the default position
        */
        if (other == _sphereCollider)
        {
            Debug.Log("XXX OnTriggerEnter: " + other.gameObject.name);
            ResetBuzzWirePosition();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        BoxCollider col = gameObject.GetComponent<BoxCollider>();
        if (other == _boxCollider)
        {
            Debug.Log("XXX OnTriggerExit Other is: " + other.gameObject.name + " trigger by " + name);
            ResetBuzzWirePosition();
        }

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
    /// </summary>
    /// <param name="activeState"></param>
    private void SetActiveBizzWireProbes(bool activeState)
    {
        foreach (var probe in _buzzWireProbeTriggers)
        {
            probe.gameObject.SetActive(activeState);
        }
    }
}

