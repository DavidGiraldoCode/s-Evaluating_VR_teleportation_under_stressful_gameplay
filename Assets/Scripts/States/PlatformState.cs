using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatformState", menuName = "States/PlatformState")]
public class PlatformState : ScriptableObject
{
    public enum state
    {
        IDLE,       //  No player on the platform
        FOCUSSED,   //  Player steped on the platform
        ACTIVATED,  // Player activated the mechanism
    };
    public enum color
    {
        NONE,
        RED,
        GREEN,
        BLUE,
        YELLOW,
        ORANGE,
        PURPLE
    }

    [SerializeField] private state m_currentState = state.IDLE;
    [SerializeField] private color m_designatedColor; // designate the value in the Editor
    [SerializeField] private Color m_displayColor; // Asign the visual representation of the color in the editor
    [SerializeField] private bool m_activationAllowed = false;
    public Color DisplayColor { get => m_displayColor; set => m_displayColor = value; }
    public color DesignatedColor { get => m_designatedColor; }
    public state CurrentState { get => m_currentState; }
    public bool AvitationAllowed { get => m_activationAllowed; set => m_activationAllowed = value; }
    // Methods
    public void InitializePlatform()
    {
        m_activationAllowed = false;
        m_currentState = state.IDLE;
        //m_displayColor = displayColor;
    }
    // Events

    public delegate void StateChanger(PlatformState thisPlatform, state state, color color);
    /// <summary>
    /// Signals everytime the platform changes state IDLE, FOCUSSED, ACTIVATED
    /// </summary>
    public event StateChanger OnStateChange;

    public void ChangeState(state state)
    {
        if (OnStateChange == null) return;
        m_currentState = state;
        OnStateChange?.Invoke(this, m_currentState, m_designatedColor);
    }

    // User feedback, but determines if the precission task can be performed or not.
    // Triggers
    public void WrongColor()
    {
        //OnStateChange?.Invoke(m_currentState);
    }
    public void RightColor()
    {

    }
}