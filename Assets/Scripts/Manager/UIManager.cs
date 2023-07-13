using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{


    public static UIManager UiInstance;
    [SerializeField] GameObject interactionButt;
    [SerializeField] GameObject attackButt;
    [SerializeField] GameObject gameWon;
    [SerializeField] GameObject gameLost;
    MonsterBase currentMonster;


    

    private void Awake()
    {
        UiInstance = this;
    }

    public GameObject InteractionButt
    {
        get { return interactionButt; }
        set { interactionButt = value; }
    }
    public GameObject AttackButt
    {
        get { return attackButt; }
        set { attackButt = value; }
    }

    public GameObject GameWon
    {
        get { return gameWon; }
        set { gameWon = value; }
    }
    public GameObject GameLost
    {
        get { return gameLost; }
        set { gameLost = value; }
    }

    public void TurnOnInteractionButtons()
    {
        interactionButt.SetActive(true);
        currentMonster = MonsterManager.MonsterInstance.MonsterQueue.Peek();
    }

    public void RestMonster()
    {
        currentMonster.Condition = 0;
        ChangeGameStatus();
    }

    public void MoveMonster()
    {
        //Färbe die bewegungsstellen ein
        interactionButt.SetActive(false);
        var actualPos = currentMonster.MonsterPrefab.transform.localPosition;
        for (int i = 0; i < currentMonster.Movement.Count; i++)
        {
            
            for (int j = 0; j < currentMonster.Movement[i].MoveableTile.Count; j++)
            {
                //Check out of bounds, check occupied, color for movement
                var vector = new Vector2(actualPos.x, actualPos.z) + currentMonster.Movement[i].MoveableTile[j];
                if (!GridManager.GridInstance.IsOccupied(vector)&& vector.x >= 0 && vector.y >= 0 && vector.x < GridManager.GridInstance.Width && vector.y < GridManager.GridInstance.Height )
                {
                    Tile thisTile = GridManager.GridInstance.GetTileAtPos(vector);
                    thisTile.MoveType = currentMonster.Movement[i];
                    thisTile.IsMovable = true;
                }
            }
        }
    }

    public void AttackMonster()
    {
        interactionButt.SetActive(false);
        attackButt.SetActive(true);
        for(int i = 0; i < currentMonster.AttackMove.Count; i++)
        {
            Instantiate(currentMonster.AttackMove[i].AttackButton, attackButt.transform);
        }
        
    }

    public void ChangeGameStatus()
    {
        interactionButt.SetActive(false);
        GameManager.GameInstance.ChangeState(GameState.MonsterTurn);
    }
}
