using System.Collections.Generic;
using UnityEngine;

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
    public color CurrentColor { get => m_currentColor; set => m_currentColor = value; }

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
        }
    }
    /*
    OnWrongColor()
    OnRightColor()
    OnSequenceCompleted()
    */
}
