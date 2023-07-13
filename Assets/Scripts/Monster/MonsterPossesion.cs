using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPossesion : MonoBehaviour
{
    public List<MonsterBase> allMonsterTemplate;
    public List<int> level;
    public List<ExperienceType> expType;
    public List<Vector2> pos;

    public List<MonsterBase> allMonster;

    private void Start()
    {
        CreateMonsters();
    }

    private void CreateMonsters()
    {
        allMonster.Clear();

        for (int i = 0; i < allMonsterTemplate.Count; i++)
        {
            MonsterBase newMonster = Instantiate(allMonsterTemplate[i]);
            newMonster.Initialize(level[i], expType[i], pos[i]);
            allMonster.Add(newMonster);
        }
    }
}
