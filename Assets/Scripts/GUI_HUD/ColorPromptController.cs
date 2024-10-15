using UnityEngine;
using TMPro;
public class ColorPromptController : MonoBehaviour
{
    [SerializeField] private GameState m_gamesState;
    [SerializeField] private TMP_Text m_indcator;
    [SerializeField] private TMP_Text m_stroopStimuli;
    [SerializeField] private Color m_stroopColor;
    public Color StroopColor { get => m_stroopColor; set => m_stroopColor = value; }
    private string m_stroopStimuliType; // "WORD" "COLOR"
    public string StroopStimuliType { get => m_stroopStimuliType; set => m_stroopStimuliType = value; }

    #region MonoBehaviour Methods
    private void Awake()
    {
        if (!m_gamesState)
            throw new System.NullReferenceException("GameState is missing");
    }
    private void OnEnable()
    {
        m_gamesState.OnNewNextColor += OnNewNextColor;
    }
    private void OnDisable()
    {
        m_gamesState.OnNewNextColor -= OnNewNextColor;
    }

    #endregion MonoBehaviour Methods
    //
    private void OnNewNextColor(GameState.stimulus newStimulus, GameState.color newColor)
    {
        //Debug.Log("newColor: " + newColor);
        UpdateColorPromptDisplay( newStimulus.ToString(), newColor.ToString(), (Color)GameState.ReadableColorToRGB[newColor]);
    }

    private void UpdateColorPromptDisplay(string stroopStimuliType, string wordColor, Color newColor)
    {
        m_indcator.text = stroopStimuliType;
        m_stroopStimuli.color = newColor;
        m_stroopStimuli.text = wordColor;
    }
}