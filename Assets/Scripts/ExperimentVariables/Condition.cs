using UnityEngine;

[CreateAssetMenu(fileName = "Condition", menuName = "Experiment/Condition", order = 0)]
public class Condition : ScriptableObject 
{
    [SerializeField] private bool m_isCompleted = false;
    [SerializeField] private IndependentVariables.Teleportation m_teleportationMethod;
    [SerializeField] private IndependentVariables.CognitiveInterference m_cognitiveInterference;
    [SerializeField] private IndependentVariables.TimeAndEnvironmentalStressor m_TimeAndEnvironmentalStressor;

    // Only get accessors
    public bool IsCompleted { get => m_isCompleted;}
    public IndependentVariables.Teleportation TeleportationMethod { get => m_teleportationMethod;}
    public IndependentVariables.CognitiveInterference CognitiveInterference { get => m_cognitiveInterference;}
    public IndependentVariables.TimeAndEnvironmentalStressor TimeAndEnvironmentalStressor { get => m_TimeAndEnvironmentalStressor;}

}