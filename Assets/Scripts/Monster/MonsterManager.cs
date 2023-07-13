using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private bool firstRound = false;
    public static MonsterManager MonsterInstance;
    
    List<MonsterBase> ownMonster;
    List<MonsterBase> enemyMonster;

    List<MonsterBase> allMonster;
    Queue<MonsterBase> monsterQueue;

    public List<MonsterBase> OwnMonster
    {
        get { return ownMonster; }
        set { ownMonster = value; }
    }

    public List<MonsterBase> EnemyMonster
    {
        get { return enemyMonster; }
        set { enemyMonster = value; }
    }

    public List<MonsterBase> AllMonster
    {
        get { return allMonster; }
        set { allMonster = value; }
    }


    public Queue<MonsterBase> MonsterQueue
    {
        get { return monsterQueue; }
        set { monsterQueue = value; }
    }

    private void Awake()
    {
        MonsterInstance = this;
        allMonster = new List<MonsterBase>();
        DontDestroyOnLoad(gameObject);
    }

    public void setMonster(List<MonsterBase> ownM, List<MonsterBase> enemyM)
    {
        ownMonster = ownM;
        enemyMonster = enemyM;
    }

    public void SpawnOwnMonster()
    {
        for (int i = 0; i < ownMonster.Count; i++)
        {
            //TODO IF:  IS OCCUPIED AND MOVE TO OTHER POS
            ownMonster[i].Team = Team.Own;
            allMonster.Add(ownMonster[i]);
            Debug.Log(OwnMonster[i] + " " + OwnMonster[i].MaxHP);

            Vector3 monsterPos = new Vector3(2, 0, 5) + new Vector3(ownMonster[i].StartPosition.x, 0, ownMonster[i].StartPosition.y);
            var spawnedMonster = Instantiate(ownMonster[i].MonsterPrefab,  monsterPos, Quaternion.Euler(0, 90, 0));

            ownMonster[i].MonsterPrefab = spawnedMonster;
            GridManager.GridInstance.SetOccupied(new Vector2(monsterPos.x, monsterPos.z), spawnedMonster);



            
        }
        GameManager.GameInstance.ChangeState(GameState.SpawnEnemyMonster);
    }
    public void SpawnEnemyMonster()
    {
        for (int i = 0; i < enemyMonster.Count; i++)
        {
            //TODO IF:  IS OCCUPIED AND MOVE TO OTHER POS
            enemyMonster[i].Team = Team.Enemy;
            allMonster.Add(enemyMonster[i]);

            Debug.Log(EnemyMonster[i] + " " + EnemyMonster[i].MaxHP);

            Vector3 monsterPos = new Vector3(13, 0, 5) - new Vector3(enemyMonster[i].StartPosition.x, 0, enemyMonster[i].StartPosition.y);
            var spawnedMonster = Instantiate(enemyMonster[i].MonsterPrefab, monsterPos, Quaternion.Euler(0, 270, 0));

            enemyMonster[i].MonsterPrefab = spawnedMonster;
            GridManager.GridInstance.SetOccupied(new Vector2(monsterPos.x, monsterPos.z), spawnedMonster);


            

        }
        firstRound = true;
        GameManager.GameInstance.ChangeState(GameState.OrderMonster);
    }

    public void OrderMonsterList()
    {
        
        chargeCondition();
        allMonster.Sort((a, b) => b.Initiative.CompareTo(a.Initiative));       
        monsterQueue = new Queue<MonsterBase>(allMonster);
        GameManager.GameInstance.ChangeState(GameState.MonsterTurn);
    }

    private void chargeCondition()
    {
        if (!firstRound)
        {
            for (int i = 0; i < allMonster.Count; i++)
            {
                allMonster[i].Condition += 50;
            }
        }
        firstRound = false;
    }

    public void RemoveMonsterFromAllList(MonsterBase monster)
    {
        int index = allMonster.IndexOf(monster);

        if (index >= 0)
        {
            allMonster.RemoveAt(index);
        }
    }

    public bool AreMonstersRemaining(Team team)
    {
        return allMonster.Exists(monster => monster.Team == team);
    }
}
