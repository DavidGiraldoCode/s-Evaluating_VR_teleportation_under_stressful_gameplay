using UnityEngine;

public class PlatformStateController : MonoBehaviour
{
    [SerializeField] private PlatformState m_platformState;
    [SerializeField] private MeshRenderer m_platformMeshR;
    [SerializeField] private GameState m_gameState;
    /// <summary>
    /// This component is connected to the SnapInteractable component, which defines the end location 
    /// of the buzz-wire game. It receives the PlatformState from this context and uses it to check 
    /// if the player is allowed to activate the platform.
    /// </summary>
    private BuzzWirePlatformActivation m_buzzWirePlatformActivation;
    public PlatformState State { get => m_platformState; }
    private CheatingController m_cheatingController;

    //Unity
    private void Awake()
    {
        if (!m_platformState)
            throw new System.ArgumentNullException("No PlatformState has been assigned");

        if (!m_gameState)
            throw new System.ArgumentNullException("The GameState is missing");

        InitializeBuzzWireComponents();

    }
    private void Start()
    {
        //Debug.Log(" PlatformStateController Start()");
        m_platformMeshR.material.color = m_platformState.DisplayColor;
        //(Color)GameState.ReadableColorToRGB[(GameState.color)m_platformState.DesignatedColor];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return; // safeguard in case any other collider enters

        m_platformState.ChangeState(PlatformState.state.FOCUSSED);

        if (m_platformState.DesignatedColor == (PlatformState.color)m_gameState.CurrentTaskColor())
        {
            m_platformState.ActivationAllowed = true;
            //Debug.Log("YYY Player on m_platformState.AvitationAllowed: " + m_platformState.ActivationAllowed);
        }
        else
        {
            //Debug.Log("YYY Player on m_platformState Avitation NOT Allowed: " + m_platformState.ActivationAllowed);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        m_platformState.ChangeState(PlatformState.state.IDLE);
        m_platformState.ActivationAllowed = false;

        //Debug.Log("YYY Player Exist platform");
    }

    /// <summary>
    /// Initialize BuzzWire-related components by passing down the PlatformState
    /// </summary>
    private void InitializeBuzzWireComponents()
    {
        m_buzzWirePlatformActivation = GetComponentInChildren<BuzzWirePlatformActivation>();
        m_buzzWirePlatformActivation.PlatformStateRef = m_platformState;

        m_cheatingController = GetComponentInChildren<CheatingController>();
        m_cheatingController.PlatformStateRef = m_platformState;
    }
}
