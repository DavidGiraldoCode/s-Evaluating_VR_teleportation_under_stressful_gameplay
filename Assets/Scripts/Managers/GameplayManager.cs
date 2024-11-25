using System;
using UnityEngine;
/// <summary>
/// Manages the actual execution of the gameloop that represent the sequence of task the player will performed.
/// It only concerns with the condition at hand. Triggers the events related to the practice and trial tasks.
/// </summary>
public class GameplayManager : MonoBehaviour, IObserver<GameStateData>
{
    public static GameplayManager Instance { get; private set; }

    [SerializeField] private GameState m_gameState;
    //TODO Adding Gaph
    [Tooltip("This represents the abstract Data Structure of the plaforms" +
             " distribution and the colors to prompt the user.")]
    //[SerializeField] private CycleGraph m_cycleGraph;
    // Observer
    private IDisposable unsubscriber;

    // Gameloop Events
    public delegate void GameplayStateChanges();
    public static event GameplayStateChanges OnPracticeStandby;     // The player is waiting to manually start the practice
    public static event GameplayStateChanges OnPracticeBegin;       // The player performs the practice tasks
    public static event GameplayStateChanges OnPracticeEnd;       // The player performs the practice tasks
    public static event GameplayStateChanges OnPracticeEndAndTrialStandby; // The player completes all the practice task and can manually start the trial
    public static event GameplayStateChanges OnTrialStandby;     // The player is waiting to manually start the practice
    public static event GameplayStateChanges OnTrialBegin;          // The player performs the trial tasks 
    public static event GameplayStateChanges OnTrialEnd;            // The player completes all the trial task
    public static event GameplayStateChanges OnGameOver;

    // Variables
    const uint STARTING_PLATFORM = 0;
    private PlatformStateController[] m_PlatformStates; // Holds all the platforms in the scene to then subscribe to their events
    [Tooltip("The Manager Finds it automatically")]
    [SerializeField] private PlayerController m_playerController; // The ref to the player to enable teleportation

    #region MonoMonoBehaviour
    private void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            //Debug.Log("Duplicated removed");
        }
        if (m_gameState == null)
            throw new System.NullReferenceException("GameState missing");

        // Graph DEPRECATED
        //if (m_cycleGraph == null)
        //    throw new System.NullReferenceException("CycleGraph missing");

        m_PlatformStates = FindObjectsOfType<PlatformStateController>();

        m_playerController = FindObjectOfType<PlayerController>();
        if (m_playerController == null)
            Debug.LogError("There is no PlayerController in the scene");

        m_playerController.CanTeleport = false;

    }
    private void OnEnable()
    {

        //m_cycleGraph.BuildGraph(6);
        //m_gameState.TestingCoordinates();
        OnPracticeBegin += m_gameState.OnPracticeBegin;
        OnTrialBegin += m_gameState.OnTrialBegin;

        //Subscribe to platform events
        if (m_PlatformStates != null)
            for (int i = 0; i < m_PlatformStates.Length; i++)
            {
                m_PlatformStates[i].State.OnStateChange += OnPlaformStateChange;
            }
    }
    private void OnDisable()
    {
        OnPracticeBegin -= m_gameState.OnPracticeBegin;
        OnTrialBegin -= m_gameState.OnTrialBegin;

        //Unsubscribe to platform events
        if (m_PlatformStates != null)
            for (int i = 0; i < m_PlatformStates.Length; i++)
            {
                m_PlatformStates[i].State.OnStateChange -= OnPlaformStateChange;
            }
    }
    private void Start()
    {
        //EnterGameplay(null); //TODO for testing, REMOVE
        //BeginGame();
    }
    #endregion MonoMonoBehaviour

    #region Enter & Exit Gameplay
    /// <summary>
    /// Starts the "game" accordingly with the condition
    /// </summary>
    /// <param name="experimentCondition"> This define the type of teleportation that get activated and all the other stimulus </param>
    public void EnterGameplay(Condition experimentCondition)
    {
        m_gameState.Setup(STARTING_PLATFORM);
        ResetPlatform();
        unsubscriber = m_gameState.Subscribe(this);
        // Setup teleporation
        // Setup HUD prompts system with congnitive interference
        // Setup time and environmental stressors
        ReturnToStandby();
    }

    /// <summary>
    /// Shotsdown the "game", gets called form the ExperimentInstanceManager
    /// </summary>
    public void ExitGameplay()
    {
        // Save the necesarry information to the server or file
        Debug.Log("Exiting Game, returning to conditions");

        if (unsubscriber != null)
            unsubscriber.Dispose(); // Unsubscribe from observing the GameState

        //ReturnToStandby();
        OnGameOver?.Invoke();
    }
    #endregion Enter & Exit Gameplay

    #region Gameloop methods ........
    private void OnPlaformStateChange(PlatformState thisPlatform, PlatformState.state state, PlatformState.color color)
    {
        if (state == PlatformState.state.ACTIVATED)
            m_gameState.CompleteTask((GameState.taskColors)color);
    }

    #endregion Gameloop methods

    #region Task Event Trigger ........
    private void ReturnToStandby()
    {
        if (OnPracticeStandby != null)
        {
            if (OnPracticeStandby.GetInvocationList().Length > 0)
            {
                OnPracticeStandby?.Invoke();
            }
        }
    }
    /// <summary>
    /// Resets the plaforms states to Idle and the AvitationAllowed to false
    /// </summary>
    private void ResetPlatform()
    {
        for (int i = 0; i < m_PlatformStates.Length; i++)
        {
            m_PlatformStates[i].State.InitializePlatform();
        }
    }
    /// <summary>
    /// Starts the tasks depending on the previos standby state
    /// </summary>
    public void BeginGame()
    {
        switch (m_gameState.CurrentState)
        {
            case GameState.state.PRACTICE_STANDBY:
                OnPracticeBegin?.Invoke();
                break;
            case GameState.state.TRIAL_STANDBY:
                OnTrialBegin?.Invoke();
                break;
        }
        m_playerController.CanTeleport = true;
    }
    // public void BeginPractice()
    // {
    //     Debug.Log("XXXXXX BeginPractice XXXXXX");
    //     OnPracticeBegin?.Invoke();
    //     Debug.Log(m_playerController.CanTeleport);
    //     m_playerController.CanTeleport = true;
    //     Debug.Log(m_playerController.CanTeleport);
    // }
    // public void BeginTrial()
    // {
    //     OnTrialBegin?.Invoke();
    //     Debug.Log(m_playerController.CanTeleport);
    //     m_playerController.CanTeleport = true;
    //     Debug.Log(m_playerController.CanTeleport);
    // }
    #endregion Task Event Trigger

    #region Observer pattern
    public void OnCompleted()
    {
        if (ExperimentManager.Instance)
            ExperimentManager.Instance.FulfillCondition();

        ExitGameplay();
    }

    public void OnError(System.Exception error)
    {
        throw new System.NotImplementedException();
    }

    public void OnNext(GameStateData value)
    {
        //Debug.Log(value.NewState);
        //Debug.Log("State changed: ");

        switch (value.NewState)
        {
            case GameState.state.PRACTICE_ENDED:
                OnPracticeEnd?.Invoke();
                m_playerController.CanTeleport = false;
                break;
            case GameState.state.TRIAL_STANDBY:
                OnTrialStandby?.Invoke();
                break;
            case GameState.state.TRIAL_ENDED:
                m_playerController.CanTeleport = false;
                OnTrialEnd?.Invoke();
                break;
        }

    }

    #endregion Observer pattern
}
