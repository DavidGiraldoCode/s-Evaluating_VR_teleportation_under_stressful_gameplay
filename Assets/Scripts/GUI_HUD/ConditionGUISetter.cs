using UnityEngine;
using UnityEngine.UI;

public class ConditionGUISetter : MonoBehaviour
{
    [SerializeField] private Condition m_condition;

    private void Awake()
    {
        Debug.Log(m_condition);
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
        Debug.Log("OnConditionChanged");
        GetComponent<Button>().interactable = !(m_condition == newCondition);
        GetComponent<Button>().enabled = !(m_condition == newCondition);
    }
    public void SetCondition()
    {
        if (!GameInstanceManager.Instance) return;
        GameInstanceManager.Instance.SetCondition(m_condition);
    }
}
