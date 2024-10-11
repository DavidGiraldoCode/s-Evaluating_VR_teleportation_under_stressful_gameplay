using UnityEngine;

public class PlatformStateController : MonoBehaviour
{
    [SerializeField] private PlatformState m_platformState;
    [SerializeField] private MeshRenderer m_platformMeshR;
    private ActivationPlatformButton m_activationPlatformButton;
    public PlatformState State { get => m_platformState; }

    //Unity
    private void Awake()
    {
        if (!m_platformState)
            throw new System.ArgumentNullException("No PlatformState has been assigned");

        m_activationPlatformButton = GetComponentInChildren<ActivationPlatformButton>();
        m_activationPlatformButton.PlatformStateRef = m_platformState;
    }
    private void Start()
    {
        //Debug.Log(" PlatformStateController Start()");
        m_platformMeshR.material.color = (Color)GameState.ReadableColorToRGB[(GameState.color)m_platformState.DesignatedColor];
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
        m_platformState.AvitationAllowed = false;
    }
}
