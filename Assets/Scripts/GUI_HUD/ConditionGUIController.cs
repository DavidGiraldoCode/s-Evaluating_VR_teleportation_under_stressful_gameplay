using UnityEngine;

public class ConditionGUIController : MonoBehaviour
{
    [SerializeField] private GameObject m_enterGameplayButton;
    [SerializeField] private GameObject m_conditionGUIMenu;
    private void Awake()
    {
        //* This is the original proposal, where the conditions were pre-defined
        // if (ExperimentManager.Instance)
        //     m_enterGameplayButton.SetActive(ExperimentManager.Instance.CurrentCondition != null);
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

}
