using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private TextMeshProUGUI _playerHealth;

    private static TextMeshProUGUI TextScore { get; set; }
    private static TextMeshProUGUI PlayerHealth { get; set; }

    public void Awake()
    {
        TextScore = _textScore;
        PlayerHealth = _playerHealth;

        if(TextScore != null)
            TextScore.text = SetValue.Score.ToString();

        if (PlayerHealth != null)
            PlayerHealth.text = SetValue.PlayerHealth.ToString();
    }

    public static void UpdateScoreUI()
    {
        TextScore.text = SetValue.Score.ToString();
    }

    public static void UpdateHealthUI()
    {
        PlayerHealth.text = SetValue.PlayerHealth.ToString();
    }
}
