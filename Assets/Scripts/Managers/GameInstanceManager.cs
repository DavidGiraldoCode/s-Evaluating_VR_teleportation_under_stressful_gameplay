using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

color.RED => Color.red

*/

public class GameInstanceManager : MonoBehaviour
{
    public static GameInstanceManager Instance { get; private set; }
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

        InitializedPlatforms();

    }

    private void OnEnable()
    {
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
    }

    private void InitializedPlatforms()
    {
        m_PlatformStates = FindObjectsOfType<PlatformStateController>();
        Debug.Log("Plaforms found: " + m_PlatformStates.Length);

        colorsTable[PlatformState.color.RED] = Color.red;
        colorsTable[PlatformState.color.BLUE] = Color.blue;
        colorsTable[PlatformState.color.GREEN] = Color.green;

        for (int i = 0; i < m_PlatformStates.Length; i++)
        {
            m_PlatformStates[i].State.GetColor = (Color)colorsTable[m_PlatformStates[i].State.CurrentColor];
        }
    }
}
