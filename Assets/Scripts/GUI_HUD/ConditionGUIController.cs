using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the variables involve in the test before the tasks start.
/// </summary>
public class ConditionGUIController : MonoBehaviour
{
    [SerializeField] private GameObject m_enterGameplayButton;
    [SerializeField] private GameObject m_conditionGUIMenu;
    [SerializeField] private ParticipantData m_participantData;
    [SerializeField] private TMP_Text m_participantID;

    [SerializeField] private GameObject m_teleportToggle;
    [SerializeField] private GameObject m_timeToggle;
    [SerializeField] private GameObject m_biasedToggle;
    [SerializeField] private GameObject m_attentionToggle;

    private void Awake()
    {
        //* This is the original proposal, where the conditions were pre-defined
        // if (ExperimentManager.Instance)
        //     m_enterGameplayButton.SetActive(ExperimentManager.Instance.CurrentCondition != null);
        if (!m_participantData)
            throw new NullReferenceException("The ParticipantData SO is missing");

        SetParticipantID();
        m_participantID.text = m_participantData.ID;

        // Toggle tt = m_teleportToggle.GetComponentInChildren<Toggle>();
        // tt.isOn = m_participantData.TeleportationMethod;

        // Toggle mt = m_teleportToggle.GetComponentInChildren<Toggle>();
        // mt.isOn = m_participantData.GameStressorTime;

        // Toggle bt = m_teleportToggle.GetComponentInChildren<Toggle>();
        // bt.isOn = m_participantData.GameStressorBiasedInstruction;

        // Toggle at = m_teleportToggle.GetComponentInChildren<Toggle>();
        // at.isOn = m_participantData.GameStressorAttentionDemand;
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

    /// <summary>
    /// The ID is set everty time the app starts. Closing the app resets the ID.
    /// </summary>
    public void SetParticipantID()
    {
        // Define the start of the year 2000
        DateTime year2000 = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Get the current time in UTC
        DateTime now = DateTime.UtcNow;

        // Calculate the difference as a TimeSpan
        TimeSpan timeSince2000 = now - year2000;

        // Get the total minutes since the year 2000
        int totalMinutesSince2000 = (int)timeSince2000.TotalMinutes;

        string number = totalMinutesSince2000.ToString();
        int length = number.Length;

        // Adding a '-' to help readability of the ID for the participant when telling the examiner
        StringBuilder formattedNumber = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            formattedNumber.Append(number[i]);

            // Add '-' every three characters except at the end
            if (i % 3 == 1 && i < length - 1)
                formattedNumber.Append('-');
        }

        // Construct the participant ID
        m_participantData.ID = $"P{formattedNumber}";
    }

    public void SetTeleportationMethod()
    {
        bool current = m_participantData.TeleportationMethod;
        m_participantData.TeleportationMethod = current == false ? true : false;
    }

    public void SetGSTime()
    {
        bool current = m_participantData.GameStressorTime;
        m_participantData.GameStressorTime = current == false ? true : false;
    }
    public void SetGSBiasedInstruction()
    {
        bool current = m_participantData.GameStressorBiasedInstruction;
        m_participantData.GameStressorBiasedInstruction = current == false ? true : false;
    }
    public void SetGSAttentionDemand()
    {
        bool current = m_participantData.GameStressorAttentionDemand;
        m_participantData.GameStressorAttentionDemand = current == false ? true : false;
    }


    #endregion Setting Participant Data

}
