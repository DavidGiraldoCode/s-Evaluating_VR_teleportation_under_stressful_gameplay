using UnityEngine;
using UnityEngine.UI;

public class ConditionGUISetter : MonoBehaviour
{
    [SerializeField] private Condition m_condition;
    [SerializeField] private Button m_button;

    private void Awake()
    {
        if(!GameInstanceManager.Instance)
            throw new System.NullReferenceException("The GameInstanceManager is missing in the scene");

        m_button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        SubribeFromConditionEvents();
    }

    private void OnDisable()
    {
        UnsubribeFromConditionEvents();
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
            UnsubribeFromConditionEvents();
    }
    public void SetCondition()
    {
        if (!GameInstanceManager.Instance) return;
        GameInstanceManager.Instance.SetCondition(m_condition);
    }

    private void SubribeFromConditionEvents()
    {
        if (GameInstanceManager.Instance)
        {
            GameInstanceManager.Instance.OnConditionChanged += OnConditionChanged;
            GameInstanceManager.Instance.OnConditionTerminated += OnConditionTerminated;
            GameInstanceManager.Instance.OnConditionFulfilled += OnConditionFulfilled;
        }
    }
    private void UnsubribeFromConditionEvents()
    {
        if (GameInstanceManager.Instance)
        {
            GameInstanceManager.Instance.OnConditionChanged -= OnConditionChanged;
            GameInstanceManager.Instance.OnConditionTerminated -= OnConditionTerminated;
            GameInstanceManager.Instance.OnConditionFulfilled -= OnConditionFulfilled;
        }
    }
}
