using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI
{
    MonsterBase currentMonster = MonsterManager.MonsterInstance.MonsterQueue.Peek();
    float distanceToEnemy;


    public bool CheckCondition()
    {
        
        for (int i = 0; i < currentMonster.AttackMove.Count; i++)
        {
            if (currentMonster.Condition >= currentMonster.AttackMove[i].Cost)
            {
                return true;
            }
        }
        for (int i = 0; i < currentMonster.Movement.Count; i++)
        {
            if (currentMonster.Condition >= currentMonster.Movement[i].Cost)
            {
                return true;
            }
        }
        return false;
    }


    public void PerformTurn()
    {
        if (CheckCondition())
        {
            var enemyMonster = FindNearestEnemy();
            if (distanceToEnemy == 1)
            {
                AttackEnemy(enemyMonster);
            }
            else
            {
                MoveToEnemy(enemyMonster);
            }
        }
        else
        {
            currentMonster.Condition = 0;
        }
        GameManager.GameInstance.MonsterTurn();
    }

    private void AttackEnemy(MonsterBase enemyMonster)
    {
        List<AttackBase> attacks = new List<AttackBase>();

        for (int i = 0; i < currentMonster.AttackMove.Count; i++)
        {
            Debug.Log("Condition - " + currentMonster.Condition + " Cost - " + currentMonster.AttackMove[i].Cost);
            if (currentMonster.AttackMove[i].Cost <= currentMonster.Condition)
            {
                attacks.Add(currentMonster.AttackMove[i]);
            }
        }
        Debug.Log("Current Monster " + currentMonster + " attacks: " + attacks.Count + " condition: " + currentMonster.Condition);
        if (attacks.Count > 0)
        {
            int randomNumber = UnityEngine.Random.Range(1, attacks.Count);

            AttackBehaviour attackBehaviour = new AttackBehaviour();
            Vector3 enemyTransform = enemyMonster.MonsterPrefab.transform.position;
            attackBehaviour.DoDmg(attacks[randomNumber -1 ], new Vector2(enemyTransform.x, enemyTransform.z));
        }
        else
        {
            currentMonster.Condition = 0;
        }
    }


    private void MoveToEnemy(MonsterBase enemyMonster)
    {
        float distance = 1000;
        int cost = 1000;
        Vector2 movement = new Vector2(0,0);
        Vector2 currentMonsterVector = new Vector2(currentMonster.MonsterPrefab.transform.position.x, currentMonster.MonsterPrefab.transform.position.z);
        Vector2 enemyMonsterVector = new Vector2(enemyMonster.MonsterPrefab.transform.position.x, enemyMonster.MonsterPrefab.transform.position.z);
        for (int i = 0; i < currentMonster.Movement.Count; i++)
        {

            var tempCost = currentMonster.Movement[i].Cost;
            for (int j = 0; j < currentMonster.Movement[i].MoveableTile.Count; j++)
            {
               
                var tempMovement = currentMonster.Movement[i].MoveableTile[j];
                var tempDistance = Vector2.Distance(tempMovement + currentMonsterVector, enemyMonsterVector);
                if (tempDistance != 0 && distance > tempDistance && currentMonster.Movement[i].Cost >= tempCost && cost >= tempCost)
                {
                    if (!GridManager.GridInstance.IsOccupied(currentMonsterVector + tempMovement))
                    {
                        distance = tempDistance;
                        cost = tempCost;
                        movement = tempMovement;
                    }                   
                }
            }
        }
        currentMonster.Condition -= cost;
        currentMonster.MonsterPrefab.transform.position += new Vector3(movement.x, 0, movement.y);
        GridManager.GridInstance.SetOccupied(currentMonsterVector + movement, currentMonster.MonsterPrefab);
        GridManager.GridInstance.SetUnoccupied(currentMonsterVector);
        GameManager.GameInstance.ChangeState(GameState.MonsterTurn);
    }


    //also sets distance
    public MonsterBase FindNearestEnemy()
    {
        Vector2 thisMonster = new Vector2(currentMonster.MonsterPrefab.transform.position.x, currentMonster.MonsterPrefab.transform.position.z);
        MonsterBase closestEnemy = null;
        float closestDistance = 1000000000f;
        var allMonster = MonsterManager.MonsterInstance.AllMonster;

        for (int i = 0; i < allMonster.Count; i++)
        {
            if (allMonster[i].Team == Team.Own)
            {
                float distance = Vector2.Distance(thisMonster, new Vector2(allMonster[i].MonsterPrefab.transform.position.x, allMonster[i].MonsterPrefab.transform.position.z));
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = allMonster[i];
                }
            }
        }
        distanceToEnemy = closestDistance;
        return closestEnemy;
    }
    
}