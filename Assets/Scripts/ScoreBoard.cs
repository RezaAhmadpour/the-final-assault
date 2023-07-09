using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    TextMeshProUGUI txtScore;

    void Start()
    {
        txtScore = GameObject.Find("TxtScore").GetComponent<TextMeshProUGUI>();
        FindObjectOfType<GameManager>().ScoreChanged += UpdateScoreText;
    }

    void UpdateScoreText(object source, ScoreChangedEventArgs e)
    {
        txtScore.text = e.Score.ToString();
    }
}
