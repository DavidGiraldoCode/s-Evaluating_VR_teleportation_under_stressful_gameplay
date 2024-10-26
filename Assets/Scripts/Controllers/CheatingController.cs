using UnityEngine;

public class CheatingController : MonoBehaviour
{
    [Tooltip("References the trigger collider of the ring")]
    [SerializeField] private Collider _ringCollider;
    [Tooltip("References an invisible sphere in the middle of the ring to avoid player to pass the ring directly to the other location")]
    [SerializeField] private SphereCollider _sphereCollider;
    [Tooltip("References an insivible bounding box where the ring should move, if the player takes the ring out, the component will bring the ring back to the default position")]
    [SerializeField] private BoxCollider _boxCollider;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("XXX OnTriggerEnter: " + other);
    }
}

