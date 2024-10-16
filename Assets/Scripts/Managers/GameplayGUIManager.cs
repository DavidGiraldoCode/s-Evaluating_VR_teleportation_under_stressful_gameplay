using System;
using TMPro;
using UnityEngine;

public class GameplayGUIManager : MonoBehaviour
{
    public static GameplayGUIManager Instance { get; private set; }
    [SerializeField] private GameObject m_startGameGUI;
    [SerializeField] private GameObject m_counterGUI;
    private ColorPromptController m_colorPromptController;
    #region Monobehavior
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

        m_colorPromptController = FindFirstObjectByType<ColorPromptController>();
        m_colorPromptController?.gameObject.SetActive(false);
    }
    private void Start()
    {
        // m_startPracticeGUI.SetActive(true);
        // m_startTrialGUI.SetActive(false);
        // m_counterGUI.SetActive(false);
    }

    private void OnEnable()
    {
        if (!GameplayManager.Instance) return;
        GameplayManager.OnPracticeBegin += OnPracticeBegin;
        GameplayManager.OnPracticeStandby += OnPracticeStandby;
        GameplayManager.OnPracticeEnd += OnPracticeEnd;
        GameplayManager.OnTrialStandby += OnTrialStandby;
        GameplayManager.OnTrialBegin += OnTrialBegin;
        GameplayManager.OnTrialEnd += OnTrialEnd;


        GameplayManager.OnPracticeBegin += OnTaskBegin;
        GameplayManager.OnTrialBegin += OnTaskBegin;

        GameplayManager.OnPracticeEnd += OnTaskEnd;
        GameplayManager.OnTrialEnd += OnTaskEnd;
    }

    private void OnDisable()
    {
        if (!GameplayManager.Instance) return;
        GameplayManager.OnPracticeBegin -= OnPracticeBegin;
        GameplayManager.OnPracticeStandby -= OnPracticeStandby;
        GameplayManager.OnPracticeEnd -= OnPracticeEnd;
        GameplayManager.OnTrialStandby -= OnTrialStandby;
        GameplayManager.OnTrialBegin -= OnTrialBegin;
        GameplayManager.OnTrialEnd -= OnTrialEnd;

        GameplayManager.OnPracticeBegin -= OnTaskBegin;
        GameplayManager.OnTrialBegin -= OnTaskBegin;

        GameplayManager.OnPracticeEnd += OnTaskEnd;
        GameplayManager.OnTrialEnd += OnTaskEnd;
    }

    #endregion Monobehavior
    #region Events handlers
    private void OnTaskBegin()
    {
        m_startGameGUI.SetActive(false);
        m_colorPromptController?.gameObject.SetActive(true);
    }
    private void OnTaskEnd()
    {
        m_colorPromptController?.gameObject.SetActive(false);
        m_counterGUI.SetActive(true);
    }

    private void OnPracticeStandby()
    {
        m_counterGUI.SetActive(false);
        m_startGameGUI.SetActive(true);
        m_startGameGUI.GetComponentInChildren<TMP_Text>().text = "Start the practice whenever you are ready";
    }

    private void OnPracticeBegin()
    {
        //m_startGameGUI.SetActive(false);
    }
    private void OnPracticeEnd()
    {
        //m_counterGUI.SetActive(true);
    }
    private void OnTrialStandby()
    {
        m_counterGUI.SetActive(false);
        m_startGameGUI.SetActive(true);
        m_startGameGUI.GetComponentInChildren<TMP_Text>().text = "Start the trial whenever you are ready";
    }
    private void OnTrialBegin()
    {
        //m_startGameGUI.SetActive(false);
    }
    private void OnTrialEnd()
    {
        //m_counterGUI.SetActive(true);
    }


    #endregion Events
}
