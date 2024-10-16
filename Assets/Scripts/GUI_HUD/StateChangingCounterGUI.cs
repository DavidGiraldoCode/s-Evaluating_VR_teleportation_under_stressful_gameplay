using System.Collections;
using UnityEngine;
using TMPro;

public class StateChangingCounterGUI : MonoBehaviour
{
    [SerializeField] private GameState m_gameState;
    [SerializeField] private GameObject m_counterGUI;
    [SerializeField] private int m_waintingTime = 5;
    [SerializeField] private TMP_Text m_message;
    private string m_baseMessage;
    [SerializeField] private TMP_Text m_counterText;
    private float m_timer;
    private void Awake()
    {
        if (!m_gameState)
            throw new System.NullReferenceException("The GameState is missing");
        if (!m_counterGUI)
            throw new System.NullReferenceException("The CounterGUI is missing");

        m_timer = m_waintingTime;
        m_baseMessage = m_message.text;
    }
    private void OnEnable()
    {
        if (GameplayManager.Instance)
        {
            GameplayManager.OnPracticeEnd += OnPracticeEnd;
            GameplayManager.OnTrialEnd += OnTrialEnd;
        }
    }

    private void OnDisable()
    {
        if (GameplayManager.Instance)
        {
            GameplayManager.OnPracticeEnd -= OnPracticeEnd;
            GameplayManager.OnTrialEnd -= OnTrialEnd;
        }
    }
    private void OnPracticeEnd()
    {
        //Debug.Log("Starting counter to pass to Trial Standby...");
        m_message.text = m_baseMessage + "starting position in";
        StartCoroutine(CountDownToStartingPosition());
    }

    private void OnTrialEnd()
    {
        //Debug.Log("Starting counter to pass to Conditions...");
        m_message.text = m_baseMessage + "conditions menu in";
        StartCoroutine(CountDownToStartingPosition());
    }
    // Recall that coroutines are methods that can wait for a certain amount of time before excecuting actions on a next frame
    // "spread tasks across several frames"
    /// <summary>
    /// Counts before sending the player to the next state
    /// </summary>
    /// <returns></returns>
    IEnumerator CountDownToStartingPosition()
    {
        for (int i = m_waintingTime; i > -1; i--)
        {
            m_timer = i;
            m_counterText.text = m_timer.ToString();
            Debug.Log(m_timer);

            if (m_timer == 0)
            {
                m_gameState.GoToStandbyOrGameOver();
                m_timer = m_waintingTime;
                m_counterGUI.SetActive(false);
                StopAllCoroutines();
            }
            // The yield lien is where the excecution pauses resume in the next frame
            yield return new WaitForSeconds(1f);
        }
    }
}
