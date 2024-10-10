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
    public color CurrentColor { get => m_currentColor; set => m_currentColor = value; }
}
