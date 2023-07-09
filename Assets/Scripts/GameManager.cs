using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int currentScore = 0;
    public event EventHandler<ScoreChangedEventArgs> ScoreChanged;
    public int CurrentScore 
    {
        get 
        {
            return currentScore;
        }
        set 
        { 
            currentScore = value;
            OnScoreChanged(new ScoreChangedEventArgs() { Score = CurrentScore });
        }
    }
    void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
    }

    public void LoadGameplay()
    {
        currentScore = 0;
        SceneManager.LoadScene(1);
    }

    protected virtual void OnScoreChanged(ScoreChangedEventArgs e)
    {
        ScoreChanged?.Invoke(this, e);
    }
}
