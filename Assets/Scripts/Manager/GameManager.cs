using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GameInstance;
    public GameState GameState;

    private void Awake()
    {
        GameInstance = this;
    }

    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }


    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.GridInstance.GenerateGrid();
                break;
            case GameState.SpawnOwnMonster:
                MonsterManager.MonsterInstance.SpawnOwnMonster();
                break;
            case GameState.SpawnEnemyMonster:
                MonsterManager.MonsterInstance.SpawnEnemyMonster();
                break;
            case GameState.OrderMonster:
                MonsterManager.MonsterInstance.OrderMonsterList();
                break;
            case GameState.MonsterTurn:
                MonsterTurn();
                break;
            case GameState.PlayerInteraction:
                UIManager.UiInstance.TurnOnInteractionButtons();
                break;
            case GameState.GameOver:
                Debug.Log("End");
                if (MonsterManager.MonsterInstance.AreMonstersRemaining(Team.Own))
                {
                    UIManager.UiInstance.GameWon.SetActive(true);
                }
                else
                {
                    UIManager.UiInstance.GameLost.SetActive(true);
                }
                break;

        }
    }

    public void MonsterTurn()
    {
        if (GameManager.GameInstance.GameState == GameState.GameOver)
        {
            return; 
        }

        if (MonsterManager.MonsterInstance.MonsterQueue.Count == 0)
        {
            ChangeState(GameState.OrderMonster);
            return;
        }

        var currentMonster = MonsterManager.MonsterInstance.MonsterQueue.Peek();
        if (currentMonster.Condition > 0)
        {
            if (currentMonster.Team == Team.Own)
            {
                ChangeState(GameState.PlayerInteraction);
            }
            else if (currentMonster.Team == Team.Enemy)
            {
                MonsterAI simpleAI = new MonsterAI();
                simpleAI.PerformTurn();
            }
        }
        else
        {
            MonsterManager.MonsterInstance.MonsterQueue.Dequeue();
            MonsterTurn();
                
            
        }
    }



}


public enum GameState
{
    GenerateGrid = 0,
    SpawnOwnMonster = 1,
    SpawnEnemyMonster = 2,
    OrderMonster = 3,
    MonsterTurn = 4,
    PlayerInteraction = 5,
    GameOver = 6
}