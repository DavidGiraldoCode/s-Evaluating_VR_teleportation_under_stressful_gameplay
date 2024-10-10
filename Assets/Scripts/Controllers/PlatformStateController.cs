using UnityEngine;

public class PlatformStateController : MonoBehaviour
{
    [SerializeField] private PlatformState m_platformState;
    [SerializeField] private MeshRenderer m_platformMeshR;
    public PlatformState State { get => m_platformState; }

    //Unity
    private void Awake()
    {
        if (!m_platformState)
            throw new System.ArgumentNullException("No PlatformState has been assigned");
    }
    private void Start()
    {
         m_platformMeshR.material.color = m_platformState.DisplayColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        m_platformState.ChangeState(PlatformState.state.FOCUSSED);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        m_platformState.ChangeState(PlatformState.state.IDLE);
    }
}
