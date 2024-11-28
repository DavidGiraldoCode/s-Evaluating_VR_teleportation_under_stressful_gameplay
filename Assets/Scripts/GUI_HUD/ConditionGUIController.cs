using System;
using UnityEngine;

/// <summary>
/// Controls the variables involve in the test before the tasks start.
/// </summary>
public class ConditionGUIController : MonoBehaviour
{
    [SerializeField] private GameObject m_enterGameplayButton;
    [SerializeField] private GameObject m_conditionGUIMenu;
    [SerializeField] private ParticipantData m_participantData;

    private void Awake()
    {
        //* This is the original proposal, where the conditions were pre-defined
        // if (ExperimentManager.Instance)
        //     m_enterGameplayButton.SetActive(ExperimentManager.Instance.CurrentCondition != null);
        if(!m_participantData)
            throw new NullReferenceException("The ParticipantData SO is missing");

        SetParticipantID();
    }

    private void OnEnable()
    {
        if (ExperimentManager.Instance)
        {
            ExperimentManager.Instance.OnConditionChanged += OnConditionChanged;
            ExperimentManager.Instance.OnConditionFulfilled += OnGameplayEnd;
            ExperimentManager.Instance.OnConditionTerminated += OnGameplayEnd;
        }
        if (GameplayManager.Instance)
        {
            GameplayManager.OnPracticeStandby += OnGameplayBegin;
        }

    }


    private void OnDisable()
    {
        if (ExperimentManager.Instance)
        {
            ExperimentManager.Instance.OnConditionChanged -= OnConditionChanged;
            ExperimentManager.Instance.OnConditionFulfilled -= OnGameplayEnd;
            ExperimentManager.Instance.OnConditionTerminated -= OnGameplayEnd;
        }
        if (GameplayManager.Instance)
        {
            GameplayManager.OnPracticeStandby -= OnGameplayBegin;
        }
    }
    private void OnConditionChanged(Condition newCondition)
    {
        m_enterGameplayButton.SetActive(newCondition != null);
    }
    private void OnGameplayBegin()
    {
        m_conditionGUIMenu.SetActive(false);
    }

    private void OnGameplayEnd(Condition newCondition)
    {
        m_conditionGUIMenu.SetActive(true);
    }

    #region Setting Participant Data

    public void SetParticipantID()
    {
        // Define the start of the year 2000
        DateTime year2000 = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Get the current time in UTC
        DateTime now = DateTime.UtcNow;

        // Calculate the difference as a TimeSpan
        TimeSpan timeSince2000 = now - year2000;

        // Get the total minutes since the year 2000
        double totalMinutesSince2000 = timeSince2000.TotalMinutes;

        m_participantData.ID = $"P{totalMinutesSince2000}";
    }

    public void SetTeleportationMethod()
    {
        bool current = m_participantData.TeleportationMethod;
        m_participantData.TeleportationMethod = current == false ? true : false ;
    }

    public void SetGSTime()
    {
        bool current = m_participantData.GameStressorTime;
        m_participantData.GameStressorTime = current == false ? true : false ;
    }
    public void SetGSBiasedInstruction()
    {
        bool current = m_participantData.GameStressorBiasedInstruction;
        m_participantData.GameStressorBiasedInstruction = current == false ? true : false ;
    }
    public void SetGSAttentionDemand()
    {
        bool current = m_participantData.GameStressorAttentionDemand;
        m_participantData.GameStressorAttentionDemand = current == false ? true : false ;
    }


    #endregion Setting Participant Data

}
