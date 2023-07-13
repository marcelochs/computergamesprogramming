using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour
{
    MonsterBase currentMonster = MonsterManager.MonsterInstance.MonsterQueue.Peek();

    public void activateTiles(AttackBase thisAttack)
    {
        int count = 0;
        for (int i = 0; i < thisAttack.AttackTile.Count; i++)
        {
            var actualPos = currentMonster.MonsterPrefab.transform.localPosition;
            var vector = new Vector2(actualPos.x, actualPos.z) + thisAttack.AttackTile[i];
            if (GridManager.GridInstance.IsOccupied(vector) && vector.x >= 0 && vector.y >= 0 && vector.x < GridManager.GridInstance.Width && vector.y < GridManager.GridInstance.Height)
            {
                if (checkIfEnemy(GridManager.GridInstance.GetOccupied(vector)))
                {
                    Tile thisTile = GridManager.GridInstance.GetTileAtPos(vector);
                    thisTile.AttackType = thisAttack;
                    thisTile.IsAttackable = true;
                    count++;
                }
            }
        }
        if (count == 0)
        {
            UIManager.UiInstance.TurnOnInteractionButtons();
        }
    }

    public void DoDmg(AttackBase attack, Vector2 monsterPos)
    {
        for(int i = 0; i < attack.AttackRange.Count; i++)
        {
            var pos = monsterPos + attack.AttackRange[i];
            var gManager = GridManager.GridInstance;
            if (gManager.IsOccupied(pos))
            {
                var monster = gManager.GetMonsterBase(gManager.GetOccupied(pos));
                Team team = monster.Team;
                var currenMonster = MonsterManager.MonsterInstance.MonsterQueue.Peek();
                monster.GetDamage(attack.AttackDamage[i] * currenMonster.Attack / 200);               
               
            }
            
            
        }
        if(GameManager.GameInstance.GameState != GameState.GameOver)
        {

            currentMonster.Condition -= attack.Cost;
        }
    }

   

    public bool checkIfEnemy(GameObject thisMonster)
    {
        var enemyMonster = MonsterManager.MonsterInstance.EnemyMonster;
        for (int i = 0; i < enemyMonster.Count; i++)
        {
            if (enemyMonster[i].MonsterPrefab == thisMonster)
            {
                return true;
            }
        }
        return false;
    }
}
