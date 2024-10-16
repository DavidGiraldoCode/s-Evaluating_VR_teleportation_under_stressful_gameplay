using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
    public static ExperimentManager Instance { get; private set; }
    [SerializeField] private GameState m_gameState;
    // Public
    [SerializeField] private List<Condition> m_conditions = new List<Condition>();
    [Tooltip("Preview only")]
    [SerializeField] public Condition m_currentCondition = null;
    private Stack<Condition> m_fulfilledConditions = new Stack<Condition>(); // Keeps track of the progress in the experiment
    private uint m_totalConditions;
    public Condition CurrentCondition { get => m_currentCondition; }
    public delegate void ConditionHasChanged(Condition newCondition);
    public delegate void ExperimentSate();
    public event ConditionHasChanged OnConditionChanged;
    public event ConditionHasChanged OnConditionTerminated;
    public event ConditionHasChanged OnConditionFulfilled;
    public event ExperimentSate OnExperimentCompleted;
    public event ExperimentSate OnExperimentReset;
    //
    private PlatformStateController[] m_PlatformStates;

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
            throw new System.NullReferenceException("The ExperimentManager is missing the GameState");

        SetupConditions();
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

    #region Experiment Conditions
    /// <summary>
    /// Traverse the list of all conditions and set them to fulfilled = false;
    /// </summary>
    private void SetupConditions()
    {
        m_currentCondition = null;
        m_totalConditions = (uint)m_conditions.Count;
        m_fulfilledConditions.Clear();

        for (int i = 0; i < m_conditions.Count; i++)
        {
            m_conditions[i].IsFulfilled = false;
        }
    }
    /// <summary>
    /// Call this function when to set the current condition
    /// </summary>
    public void SetCondition(Condition newCondition)
    {
        if (m_currentCondition == null || m_currentCondition != newCondition)
            m_currentCondition = newCondition;

        if (OnConditionChanged == null) return; // No method has instantiate this event
        if (OnConditionChanged.GetInvocationList().Length > 0)
            OnConditionChanged?.Invoke(m_currentCondition);
        else
            Debug.Log("No one is listening to this event");

        //Triggers the necesarry events for the classes that need this update
    }

    public void EnterGameplay()
    {
        if (!GameplayManager.Instance) return;
        GameplayManager.Instance.EnterGameplay(m_currentCondition);
    }
    /// <summary>
    /// Call this function when the condition has been forcely terminated.
    /// It exits the gamaplay
    /// </summary>
    public void TerminateCondition()
    {
        if (m_currentCondition == null) return;

        if (GameplayManager.Instance)
            GameplayManager.Instance.ExitGameplay();

        if (OnConditionTerminated != null)
        {
            if (OnConditionTerminated.GetInvocationList().Length > 0)
                OnConditionTerminated?.Invoke(m_currentCondition); // Send what condition was terminated
        }

        m_currentCondition = null;
        if (OnConditionChanged != null) OnConditionChanged?.Invoke(m_currentCondition);
    }
    /// <summary>
    /// Call this function when the task during the current condition has been fulfilled.
    /// It exits the gamaplay
    /// </summary>
    public void FulfillCondition()
    {
        if (m_currentCondition == null) return;

        if (GameplayManager.Instance)
            GameplayManager.Instance.ExitGameplay();

        m_currentCondition.IsFulfilled = true;
        if (OnConditionFulfilled != null)
        {
            if (OnConditionFulfilled.GetInvocationList().Length > 0)
            {
                OnConditionFulfilled?.Invoke(m_currentCondition);
                m_fulfilledConditions.Push(m_currentCondition);
                // Notify everyone that this condition has been fulfilled
            }
        }
        m_currentCondition = null;
        if (OnConditionChanged != null) OnConditionChanged?.Invoke(m_currentCondition);

        if (m_fulfilledConditions.Count == m_totalConditions)
        {
            Debug.Log("Experiment completed!");
            if (OnExperimentCompleted != null)
            {
                if (OnExperimentCompleted.GetInvocationList().Length > 0)
                {
                    OnExperimentCompleted?.Invoke();
                }
            }
        }
    }
    /// <summary>
    /// Resets the experiment with the coonditions
    /// </summary>
    public void ResetExperiment()
    {
        SetupConditions();
        // Reset the gamestate somehow
        if (OnExperimentReset != null)
        {
            if (OnExperimentReset.GetInvocationList().Length > 0)
            {
                OnExperimentReset?.Invoke();
            }
        }
    }

    #endregion Experiment Conditions

    #region Gameloop logic
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
    #endregion Gameloop logic
}
