using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    public Text highScorePauseText;
    public Text highScoreGameOverText;
    public Text scoreGameOverText;
    public int highScore;
    public int incrementRate = 32;

    private int currScore;
    private InGameManager inGameManager;

    private const string HIGHSCORE_TAG = "HIGHSCORE";
    private void Awake()
    {
        if (instance == null)
            instance = this;

    }
    void Start()
    {
        currScore = 0;
        inGameManager = InGameManager.instance;
        highScore = PlayerPrefs.GetInt(HIGHSCORE_TAG);

    }
    private void Update()
    {
        if (inGameManager.gameState == InGameManager.GameState.GAMEOVER)
        {
            if(currScore > highScore)
            {
                highScore = currScore;
                PlayerPrefs.SetInt(HIGHSCORE_TAG, highScore);

            }
            return;
        }
        
        scoreText.text = currScore.ToString();
        highScorePauseText.text = highScore.ToString();
        highScoreGameOverText.text = highScore.ToString();
        scoreGameOverText.text = currScore.ToString();
    }

    private void FixedUpdate()
    {
       

        currScore += (int)(incrementRate * Time.fixedDeltaTime);
        
    }

}
