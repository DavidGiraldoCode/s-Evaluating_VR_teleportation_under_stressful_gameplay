using UnityEngine;
using TMPro;
using UnityEngine.ProBuilder;
/// <summary>
/// This is attached to the Wrist_Mounted_Display Prefab
/// </summary>
public class ColorPromptController : MonoBehaviour
{
    [SerializeField] private GameState m_gamesState;
    [SerializeField] private ParticipantData m_participantData;
    [SerializeField] private TMP_Text m_indcator;
    [SerializeField] private TMP_Text m_timer;
    [SerializeField] private TMP_Text m_stroopStimuli;
    [SerializeField] private Color m_stroopColor;
    public Color StroopColor { get => m_stroopColor; set => m_stroopColor = value; }
    private string m_stroopStimuliType; // "WORD" "COLOR"
    public string StroopStimuliType { get => m_stroopStimuliType; set => m_stroopStimuliType = value; }

    // This stores the last biased word color so the next time, it can change
    private string m_previousBiasedWordColor = "";

    #region MonoBehaviour Methods
    private void Awake()
    {
        if (!m_gamesState)
            throw new System.NullReferenceException("GameState is missing");
        if (!m_participantData)
            throw new System.NullReferenceException("ParticipantData is missing");
    }
    private void OnEnable()
    {
        m_gamesState.OnNewNextColor += OnNewNextColor;

    }
    private void OnDisable()
    {
        m_gamesState.OnNewNextColor -= OnNewNextColor;
    }

    private void Update()
    {
        if (GameplayManager.Instance)
            m_timer.text = ((int)GameplayManager.Instance.Timer).ToString();
    }

    #endregion MonoBehaviour Methods
    //
    public void OnNewNextColor(GameState.stimulus newStimulus, GameState.taskColors newColor, bool wasCompleted)
    {
        //Debug.Log("OnNewNextColor: " + newColor);
        UpdateColorPromptDisplay(newStimulus.ToString(), newColor.ToString(), (Color)GameState.ReadableColorToRGB[newColor]);
    }

    private void UpdateColorPromptDisplay(string stroopStimuliType, string wordColor, Color newColor)
    {
        //Debug.Log(newColor);
        m_indcator.text = stroopStimuliType;
        string baisedWordColor = wordColor;

        // Apply Game Stressor based on Stroop Test
        if (m_participantData.GameStressorBiasedInstruction)
        {
            while (baisedWordColor == wordColor) // Checks until the baisedWordColor is different from the original
            {
                float t = Random.value;
                GameState.taskColors baisedColor = (GameState.taskColors)(0.0f * t + (1.0f - t) * 5.0f);

                if (baisedColor.ToString() == m_previousBiasedWordColor) // Makes sure that is different from the last biased result
                    continue;

                baisedWordColor = baisedColor.ToString();
            }

            m_stroopStimuli.color = newColor;
            m_stroopStimuli.text = baisedWordColor;
            m_previousBiasedWordColor = m_stroopStimuli.text;
        }
        else
        {

            m_stroopStimuli.color = newColor;
            m_stroopStimuli.text = wordColor;
        }
    }

    public void SetTaskColorPromptDisplay(string stroopStimuliType, string wordColor, Color newColor)
    {
        //      Debug.Log("Direct setting of color: " + newColor.ToString());
        //        Debug.Log((Color)GameState.ReadableColorToRGB[newColor]);
        //m_stroopStimuli.color = (Color)GameState.ReadableColorToRGB[newColor];

        m_indcator.text = stroopStimuliType;
        m_stroopStimuli.color = newColor;
        m_stroopStimuli.text = wordColor;
    }
}
