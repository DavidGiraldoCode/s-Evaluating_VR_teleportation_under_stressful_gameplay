using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Constrols the scale of a cylinder that blocks the Nav Mesh and creates the illussion that the platform
/// has less teleportable area. but the Nav Mesh is the same, only the blocker changes it scale.
/// </summary>
public class ShrinkPlatformController : MonoBehaviour
{
    [SerializeField] private GameState m_gameState;
    [SerializeField] private ParticipantData m_participantData;
    [Tooltip("The cylinder that block the NavMesh")]
    [SerializeField] private Transform m_shrinkingMesh;
    [SerializeField] private float m_shrinkingSpeed = 0.01f;
    [SerializeField] private float m_reductionFactor = 0.25f;
    private Vector3 m_reduction;

    private void Awake()
    {
        if (!m_participantData)
            throw new NullReferenceException("The ParticipantData is Missing");

        ResetScale();
    }

    private void OnEnable()
    {
        m_gameState.OnNewNextColor += OnNewNextColor;
    }

    private void OnDisable()
    {
        m_gameState.OnNewNextColor -= OnNewNextColor;
    }

    private void Update()
    {
        //ShrinkTeleportationBlocker();
    }
    // Listen to events

    private void OnNewNextColor(GameState.stimulus newStimulus, GameState.taskColors nextColor, bool wasCompleted)
    {
        StopCoroutine(ShrinkTeleportationBlocker());
        ResetScale();
        StartCoroutine(ShrinkTeleportationBlocker());
        //Debug.Log($"XXX NEW color, scaling!");
    }

    private void ResetScale()
    {
        m_shrinkingMesh.localScale = Vector3.one * 2.0f;
        m_reduction = Vector3.one * 2.0f * m_reductionFactor; // Copy the scale vector
        m_reduction *= -1f; // Flip it
    }

    IEnumerator ShrinkTeleportationBlocker()
    {
        // Ensure the shrinking logic only happens when the attention demand stressor is active
        while (m_participantData.GameStressorAttentionDemand)
        {
            // Shrink the mesh
            m_shrinkingMesh.localScale += m_reduction * m_shrinkingSpeed * Time.deltaTime;

            // Stop shrinking if the scale magnitude is smaller than the reduction magnitude
            if (m_shrinkingMesh.localScale.magnitude < m_reduction.magnitude)
                break;

            // Wait for the next frame
            yield return null;
        }
    }
}
