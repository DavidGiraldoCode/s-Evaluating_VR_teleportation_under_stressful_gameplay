using UnityEngine;
using UnityEngine.UI;

public class ConditionGUISetter : MonoBehaviour
{
    [SerializeField] private Condition m_condition;
    [SerializeField] private Button m_button;

    private void Awake()
    {
        m_button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (GameInstanceManager.Instance)
        {
            GameInstanceManager.Instance.OnConditionChanged += OnConditionChanged;
            GameInstanceManager.Instance.OnConditionTerminated += OnConditionTerminated;
        }
        else
        {
            Debug.Log("Nothing to Subscribe to");
        }
    }

    private void OnDisable()
    {
        if (GameInstanceManager.Instance)
        {
            GameInstanceManager.Instance.OnConditionChanged -= OnConditionChanged;
            GameInstanceManager.Instance.OnConditionTerminated -= OnConditionTerminated;
        }
    }

    private void OnConditionChanged(Condition newCondition)
    {
        // If the current condition is the same as this button's condition, disable its interaction
        m_button.interactable = (m_condition != newCondition);
    }
    private void OnConditionTerminated(Condition newCondition)
    {
        // If the contidion is completed, the button does not need to be interactive
        m_button.interactable = !m_condition.IsCompleted;
    }
    public void SetCondition()
    {
        if (!GameInstanceManager.Instance) return;
        GameInstanceManager.Instance.SetCondition(m_condition);
    }
}
