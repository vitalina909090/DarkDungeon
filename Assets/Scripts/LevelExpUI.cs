using UnityEngine;
using TMPro;

public class LevelExpUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        if (text == null) text = GetComponent<TextMeshProUGUI>();

        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.OnStatsChanged += UpdateText;
            UpdateText();
        }
    }

    void UpdateText()
    {
        text.text = $"Level {PlayerStats.Instance.Level}   EXP: {PlayerStats.Instance.CurrentExp}/{PlayerStats.Instance.ExpToNextLevel}";
    }

    void OnDestroy()
    {
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.OnStatsChanged -= UpdateText;
        }
    }
}