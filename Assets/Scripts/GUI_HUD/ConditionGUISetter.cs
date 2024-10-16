using System;
using UnityEngine;
using UnityEngine.UI;

public class ConditionGUISetter : MonoBehaviour
{
    [SerializeField] private Condition m_condition;
    [SerializeField] private Button m_button;

    private void Awake()
    {
        if (!ExperimentManager.Instance)
            throw new NullReferenceException("The ExperimentManager is missing in the scene");

        m_button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        SubribeToConditionEvents();
        if (ExperimentManager.Instance)
            ExperimentManager.Instance.OnExperimentReset += OnExperimentReset;
    }

    private void OnDisable()
    {
        UnsubribeFromConditionEvents();
        if (ExperimentManager.Instance)
            ExperimentManager.Instance.OnExperimentReset -= OnExperimentReset;
    }

    private void OnConditionChanged(Condition newCondition)
    {
        // If this button's condition is alredy fulfilled, stop.
        if (m_condition.IsFulfilled) return;

        // If the new condition is the same as this button's condition, disable its interaction
        m_button.interactable = (m_condition != newCondition);
    }
    private void OnConditionTerminated(Condition newCondition)
    {
        // If the condition is completed, the button does not need to be interactive
        m_button.interactable = !m_condition.IsFulfilled;
    }
    private void OnConditionFulfilled(Condition theFulfilledCondition)
    {
        
        // If the fulfilled condition is the same as this button's condition, do:
        // 1. disable the button
        m_button.interactable = !(m_condition == theFulfilledCondition);
        // 2. Unsubribed from the events.
        if (m_condition == theFulfilledCondition)
        {
            UnsubribeFromConditionEvents();
            Debug.Log("OnConditionFulfilled");
            Debug.Log("UnsubribeFromConditionEvents");
        }

    }

    private void OnExperimentReset()
    {
        SubribeToConditionEvents();
        m_button.interactable = !m_condition.IsFulfilled;
    }

    public void SetCondition()
    {
        if (!ExperimentManager.Instance) return;
        ExperimentManager.Instance.SetCondition(m_condition);
    }

    private void SubribeToConditionEvents()
    {
        if (ExperimentManager.Instance)
        {
            ExperimentManager.Instance.OnConditionChanged += OnConditionChanged;
            ExperimentManager.Instance.OnConditionTerminated += OnConditionTerminated;
            ExperimentManager.Instance.OnConditionFulfilled += OnConditionFulfilled;
            Debug.Log("SubribeToConditionEvents");
        }
    }
    private void UnsubribeFromConditionEvents()
    {
        if (ExperimentManager.Instance)
        {
            ExperimentManager.Instance.OnConditionChanged -= OnConditionChanged;
            ExperimentManager.Instance.OnConditionTerminated -= OnConditionTerminated;
            ExperimentManager.Instance.OnConditionFulfilled -= OnConditionFulfilled;
        }
    }
}
