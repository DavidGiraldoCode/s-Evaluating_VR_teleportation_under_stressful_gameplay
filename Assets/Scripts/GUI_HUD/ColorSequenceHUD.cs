using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSequenceHUD : MonoBehaviour
{
    [SerializeField] private GameState m_gameState;
    [SerializeField] private ColorSwatchController[] m_colorSwatchController;

    private void Awake()
    {
        if (!m_gameState)
            throw new System.NullReferenceException("The GameState is missing");
    }

    private void OnEnable()
    {
        m_gameState.OnNewSequence += OnNewSequence;
        //InitializeColorHUD();

    }
    private void OnDisable()
    {
        m_gameState.OnNewSequence -= OnNewSequence;
    }

    // Custom
    private void OnNewSequence(Stack<GameState.color> sequence)
    {

        Debug.Log("OnNewSequence ColorSequenceHUD");
        SetColorHUD(sequence);
    }
    private void SetColorHUD(Stack<GameState.color> sequence)
    {
        m_colorSwatchController = FindObjectsOfType<ColorSwatchController>();

        IEnumerator<GameState.color> IColor = sequence.GetEnumerator();
        uint index = 0;

        while (IColor.MoveNext() != false)
        {
            GameState.color color = IColor.Current;
            m_colorSwatchController[index].ColorSwatch = (Color)GameState.ColorTable[color];
            index++;
        }
    }

}