using UnityEngine;
using UnityEngine.UI;

public class ConditionGUISetter : MonoBehaviour
{
    [SerializeField] private Condition m_condition;
    [SerializeField] private Button m_button;

    private void Awake()
    {
        Debug.Log(m_condition);
        m_button = GetComponent<Button>();
    }

    private void OnEnable()
    {

        if (GameInstanceManager.Instance)
        {
            Debug.Log("Subscribed");
            GameInstanceManager.Instance.OnConditionChanged += OnConditionChanged;
        }
        else
        {
            Debug.Log("Nothing to Subscribe to");
        }

    }

    private void OnDisable()
    {
        if (GameInstanceManager.Instance)
            GameInstanceManager.Instance.OnConditionChanged -= OnConditionChanged;
    }

    private void OnConditionChanged(Condition newCondition)
    {
        // If the current condition is the same as this button's condition, disable its interaction
        m_button.interactable = (m_condition != newCondition);
    }
    public void SetCondition()
    {
        if (!GameInstanceManager.Instance) return;
        GameInstanceManager.Instance.SetCondition(m_condition);
    }
}
