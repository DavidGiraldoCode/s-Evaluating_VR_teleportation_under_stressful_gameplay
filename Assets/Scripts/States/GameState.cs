using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameStateData
{
    public GameStateData(GameState.state newState)
    {
        _newState = newState;
    }
    private GameState.state _newState;
    public GameState.state NewState { get => _newState; }
}

/// <summary>
/// Holds the state of the game loop that is being performed by the player. 
/// Is the task itself within the conditions. Depending on the current condition the characterisitc of the game change.
/// </summary>

//the colors that were considered for the original Stroop Test: red, green, blue, purple, yellow, and brown [Stroop 1935].
//NT was set to 120
[CreateAssetMenu(fileName = "GameState", menuName = "States/GameState", order = 0)]
public class GameState : ScriptableObject, IObservable<GameStateData>
{
    public enum state
    {
        PRACTICE_STANDBY,
        PRACTICE_ONGOING,
        PRACTICE_ENDED,
        TRIAL_STANDBY,
        TRIAL_ONGOING,
        TRIAL_ENDED,
        GAMEOVER,
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
    // public enum color
    // {
    //     NONE,
    //     RED,
    //     GREEN,
    //     BLUE,
    //     YELLOW,
    //     ORANGE,
    //     PURPLE
    // }
    public enum stimulus
    {
        WORD,
        COLOR
    }
    private Stack<taskColors> m_practiceTasks = new Stack<taskColors>();
    private Stack<taskColors> m_trialTasks = new Stack<taskColors>();
    [SerializeField] private state m_currentState = state.PRACTICE_STANDBY;
    //[SerializeField] private color m_currentColor = color.NONE;
    [SerializeField] private stimulus m_currentStimulus = stimulus.COLOR;
    [SerializeField] public taskColors[] HardCodedPracticeSequence; //TODO For testing
    [SerializeField] public taskColors[] HardCodedTrialSequence; //TODO For testing
    //private Stack<taskColors> m_currentSequence = new Stack<taskColors>();
    static Color o = new Color(255.0f, 127.0f, 80.0f);
    static Color p = new Color(153.0f, 50.0f, 204.0f);
    private static Hashtable m_colorsTable = new Hashtable()
    {
        { taskColors.NONE, Color.black},
        { taskColors.RED, Color.red},
        { taskColors.GREEN, Color.green},
        { taskColors.BLUE, Color.blue},
        { taskColors.YELLOW, Color.yellow},
        { taskColors.ORANGE, new Color(1.0f,0.498f,0.314f)}, // Domain [0,1]
        { taskColors.PURPLE, new Color(0.6f,0.196f,0.8f)},
    };

    //[SerializeField] private uint remainingSequences = 3;
    //public Stack<taskColors> CurrentSequence { get => m_currentSequence; }
    /// <summary>
    /// Table to translate the redable color into RGB
    /// </summary>
    public static Hashtable ReadableColorToRGB { get => m_colorsTable; }
    //public color CurrentColor { get => m_currentColor; set => m_currentColor = value; }
    public stimulus CurrentStimulus { get => m_currentStimulus; set => m_currentStimulus = value; }
    public state CurrentState { get => m_currentState; private set => m_currentState = value; }
    public delegate void CompleteSequence();
    //public event CompleteSequence OnSequenceCompleted;
    public delegate void NewSequence(Stack<taskColors> sequence);
    public delegate void NextColor(stimulus newStimulus, taskColors nextColor);
    public event NewSequence OnNewSequence;
    public event NextColor OnNewNextColor;

    //========================================================================================================================

    #region Observer
    private List<IObserver<GameStateData>> m_observers; // List of class intrested in listening to this game state
    private class Unsubscriber : IDisposable // An interface that allows subcribers to remove themselves from the list of observers
    {
        private List<IObserver<GameStateData>> _observers;
        private IObserver<GameStateData> _observer;

