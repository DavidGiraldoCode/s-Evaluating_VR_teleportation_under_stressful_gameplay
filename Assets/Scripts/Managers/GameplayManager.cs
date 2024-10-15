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
    public delegate void GameplayStateChanges();
    
    // Gameloop Events
    public static event GameplayStateChanges OnPracticeStandby;     // The player is waiting to manually start the practice
    public static event GameplayStateChanges OnPracticeBegin;       // The player performs the practice tasks
    public static event GameplayStateChanges OnPracticeEnd;       // The player performs the practice tasks
    public static event GameplayStateChanges OnPracticeEndAndTrialStandby; // The player completes all the practice task and can manually start the trial
    public static event GameplayStateChanges OnTrialBegin;          // The player performs the trial tasks 
    public static event GameplayStateChanges OnTrialEnd;            // The player completes all the trial task
    
    // Observer
    private IDisposable unsubscriber;
    
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
            Debug.Log("Duplicated removed");
        }
        if (m_gameState == null)
            throw new System.NullReferenceException("GameState missing");
    }
    private void OnEnable()
    {
        OnPracticeBegin += m_gameState.OnPracticeBegin;
        OnTrialBegin += m_gameState.OnTrialBegin;
    }
    private void OnDisable()
    {
        OnPracticeBegin -= m_gameState.OnPracticeBegin;
        OnTrialBegin -= m_gameState.OnTrialBegin;
    }
    private void Start()
    {
        EnterGameplay(null); //TODO for testing     
    }
    #endregion MonoMonoBehaviour
    
    #region Gameplay
    /// <summary>
    /// Starts the "game" accordingly with the condition
    /// </summary>
    /// <param name="experimentCondition"> This define the type of teleportation that get activated and all the other stimulus </param>
    public void EnterGameplay(Condition experimentCondition)
    {
        m_gameState.Setup();
        unsubscriber = m_gameState.Subscribe(this);
        // Setup teleporation
        // Setup HUD prompts system with congnitive interference
        // Setup time and environmental stressors

        if (OnPracticeStandby != null)
        {
            if (OnPracticeStandby.GetInvocationList().Length > 0)
            {
                OnPracticeStandby?.Invoke();
            }
        }
    }

    /// <summary>
    /// Shotsdown the "game", gets called form the ExperimentInstanceManager
    /// </summary>
    public void ExitGameplay()
    {
        // Save the necesarry information to the server or file
        unsubscriber.Dispose(); // Unsubscribe from observing the GameState
        Debug.Log("Exiting Game, returning to conditions");
    }
    #endregion Gameplay
    #region Event Trigger
    public void BeginPractice()
    {
        OnPracticeBegin?.Invoke();
    }
    public void BeginTrial()
    {
        OnTrialBegin?.Invoke();
    }
    #endregion Event Trigger
    
    #region Observer pattern
    public void OnCompleted()
    {
        ExitGameplay();
    }

    public void OnError(System.Exception error)
    {
        throw new System.NotImplementedException();
    }

    public void OnNext(GameStateData value)
    {
        Debug.Log(value.NewState);
        Debug.Log("State changed: ");

        switch (value.NewState)
        {
            case GameState.state.PRACTICE_ENDED:
                OnPracticeEnd?.Invoke();
                break;
            case GameState.state.TRIAL_ENDED:
                OnTrialEnd?.Invoke();
                break;
        }

    }

    #endregion Observer pattern
}
