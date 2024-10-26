using UnityEngine;

public class BuzzWireTouches : MonoBehaviour
{
    [SerializeField] private BuzzWireTouchesCounter _buzzWireTouchesCounter;
    private MeshRenderer _mesh;

    private void OnEnable()
    {
        _mesh = GetComponent<MeshRenderer>();
        _mesh.material.color = Color.white;
    }

    public void SetColor(Color newColor)
    {

        _mesh.material.color = newColor;
    }
    // private void OnTriggerEnter(Collider other)
    // {

    //     if (other.gameObject.TryGetComponent<BuzzWireTrigger>(out BuzzWireTrigger buzzWireTrigger))
    //     {
    //         Debug.Log("XXX " + buzzWireTrigger.CurrentPlatformState);
    //         _buzzWireTouchesCounter.currentPlatfrom = buzzWireTrigger.CurrentPlatformState;
    //         _buzzWireTouchesCounter.touchesCount++;
    //         _mesh.material.color = Color.green;
    //     }
    // }
    // private void OnTriggerExit(Collider other)
    // {

    //     if (other.gameObject.TryGetComponent<BuzzWireTrigger>(out BuzzWireTrigger buzzWireTrigger))
    //     {
    //         _mesh.material.color = Color.white;
    //     }
    // }
}
