using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the data of a condition.
/// And the state to complete the game loop
/// </summary>

[CreateAssetMenu(fileName = "GameState", menuName = "States/GameState", order = 0)]
public class GameState : ScriptableObject
{
    public enum color
    {
        NONE,
        RED,
        GREEN,
        BLUE
    }
    [SerializeField] private color m_currentColor = color.NONE;
    [SerializeField] public color[] HardCodedSequence; // For testing
    private Stack<color> m_currentSequence = new Stack<color>();
    private static Hashtable m_colorsTable = new Hashtable()
    {
        { color.NONE, Color.black},
        { color.RED, Color.red},
        { color.GREEN, Color.green},
        { color.BLUE, Color.blue},
    };
    public Stack<color> CurrentSequence { get => m_currentSequence; }
    public static Hashtable ColorTable { get => m_colorsTable; }
    [SerializeField] private uint remainingSequences = 3;
    public color CurrentColor { get => m_currentColor; set => m_currentColor = value; }

    public delegate void CompleteSequence();
    public event CompleteSequence OnSequenceCompleted;
    public delegate void NewSequence(Stack<color> sequence);
    public event NewSequence OnNewSequence;
    // Methods
    public void Init()
    {
        CreateNewSequence();
    }

    public void CreateNewSequence()
    {
        for (int i = HardCodedSequence.Length - 1; i >= 0; i--)
        {
            Debug.Log("pushing: " + HardCodedSequence[i]);
            m_currentSequence.Push(HardCodedSequence[i]);
        }

        if(OnNewSequence != null)
            OnNewSequence?.Invoke(m_currentSequence);
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
}
