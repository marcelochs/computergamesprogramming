using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject highlight;
    bool isMovable = false;
    bool isAttackable = false;
    AttackBase attackType;
    MovementBase moveType;
    [SerializeField] Material highlightMat;


    public bool IsMovable
    {
        get { return isMovable; }
        set { isMovable = value; }
    }
    public bool IsAttackable
    {
        get { return isAttackable; }
        set { isAttackable = value; }
    }

    public AttackBase AttackType
    {
        get { return attackType; }
        set { attackType = value; }
    }
    public MovementBase MoveType
    {
        get { return moveType; }
        set { moveType = value;  }
    }

    private void OnMouseEnter()
    {
        if (isMovable ||isAttackable)
        {
            highlightMat.color = Color.green;
        }
        
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlightMat.color = Color.white;
        highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (isMovable)
        {
            moveTo(MonsterManager.MonsterInstance.MonsterQueue.Peek());
            GridManager.GridInstance.SetNotMovable();
        }
        else if(isAttackable)
        {
            

            AttackBehaviour attackBehaviour = new AttackBehaviour();
            attackBehaviour.DoDmg(attackType, new Vector2(transform.position.x,transform.position.z));
            GridManager.GridInstance.SetNotAttackable();
            if (GameManager.GameInstance.GameState != GameState.GameOver)
            {
                GameManager.GameInstance.ChangeState(GameState.MonsterTurn);
            }

        }
    }

    private void moveTo(MonsterBase monster)
    {
        var currentMonster = monster.MonsterPrefab;
        var grid = GridManager.GridInstance;
        grid.SetOccupied(new Vector2(transform.position.x, transform.position.z), currentMonster);
        grid.SetUnoccupied(new Vector2(currentMonster.transform.position.x, currentMonster.transform.position.z));

        currentMonster.transform.position = transform.position;
        monster.Condition -= moveType.Cost;

        UIManager.UiInstance.ChangeGameStatus();
    }
}
