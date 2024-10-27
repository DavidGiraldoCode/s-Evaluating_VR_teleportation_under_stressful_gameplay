using UnityEngine;

public class PlatformStateController : MonoBehaviour
{
    [SerializeField] private PlatformState m_platformState;
    [SerializeField] private MeshRenderer m_platformMeshR;
    [SerializeField] private GameState m_gameState;
    private ActivationPlatformButton m_activationPlatformButton;
    public PlatformState State { get => m_platformState; }
    private CheatingController m_cheatingController;

    //Unity
    private void Awake()
    {
        if (!m_platformState)
            throw new System.ArgumentNullException("No PlatformState has been assigned");

        if (!m_gameState)
            throw new System.ArgumentNullException("The GameState is missing");

        //TODO Change the large button to be the start of each task
        //m_activationPlatformButton = GetComponentInChildren<ActivationPlatformButton>();
        //m_activationPlatformButton.PlatformStateRef = m_platformState;

        m_cheatingController = GetComponentInChildren<CheatingController>();
        m_cheatingController.CurrentPlatformState = m_platformState;
    }
    private void Start()
    {
        //Debug.Log(" PlatformStateController Start()");
        m_platformMeshR.material.color = m_platformState.DisplayColor;
        //(Color)GameState.ReadableColorToRGB[(GameState.color)m_platformState.DesignatedColor];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        m_platformState.ChangeState(PlatformState.state.FOCUSSED);

        //Debug.Log(m_platformState.DesignatedColor);
        //Debug.Log(m_gameState.CurrentTaskColor());


        if (m_platformState.DesignatedColor == (PlatformState.color)m_gameState.CurrentTaskColor())
        {
            m_platformState.AvitationAllowed = true;
            //Debug.Log("m_platformState.AvitationAllowed: " + m_platformState.AvitationAllowed);
        }
        else
        {
            //Debug.Log("m_platformState Avitation NOT Allowed: " + m_platformState.AvitationAllowed);
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        m_platformState.ChangeState(PlatformState.state.IDLE);
        m_platformState.AvitationAllowed = false;
    }
}
