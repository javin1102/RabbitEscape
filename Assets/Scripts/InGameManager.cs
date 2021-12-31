using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InGameManager : MonoBehaviour
{
    public enum GameState { PLAY,PAUSE, GAMEOVER}
    public Direction nextLaneDir;
    
    public static InGameManager instance;
    public event Action<bool> onGenerateLane;
    public GameObject [] groundList;
    public GameObject[] obsList;
    public GameObject[] powList;

    public GameState gameState;

    private InGameUIController inGameUIController;
    private bool hasPlaySFX = false;

    public void GenerateLaneEnter(bool generatePow)
    {
        if (onGenerateLane != null)
        {
            onGenerateLane(generatePow);
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        nextLaneDir = Direction.STRAIGHT;
        gameState = GameState.PLAY;
        inGameUIController = InGameUIController.instance;
    }

    private void Update()
    {
        if(gameState == GameState.GAMEOVER)
        {
            inGameUIController.GameOver();
            if(!hasPlaySFX)
            {
                print("Play");
                AudioManager.instance.PlayS("game_over");
                hasPlaySFX = true;
            }
        }
    }


}
