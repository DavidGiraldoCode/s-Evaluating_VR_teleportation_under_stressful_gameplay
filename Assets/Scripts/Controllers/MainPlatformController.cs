using System;
using UnityEngine;

public class MainPlatformController : MonoBehaviour
{
    [SerializeField] private GameObject m_mainPlatformGO;

    private void OnEnable()
    {
        if (GameplayManager.Instance)
        {
            GameplayManager.OnPracticeStandby += OnRelocatePlayer;
            GameplayManager.OnTrialStandby += OnRelocatePlayer;
            GameplayManager.OnGameOver += OnRelocatePlayer;
        }
    }

    private void OnDisable()
    {
        if (GameplayManager.Instance)
        {
            GameplayManager.OnPracticeStandby -= OnRelocatePlayer;
            GameplayManager.OnTrialStandby -= OnRelocatePlayer;
            GameplayManager.OnGameOver -= OnRelocatePlayer;
        }
    }
    private void OnRelocatePlayer()
    {
        m_mainPlatformGO.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player outside");
        if (!other.gameObject.CompareTag("Player")) return;

        m_mainPlatformGO.SetActive(false);
    }
}
