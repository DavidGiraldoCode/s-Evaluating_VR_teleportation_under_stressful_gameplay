using System.Collections;
using UnityEngine;

public class GameInstanceManager : MonoBehaviour
{
    public static GameInstanceManager Instance { get; private set; }
    [SerializeField] private GameState m_gameState;
    private PlatformStateController[] m_PlatformStates;
    private Hashtable colorsTable = new Hashtable();
    // Unity 
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

        if (!m_gameState)
            throw new System.NullReferenceException("The GameInstance is missing the GameState");

        InitializedPlatforms();
    }

    private void OnEnable()
    {
        m_PlatformStates = FindObjectsOfType<PlatformStateController>();
        for (int i = 0; i < m_PlatformStates.Length; i++)
        {
            m_PlatformStates[i].State.OnStateChange += OnPlaformStateChange;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < m_PlatformStates.Length; i++)
        {
            m_PlatformStates[i].State.OnStateChange -= OnPlaformStateChange;
        }
    }
    // Custom
    private void OnPlaformStateChange(PlatformState.state state, PlatformState.color color)
    {
        Debug.Log("The " + color.ToString() + " platform has changed to: " + state.ToString());
        m_gameState.CurrentColor = (GameState.color) color;
    }

    private void InitializedPlatforms()
    {
        if(m_PlatformStates == null) return;

        Debug.Log("Plaforms found: " + m_PlatformStates.Length);

        colorsTable[PlatformState.color.RED] = Color.red;
        colorsTable[PlatformState.color.BLUE] = Color.blue;
        colorsTable[PlatformState.color.GREEN] = Color.green;

        for (int i = 0; i < m_PlatformStates.Length; i++)
        {
            m_PlatformStates[i].State.InitializePlatform( (Color)colorsTable[m_PlatformStates[i].State.CurrentColor]);
        }
    }
}
