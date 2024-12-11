using System;
using UnityEngine;
/// <summary>
/// Listens to the gameplay states and relocates the player in the middle of the environment
/// </summary>
public class PlayerRelocator : MonoBehaviour
{
    [SerializeField] private Transform m_player;
    [SerializeField] private Transform m_mainPlatformRelocator;
    [SerializeField] private Transform m_practiceTrialRelocator;

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
        UnsubribeFromGameplayEvents();
    }

    /// <summary>
    /// Places the player on the main platform to configure conditions.
    /// </summary>
    private void OnMainPlatformPositionReset()
    {
        m_player.position = m_mainPlatformRelocator.position;
    }
    /// <summary>
    /// Places the player on the starting platform (RED to begin the test
    /// </summary>
    private void OnTaskPlatformPositionReset()
    {
        m_player.position = m_practiceTrialRelocator.position;
    }
    private void SubribeToGameplayEvents()
    {
        if (GameplayManager.Instance)
        {
            GameplayManager.OnPracticeStandby += OnTaskPlatformPositionReset;
            GameplayManager.OnTrialStandby += OnTaskPlatformPositionReset;
            GameplayManager.OnGameOver += OnMainPlatformPositionReset;
        }
    }

    private void UnsubribeFromGameplayEvents()
    {
        if (GameplayManager.Instance)
        {
            GameplayManager.OnPracticeStandby -= OnTaskPlatformPositionReset;
            GameplayManager.OnTrialStandby -= OnTaskPlatformPositionReset;
            GameplayManager.OnGameOver -= OnMainPlatformPositionReset;
        }
    }

    // private void PlayerRelocationOnConditionChange(Condition newCondition)
    // {
    //     m_player.position = transform.position;
    // }
    // private void SubribeFromConditionEvents()
    // {
    //     if (ExperimentManager.Instance)
    //     {
    //         ExperimentManager.Instance.OnConditionChanged += PlayerRelocationOnConditionChange;
    //         ExperimentManager.Instance.OnConditionTerminated += PlayerRelocationOnConditionChange;
    //         ExperimentManager.Instance.OnConditionFulfilled += PlayerRelocationOnConditionChange;
    //     }
    // }

    // private void UnsubribeFromConditionEvents()
    // {
    //     if (ExperimentManager.Instance)
    //     {
    //         ExperimentManager.Instance.OnConditionChanged -= PlayerRelocationOnConditionChange;
    //         ExperimentManager.Instance.OnConditionTerminated -= PlayerRelocationOnConditionChange;
    //         ExperimentManager.Instance.OnConditionFulfilled -= PlayerRelocationOnConditionChange;
    //     }
    // }
}