using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatformState", menuName = "States/PlatformState")]
public class PlatformState : ScriptableObject
{
    public enum state
    {
        IDLE,       //  No player on the platform
        FOCUSSED,   //  Player steped on the platform
        ACTIVATED,  // Player activated the mechanism
    };
    public enum color
    {
        RED,
        GREEN,
        BLUE
    }

    [SerializeField] private state m_currentState;
    [SerializeField] private color m_currentColor;
    [SerializeField] private Color m_color;
    public Color GetColor { get => m_color; set => m_color = value; }
    public color CurrentColor { get => m_currentColor;}

    // Methods

    // Events

    public delegate void StateChanger(state state, color color);
    public event StateChanger OnStateChange;

    public void ChangeState(state state)
    {
        if (OnStateChange == null) return;
        m_currentState = state;
        OnStateChange?.Invoke(m_currentState, m_currentColor);
    }

    // User feedback, but determines if the precission task can be performed or not.

    // Triggers
    public void WrongColor()
    {
        //OnStateChange?.Invoke(m_currentState);
    }
    public void RightColor()
    {

    }
}