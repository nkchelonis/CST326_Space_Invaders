
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    private int _currentScore = 0;
    private int _highScore = 0;

    private const string HIGH_SCORE_KEY = "SpaceInvaders_HighScore";
    
    void Start()
    {
       // sign up for notification about enemy death 
       Enemy.OnEnemyDied += OnEnemyDied;
       
       //get high score from preferences
       _highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
       string text = "HIGH SCORE: " + _highScore.ToString("d4");
       highScoreText.SetText(text);
    }

    void Update()
    {
        //set new high score
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, _highScore);
        }
    }

    void OnDestroy()
    {
        Enemy.OnEnemyDied -= OnEnemyDied;
    }

    //maybe pass enemy rather than score itself
    void OnEnemyDied(int score)
    {
        Debug.Log($"Killed enemy worth{score}");
        _currentScore += score;
        string text = "SCORE: " + _currentScore.ToString("d4");
        scoreText.SetText(text);
    }
}
