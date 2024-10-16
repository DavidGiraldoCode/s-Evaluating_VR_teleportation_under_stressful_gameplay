using System;
using UnityEngine;

public class GameplayGUIManager : MonoBehaviour
{
    public static GameplayGUIManager Instance { get; private set; }
    [SerializeField] private GameObject m_startPracticeGUI;
    [SerializeField] private GameObject m_startTrialGUI;
    [SerializeField] private GameObject m_counterGUI;
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
    }

   
    #endregion Monobehavior
    #region Events handlers
    private void OnPracticeStandby()
    {
        m_startPracticeGUI.SetActive(true);
        m_startTrialGUI.SetActive(false);
        m_counterGUI.SetActive(false);
    }
    private void OnPracticeBegin()
    {
        m_startPracticeGUI.SetActive(false);
    }
    private void OnPracticeEnd()
    {
        m_counterGUI.SetActive(true);
    }
    private void OnTrialStandby()
    {
        m_counterGUI.SetActive(false);
        m_startTrialGUI.SetActive(true);
        Debug.Log(m_startPracticeGUI.activeSelf);
    }

    private void OnTrialBegin()
    {
        m_startTrialGUI.SetActive(false);
    }
    private void OnTrialEnd()
    {
        m_counterGUI.SetActive(true);
    }

    #endregion Events
}
