using Oculus.Interaction;
using UnityEngine;
/// <summary>
/// The CheatingController keep track of two colliders, a sphere between the two points of the buzz-wire, and a bouding box
/// that represents the playable area. If the ring touches the sphere or leaves the bounding box, it will return no the default position.
/// </summary>
public class CheatingController : MonoBehaviour
{
    [Tooltip("References the trigger collider of the ring")]
    [SerializeField] private Collider _ringCollider;
    [Tooltip("References an invisible sphere in the middle of the ring to avoid player to pass the ring directly to the other location")]
    [SerializeField] private SphereCollider _sphereCollider;
    [Tooltip("References an insivible bounding box where the ring should move, if the player takes the ring out, the component will bring the ring back to the default position")]
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Transform _defaultLocation;
    [SerializeField] private InteractorActiveState _grabInteractorState;
    [SerializeField] private GameObject _ISDK_HandGrabInteraction;

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
            Debug.Log("XXX OnTriggerEnter: " + other);
            _ISDK_HandGrabInteraction.SetActive(false); // Disable the grabbing by force
            gameObject.transform.position = _defaultLocation.position;
            gameObject.transform.rotation = _defaultLocation.rotation;
            _ISDK_HandGrabInteraction.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == _boxCollider)
        {
            Debug.Log("XXX OnTriggerExit: " + other);
            _ISDK_HandGrabInteraction.SetActive(false); // Disable the grabbing by force
            gameObject.transform.position = _defaultLocation.position;
            gameObject.transform.rotation = _defaultLocation.rotation;
            _ISDK_HandGrabInteraction.SetActive(true);
        }
    }
}

