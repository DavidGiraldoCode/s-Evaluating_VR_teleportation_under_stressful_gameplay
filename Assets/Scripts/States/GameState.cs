using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the state of the game loop that is being performed by the player. 
/// Is the task itself within the conditions. Depending on the current condition the characterisitc of the game change.
/// </summary>

//the colors that were considered for the original Stroop Test: red, green, blue, purple, yellow, and brown [Stroop 1935].
//NT was set to 120
[CreateAssetMenu(fileName = "GameState", menuName = "States/GameState", order = 0)]
public class GameState : ScriptableObject
{
    public enum state
    {
        PRACTICE_STANDBY,
        DOING_PRACTICE,
        PRACTICE_DONE_TRIAL_STANDBY,
        DOING_TRIAL,
        TRIAL_DONE,
    }
    public enum taskColors
    {
        NONE,
        RED,
        GREEN,
        BLUE,
        YELLOW,
        ORANGE,
        PURPLE
    }
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
    public enum stimulus
    {
        WORD,
        COLOR
    }
    [SerializeField] private state m_currentState = state.PRACTICE_STANDBY;
    [SerializeField] private color m_currentColor = color.NONE;
    [SerializeField] private stimulus m_currentStimulus = stimulus.COLOR;
    [SerializeField] public color[] HardCodedSequence; //TODO For testing
    [SerializeField] public color[] HardCodedTrialSequence; //TODO For testing
    private Stack<color> m_currentSequence = new Stack<color>();
    static Color o = new Color(255.0f, 127.0f, 80.0f);
    static Color p = new Color(153.0f, 50.0f, 204.0f);
    private static Hashtable m_colorsTable = new Hashtable()
    {
        { color.NONE, Color.black},
        { color.RED, Color.red},
        { color.GREEN, Color.green},
        { color.BLUE, Color.blue},
        { color.YELLOW, Color.yellow},
        { color.ORANGE, o},
        { color.PURPLE, p},
    };
    public Stack<color> CurrentSequence { get => m_currentSequence; }
    /// <summary>
    /// Table to translate the redable color into RGB
    /// </summary>
    public static Hashtable ReadableColorToRGB { get => m_colorsTable; }
    [SerializeField] private uint remainingSequences = 3;
    public color CurrentColor { get => m_currentColor; set => m_currentColor = value; }
    public stimulus CurrentStimulus { get => m_currentStimulus; set => m_currentStimulus = value; }
    public delegate void CompleteSequence();
    public event CompleteSequence OnSequenceCompleted;
    public delegate void NewSequence(Stack<color> sequence);
    public delegate void NextColor(stimulus newStimulus, color nextColor);
    public event NewSequence OnNewSequence;
    public event NextColor OnNewNextColor;


    #region Gameloop methods

    public void Setup()
    {
        GenerateRandomTasks();
        m_currentState = state.PRACTICE_STANDBY;
    }

    private Stack<taskColors> m_practiceTasks = new Stack<taskColors>();
    private Stack<taskColors> m_trialTasks = new Stack<taskColors>();

    /// <summary>
    /// Returns the top of the stack of task colors depending whether is the practice of the trial
    /// </summary>
    /// <returns>taskColors value</returns>
    public taskColors CurrentTaskColor()
    {
        switch (m_currentState)
        {
            case state.DOING_PRACTICE:
                return m_practiceTasks.Count > 0 ? m_practiceTasks.Peek() : taskColors.NONE;
            case state.DOING_TRIAL:
                return m_trialTasks.Count > 0 ? m_trialTasks.Peek() : taskColors.NONE;
        }
        return taskColors.NONE;
    }

    /// <summary>
    /// Compares if the top of the stack, meaning the current task color is the same
    /// as the given taskColor argument. If so, pops the task and triggers a possitive event. 
    /// If not, trigger a wrong color event.
    /// </summary>
    /// <param name="taskColor"></param>
    public void CompleteTask(taskColors platformColor)
    {
        if (platformColor == CurrentTaskColor())
        {
            RemoveTaskFromStack();
        }
        else
        {
            //Wrong platform
        }

        CheckForTaskComplition();
    }
    /// <summary>
    /// Depending on the state of the gameloop, it check wether the stack of task is empty, 
    /// and trigger the correspoing event
    /// </summary>
    private void CheckForTaskComplition()
    {
        switch (m_currentState)
        {
            case state.DOING_PRACTICE:
                // OnPracticeTaskCompleted()
                break;
            case state.DOING_TRIAL:
                // OnTrialTaskCompleted()
                break;
        }
    }

    /// <summary>
    /// Removes the task at the top of the stack of task in the current state, practice or trial
    /// </summary>
    private void RemoveTaskFromStack()
    {
        if (m_currentState == state.DOING_PRACTICE && m_practiceTasks.Count > 0)
            m_practiceTasks.Pop();
        if (m_currentState == state.DOING_TRIAL && m_trialTasks.Count > 0)
            m_trialTasks.Pop();
    }

    private void GenerateRandomTasks()
    {
        for (int i = HardCodedTrialSequence.Length - 1; i >= 0; i--)
        {
            m_trialTasks.Push((taskColors)HardCodedTrialSequence[i]);
        }
        for (int i = HardCodedSequence.Length - 1; i >= 0; i--)
        {
            m_trialTasks.Push((taskColors)HardCodedSequence[i]);
        }
        Debug.Log("All task stacks are ready");
    }

    #endregion Gameloop methods
    #region Gameloop events
    #endregion Gameloop events

    #region Legacy
    // Methods
    public void Init()
    {
        CreateNewSequence();
    }

    public void CreateNewSequence()
    {
        for (int i = HardCodedSequence.Length - 1; i >= 0; i--)
        {
            //Debug.Log("pushing: " + HardCodedSequence[i]);
            m_currentSequence.Push(HardCodedSequence[i]);
        }

        if (OnNewSequence != null)
            OnNewSequence?.Invoke(m_currentSequence);

        if (OnNewNextColor != null)
            OnNewNextColor?.Invoke(m_currentStimulus, m_currentSequence.Peek());
    }
    public void ProgressInSequence(color platformColor)
    {
        if (m_currentSequence.Count == 0) return;

        if (platformColor == m_currentSequence.Peek())
        {
            Debug.Log("Removing " + m_currentSequence.Peek());
            m_currentSequence.Pop();
        }
        else
        {
            Debug.Log("Wrong color");
        }

        if (m_currentSequence.Count == 0)
        {
            Debug.Log("Sequence completed");
            remainingSequences--;

            if (remainingSequences == 0)
                Debug.Log("Condition completed");

            if (OnSequenceCompleted != null)
                OnSequenceCompleted?.Invoke();
        }
        else
        {
            if (OnNewNextColor != null)
                OnNewNextColor?.Invoke(m_currentStimulus, m_currentSequence.Peek());
        }
    }
    /*
OnWrongColor()
OnRightColor()
OnSequenceCompleted()
{
   remainingSequences--;
}
OnRoundsWithinConditionCompleted()
*/
    #endregion Legacy
}
