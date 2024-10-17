using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorSwatchController : MonoBehaviour
{
    [SerializeField] private RawImage m_colorSwatchDisplay;
    [SerializeField] private Color m_colorSwatch;
    [SerializeField] private bool m_isCompleted;
    public Color ColorSwatch { get => m_colorSwatch; set => m_colorSwatch = value; }
    public bool IsCompleted { get => m_isCompleted; }
    // Unity
    private void Awake()
    {
        if(!m_colorSwatchDisplay)
            m_colorSwatchDisplay.GetComponent<RawImage>();
    }
    private void Start()
    {
        //Debug.Log("Start ColorSwatchController");
        SetColorSwatchDisplay();   
    }
    // Custom
    private void SetColorSwatchDisplay()
    {
        if(!m_colorSwatchDisplay) return;
        m_colorSwatchDisplay.color = m_colorSwatch;
    }
}
