using UnityEngine;

public class GameplayGUIManager : MonoBehaviour
{
    public static GameplayGUIManager Instance { get; private set; }
    [SerializeField] private GameObject m_startPracticeGUI;
    [SerializeField] private GameObject m_startTrialGUI;
    [SerializeField] private GameObject m_counterGUI;

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
        m_startPracticeGUI.SetActive(true);
        m_startTrialGUI.SetActive(false);
        m_counterGUI.SetActive(false);
    }

    private void OnEnable()
    {
        
    }

}
