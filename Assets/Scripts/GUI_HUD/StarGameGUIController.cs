using UnityEngine;
using TMPro;
[RequireComponent(typeof(TMP_Text))]
public class StarGameGUIController : MonoBehaviour
{
    [SerializeField] private GameState m_gameState;
    private TMP_Text m_message;

    private void Awake()
    {
        if (!m_gameState)
            throw new System.NullReferenceException("GameState is missing");
        m_message = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        if (!GameplayManager.Instance) return;
        GameplayManager.OnPracticeStandby += OnPracticeStandby;
        GameplayManager.OnTrialStandby += OnTrialStandby;
    }

    private void OnDisable()
    {
        if (!GameplayManager.Instance) return;
        GameplayManager.OnPracticeStandby -= OnPracticeStandby;
        GameplayManager.OnTrialStandby -= OnTrialStandby;
    }
    private void OnPracticeStandby()
    {
        m_message.text = "Start the practice whenever you are ready";
    }
    private void OnTrialStandby()
    {
        m_message.text = "Start the trial whenever you are ready";
    }
}
