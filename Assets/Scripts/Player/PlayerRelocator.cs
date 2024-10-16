using System;
using UnityEngine;
/// <summary>
/// Listens to the gameplay states and relocates the player in the middle of the environment
/// </summary>
public class PlayerRelocator : MonoBehaviour
{
    [SerializeField] private Transform m_player;

    private void Awake()
    {
        if (!GameplayManager.Instance)
            throw new System.NullReferenceException("The GameplayManager is missing in the scene");
        if (!m_player)
            throw new System.NullReferenceException("No Player has been asgined to be realocated");
    }
    private void OnEnable()
    {
        //SubribeFromConditionEvents();
        SubribeToGameplayEvents();
    }
    private void OnDisable()
    {
        //UnsubribeFromConditionEvents();
        SubribeToGameplayEvents();
    }

    private void OnTasksReset()
    {
        m_player.position = transform.position;
    }
    private void SubribeToGameplayEvents()
    {
        if (GameplayManager.Instance)
        {
            GameplayManager.OnPracticeStandby += OnTasksReset;
            GameplayManager.OnTrialStandby += OnTasksReset;
        }
    }

    private void UnsubribeToGameplayEvents()
    {
        if (GameplayManager.Instance)
        {
            GameplayManager.OnPracticeStandby -= OnTasksReset;
            GameplayManager.OnTrialStandby -= OnTasksReset;
        }
    }

    private void PlayerRelocationOnConditionChange(Condition newCondition)
    {
        m_player.position = transform.position;
    }
    private void SubribeFromConditionEvents()
    {
        if (ExperimentManager.Instance)
        {
            ExperimentManager.Instance.OnConditionChanged += PlayerRelocationOnConditionChange;
            ExperimentManager.Instance.OnConditionTerminated += PlayerRelocationOnConditionChange;
            ExperimentManager.Instance.OnConditionFulfilled += PlayerRelocationOnConditionChange;
        }
    }

    private void UnsubribeFromConditionEvents()
    {
        if (ExperimentManager.Instance)
        {
            ExperimentManager.Instance.OnConditionChanged -= PlayerRelocationOnConditionChange;
            ExperimentManager.Instance.OnConditionTerminated -= PlayerRelocationOnConditionChange;
            ExperimentManager.Instance.OnConditionFulfilled -= PlayerRelocationOnConditionChange;
        }
    }
}