using System;
using UnityEngine;

public class ConditionGUIController : MonoBehaviour
{
    [SerializeField] private GameObject m_enterGameplayButton;
    private void Awake()
    {
        if (ExperimentManager.Instance)
            m_enterGameplayButton.SetActive(ExperimentManager.Instance.CurrentCondition != null);
    }

    private void OnEnable()
    {
        if (ExperimentManager.Instance)
        {
            ExperimentManager.Instance.OnConditionChanged += OnConditionChanged;
        }

    }

    private void OnDisable()
    {
        if (ExperimentManager.Instance)
        {
            ExperimentManager.Instance.OnConditionChanged -= OnConditionChanged;
        }
    }

    private void OnConditionChanged(Condition newCondition)
    {
        m_enterGameplayButton.SetActive(newCondition != null);
    }
}
