using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstanceManager : MonoBehaviour
{
    public static GameInstanceManager Instance { get; private set; }
    [SerializeField] private List<Condition> m_conditions = new List<Condition>();
    [Tooltip("Preview only")]
    [SerializeField] public Condition m_CurrentCondition = null;
    [SerializeField] private GameState m_gameState;
    private PlatformStateController[] m_PlatformStates;
    //private Hashtable colorsTable = new Hashtable();

    // Public
    public Condition CurrentCondition { get => m_CurrentCondition; }

    public delegate void ConditionHasChanged(Condition newCondition);
    public event ConditionHasChanged OnConditionChanged;

    #region MonoBehaviour
    private void Awake()
    {
        if (!Instance || Instance != this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (!m_gameState)
            throw new System.NullReferenceException("The GameInstance is missing the GameState");

        Debug.Log("Awake GameInstanceManager");
        m_CurrentCondition = null;
        m_gameState.Init();
        InitializedPlatforms();

    }

    private void OnEnable()
    {
        m_gameState.OnSequenceCompleted += OnSequenceCompleted;
        m_PlatformStates = FindObjectsOfType<PlatformStateController>();

        for (int i = 0; i < m_PlatformStates.Length; i++)
        {
            m_PlatformStates[i].State.OnStateChange += OnPlaformStateChange;
        }
    }

    private void OnDisable()
    {
        m_gameState.OnSequenceCompleted -= OnSequenceCompleted;

        for (int i = 0; i < m_PlatformStates.Length; i++)
        {
            m_PlatformStates[i].State.OnStateChange -= OnPlaformStateChange;
        }
    }
    #endregion MonoBehaviour

    #region Custom Methods
    // Experiment
    public void SetCondition(Condition newCondition)
    {
        if (m_CurrentCondition == null || m_CurrentCondition != newCondition)
            m_CurrentCondition = newCondition;

        if(OnConditionChanged == null) return; // No method has instantiate this event
        if (OnConditionChanged.GetInvocationList().Length > 0)
        {
            OnConditionChanged?.Invoke(m_CurrentCondition);
        }
        else
        {
            Debug.Log("No one is listening to this event");
        }

        //Triggers the necesarry events for the classes that need this update
    }
    //
    private void OnPlaformStateChange(PlatformState thisPlatform, PlatformState.state state, PlatformState.color platformColor)
    {
        //Debug.Log("The " + color.ToString() + " platform has changed to: " + state.ToString());
        m_gameState.CurrentColor = (GameState.color)platformColor;

        switch (state)
        {
            case PlatformState.state.FOCUSSED:
                //m_gameState.ProgressInSequence((GameState.color)color);
                if (m_gameState.CurrentColor == m_gameState.CurrentSequence.Peek())
                {
                    Debug.Log("On the right platform, you can activate the platform now");
                    thisPlatform.AvitationAllowed = true;
                }
                else
                {
                    Debug.Log("Wrong color, move to the right platform");
                    thisPlatform.AvitationAllowed = false;
                }
                break;
            case PlatformState.state.ACTIVATED:
                m_gameState.ProgressInSequence((GameState.color)platformColor);
                break;
        }
    }


    private void OnPlatfromActivated(PlatformState.color color) // Calls ProgressInSequence()
    {
        //m_gameState.ProgressInSequence((GameState.color)color);
    }

    private void OnSequenceCompleted()
    {
        // Calls 
        //Debug.Log("Creating NEW sequence");
        Debug.Log("Task completed, condition completed!");
        // m_gameState.CreateNewSequence();
        // TODO ResetStartingPosition();
    }

    private void InitializedPlatforms()
    {
        if (m_PlatformStates == null) return;

        Debug.Log("Plaforms found: " + m_PlatformStates.Length);

        // colorsTable[PlatformState.color.RED] = Color.red;
        // colorsTable[PlatformState.color.BLUE] = Color.blue;
        // colorsTable[PlatformState.color.GREEN] = Color.green;

        for (int i = 0; i < m_PlatformStates.Length; i++)
        {
            m_PlatformStates[i].State.InitializePlatform((Color)GameState.ReadableColorToRGB[m_PlatformStates[i].State.DesignatedColor]);
        }
    }
    #endregion Custom Methods
}
