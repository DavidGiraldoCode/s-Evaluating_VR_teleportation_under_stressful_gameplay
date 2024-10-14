using UnityEngine;
/// <summary>
/// Manager the actual execution of the gameloop that represent the sequence of task the player will performed.
/// It only consernce with the condition at hand. Triggers the events related to the practice and trial tasks.
/// </summary>
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }
    [SerializeField] private GameState m_gameState;
    public delegate void GameplayStateChanges();
    public static event GameplayStateChanges OnPracticeStandby;     // The player is waiting to manually start the practice
    public static event GameplayStateChanges OnPracticeBegin;       // The player performs the practice tasks 
    public static event GameplayStateChanges OnPracticeEndAndTrialStandby; // The player completes all the practice task and can manually start the trial
    public static event GameplayStateChanges OnTrialBegin;          // The player performs the trial tasks 
    public static event GameplayStateChanges OnTrialEnd;            // The player completes all the trial task
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
    //TODO for testing
    private void Start() 
    {
        EnterGameplay(null);    
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
    }
    #endregion Gameplay
}