        public Unsubscriber(List<IObserver<GameStateData>> observers, IObserver<GameStateData> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
    public IDisposable Subscribe(IObserver<GameStateData> observer)
    {
        m_observers.Add(observer);
        return new Unsubscriber(m_observers, observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in m_observers)
        {
            observer.OnNext(new GameStateData(m_currentState));
        }
    }
    /// <summary>
    /// Send the last notification to all observers,traversing backwards to avoid problems when removing observers
    /// </summary>
    private void NotifyObserversForTheLastTime()
    {
        for (int i = m_observers.Count - 1; i >= 0; i--)
        {
            m_observers[i].OnCompleted();
        }
    }

    #endregion Observer

    //========================================================================================================================

    #region Gameloop methods

    public void Setup()
    {
        m_observers = new List<IObserver<GameStateData>>();
        GenerateRandomTasks();
        m_currentState = state.PRACTICE_STANDBY;
    }


    /// <summary>
    /// Returns the top of the stack of task colors depending whether is the practice of the trial
    /// Use it to compare with the platform color
    /// </summary>
    /// <returns>taskColors value</returns>
    public taskColors CurrentTaskColor()
    {
        switch (m_currentState)
        {
            case state.PRACTICE_ONGOING:
                return m_practiceTasks.Count > 0 ? m_practiceTasks.Peek() : taskColors.NONE;
            case state.TRIAL_ONGOING:
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
        //Invariance; it wont let any color be removed if the task state is not ongoing
        if (CurrentState == state.PRACTICE_ONGOING || CurrentState == state.TRIAL_ONGOING)
        {
            if (platformColor == CurrentTaskColor())
            {
                RemoveTaskFromStack();
                //Right platform
                Debug.Log("Right platform, removing " + platformColor.ToString());
            }
            else
            {
                //Wrong platform
                Debug.Log("Wrong platform " + platformColor.ToString());
            }

            CheckForTaskComplition();
        }

    }

    /// <summary>
    /// Terminates the current task Practice or Trials and nitifies the observers
    /// </summary>
    public void CompleteTask()
    {

        switch (m_currentState)
        {
            case state.PRACTICE_ONGOING:
                m_currentState = state.PRACTICE_ENDED;
                NotifyObservers();
                break;
            case state.TRIAL_ONGOING:
                m_currentState = state.TRIAL_ENDED;
                NotifyObservers();
                break;
        }
    }

    /// <summary>
    /// This is call by the counter, 
    /// Once a task has ended, set the current state to the corresponding standby state and notifiy observers
    /// If the previous state was practice, goes to trial. If it was trial, goes to gameover.
    /// </summary>
    public void GoToStandbyOrGameOver()
    {
        switch (m_currentState)
        {
            case state.PRACTICE_ENDED:
                m_currentState = state.TRIAL_STANDBY;
                NotifyObservers();
                break;
            case state.TRIAL_ENDED:
                m_currentState = state.GAMEOVER;
                NotifyObserversForTheLastTime();
                break;
        }

    }

    /// <summary>
    /// Depending on the state of the gameloop, it check wether the stack of task is empty, 
    /// and trigger the correspoing event
    /// </summary>
    private void CheckForTaskComplition()
    {
        switch (m_currentState)
        {
            case state.PRACTICE_ONGOING:
                if (m_practiceTasks.Count > 0) break;
                m_currentState = state.PRACTICE_ENDED;
                NotifyObservers();
                //OnPracticeTaskCompleted
                break;
            case state.TRIAL_ONGOING:
                if (m_trialTasks.Count > 0) break;
                m_currentState = state.TRIAL_ENDED;
                NotifyObservers();
                // OnTrialTaskCompleted()
                break;
        }
    }

    /// <summary>
    /// Removes the task at the top of the stack of task in the current state, practice or trial
    /// </summary>
    private void RemoveTaskFromStack()
    {
        if (m_currentState == state.PRACTICE_ONGOING && m_practiceTasks.Count > 0)
        {
            m_practiceTasks.Pop();

            if (OnNewNextColor != null)
                OnNewNextColor?.Invoke(m_currentStimulus, CurrentTaskColor());
        }
        else if (m_currentState == state.TRIAL_ONGOING && m_trialTasks.Count > 0)
        {
            m_trialTasks.Pop();
            if (OnNewNextColor != null)
                OnNewNextColor?.Invoke(m_currentStimulus, CurrentTaskColor());
        }

    }

    private void GenerateRandomTasks()
    {
        for (int i = HardCodedPracticeSequence.Length - 1; i >= 0; i--)
        {
            m_practiceTasks.Push(HardCodedPracticeSequence[i]);
        }
        for (int i = HardCodedTrialSequence.Length - 1; i >= 0; i--)
        {
            m_trialTasks.Push(HardCodedTrialSequence[i]);
        }
        Debug.Log("All task stacks are ready");
    }

    #endregion Gameloop methods
    #region Event Listeners
    public void OnPracticeBegin()
    {
        m_currentState = state.PRACTICE_ONGOING;
        if (OnNewNextColor != null)
            OnNewNextColor?.Invoke(m_currentStimulus, CurrentTaskColor());
    }
    public void OnTrialBegin()
    {
        Debug.Log("OnTrialBegin");
        m_currentState = state.TRIAL_ONGOING;
        if (OnNewNextColor != null)
            OnNewNextColor?.Invoke(m_currentStimulus, CurrentTaskColor());
    }
    #endregion Event Listeners


    #region Legacy ............................
    // Methods
    /*public void Init()
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

        if (OnNewNextColor != null) ;
        //OnNewNextColor?.Invoke(m_currentStimulus, m_currentSequence.Peek());
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
            if (OnNewNextColor != null) ;
            //OnNewNextColor?.Invoke(m_currentStimulus, m_currentSequence.Peek());
        }
    }*/

    #endregion Legacy
    /*
OnWrongColor()
OnRightColor()
OnSequenceCompleted()
{
remainingSequences--;
}
OnRoundsWithinConditionCompleted()
*/
}
