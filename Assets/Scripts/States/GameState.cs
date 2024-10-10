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
    [SerializeField] private color[] hardCodedSequence;
    private Stack<color> m_currentSequence = new Stack<color>();
    [SerializeField] private uint remainingSequences = 3;
    public color CurrentColor { get => m_currentColor; set => m_currentColor = value; }

    public delegate void CompleteSequence();
    public event CompleteSequence OnSequenceCompleted;

    // Methods
    public void Init()
    {
        for (int i = hardCodedSequence.Length - 1; i >= 0; i--)
        {
            Debug.Log("pushing: " + hardCodedSequence[i]);
            m_currentSequence.Push(hardCodedSequence[i]);
        }
    }

    public void CreateNewSequence()
    {
        for (int i = hardCodedSequence.Length - 1; i >= 0; i--)
        {
            Debug.Log("pushing: " + hardCodedSequence[i]);
            m_currentSequence.Push(hardCodedSequence[i]);
        }
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

            if(remainingSequences == 0)
                Debug.Log("Condition completed");

            if(OnSequenceCompleted != null)
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
