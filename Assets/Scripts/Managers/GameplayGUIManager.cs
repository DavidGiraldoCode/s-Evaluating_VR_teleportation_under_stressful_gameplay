using System;
using TMPro;
using UnityEngine;

public class GameplayGUIManager : MonoBehaviour
{
    public static GameplayGUIManager Instance { get; private set; }
    [SerializeField] private GameState m_gameState;
    [SerializeField] private GameObject m_startGameGUI;
    [SerializeField] private GameObject m_counterGUI;
    [SerializeField] private ColorPromptController m_colorPromptController;

    #region Monobehavior Methods
    private void Awake()
    {
        if (!Instance || Instance != this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (m_colorPromptController == null)
            m_colorPromptController = FindFirstObjectByType<ColorPromptController>();

        m_colorPromptController.gameObject.SetActive(false);

        m_startGameGUI.SetActive(false);
        m_counterGUI.SetActive(false);
    }
    private void Start()
    {
        // m_startPracticeGUI.SetActive(true);
        // m_startTrialGUI.SetActive(false);
        // m_counterGUI.SetActive(false);
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsuscribeToEvents();
    }

    #endregion Monobehavior
    #region Events handlers
    private void SubscribeToEvents()
    {
        if (!GameplayManager.Instance) return;

        GameplayManager.OnPracticeStandby += OnTaskStandby;
        GameplayManager.OnTrialStandby += OnTaskStandby;

        GameplayManager.OnPracticeBegin += OnTaskBegin;
        GameplayManager.OnTrialBegin += OnTaskBegin;

        GameplayManager.OnPracticeEnd += OnTaskEnd;
        GameplayManager.OnTrialEnd += OnTaskEnd;

        // GameplayManager.OnPracticeBegin += OnPracticeBegin;
        // GameplayManager.OnPracticeEnd += OnPracticeEnd;
        // GameplayManager.OnTrialBegin += OnTrialBegin;
        // GameplayManager.OnTrialEnd += OnTrialEnd;
    }
    private void UnsuscribeToEvents()
    {
        if (!GameplayManager.Instance) return;

        GameplayManager.OnPracticeStandby -= OnTaskStandby;
        GameplayManager.OnTrialStandby -= OnTaskStandby;

        GameplayManager.OnPracticeBegin -= OnTaskBegin;
        GameplayManager.OnTrialBegin -= OnTaskBegin;

        GameplayManager.OnPracticeEnd += OnTaskEnd;
        GameplayManager.OnTrialEnd += OnTaskEnd;

        // GameplayManager.OnPracticeBegin -= OnPracticeBegin;
        // GameplayManager.OnPracticeEnd -= OnPracticeEnd;
        // GameplayManager.OnTrialBegin -= OnTrialBegin;
        // GameplayManager.OnTrialEnd -= OnTrialEnd;
    }
    private void OnTaskBegin()
    {
        m_startGameGUI.SetActive(false);
        m_colorPromptController.gameObject.SetActive(true);
        m_gameState.OnNewNextColor += m_colorPromptController.OnNewNextColor;
        m_colorPromptController.SetTaskColorPromptDisplay(  m_gameState.CurrentStimulus.ToString(), 
                                                            m_gameState.CurrentTaskColor().ToString(),
                                                            (Color)GameState.ReadableColorToRGB[m_gameState.CurrentTaskColor()]);

    }
    private void OnTaskEnd()
    {
        m_colorPromptController.gameObject.SetActive(false);
        m_gameState.OnNewNextColor -= m_colorPromptController.OnNewNextColor;

        m_counterGUI.SetActive(true);
    }
    private void OnTaskStandby()
    {
        m_counterGUI.SetActive(false);
        m_startGameGUI.SetActive(true);

        if (m_gameState.CurrentState == GameState.state.PRACTICE_STANDBY)
            m_startGameGUI.GetComponentInChildren<TMP_Text>().text = "Start the practice whenever you are ready";
        else if (m_gameState.CurrentState == GameState.state.TRIAL_STANDBY)
            m_startGameGUI.GetComponentInChildren<TMP_Text>().text = "Start the trial whenever you are ready";
    }

    // private void OnPracticeStandby()
    // {
    //     m_counterGUI.SetActive(false);
    //     m_startGameGUI.SetActive(true);
    //     m_startGameGUI.GetComponentInChildren<TMP_Text>().text = "Start the practice whenever you are ready";
    // }

    // private void OnTrialStandby()
    // {
    //     m_counterGUI.SetActive(false);
    //     m_startGameGUI.SetActive(true);
    //     m_startGameGUI.GetComponentInChildren<TMP_Text>().text = "Start the trial whenever you are ready";
    // }
    // private void OnPracticeBegin()
    // {
    //     //m_startGameGUI.SetActive(false);
    // }
    // private void OnPracticeEnd()
    // {
    //     //m_counterGUI.SetActive(true);
    // }
    // private void OnTrialBegin()
    // {
    //     //m_startGameGUI.SetActive(false);
    // }
    // private void OnTrialEnd()
    // {
    //     //m_counterGUI.SetActive(true);
    // }


    #endregion Events
}
