using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Monster/Create new Monster")]
public class MonsterBase : ScriptableObject
{
    [SerializeField] private string monsterName;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHP;
    [SerializeField] private int attack;
    [SerializeField] private int specialAttack;
    [SerializeField] private int defense;
    [SerializeField] private int initiative;
    [SerializeField] private int condition;
    [SerializeField] private int speed;
    [SerializeField] private int level;
    [SerializeField] private ExperienceType expType;
    [SerializeField] private NatureType natureType;
    [SerializeField] private Team team;
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private List<MovementBase> movement;
    [SerializeField] private List<AttackBase> attackMove;





    public string MonsterName
    {
        get { return monsterName; }
        set { monsterName = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public GameObject MonsterPrefab
    {
        get { return monsterPrefab; }
        set { monsterPrefab = value; }
    }

    public int MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
    }

    public int CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }

    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    public int SpecialAttack
    {
        get { return specialAttack; }
        set { specialAttack = value; }
    }

    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    public int Initiative
    {
        get { return initiative; }
        set { initiative = value; }
    }

    public int Condition
    {
        get { return condition; }
        set { condition = value; }
    }

    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public Vector2 StartPosition
    {
        get { return startPosition; }
        set { startPosition = value; }
    }

    public ExperienceType ExpType
    {
        get { return expType; }
        set { expType = value; }
    }

    public NatureType NatureType
    {
        get { return natureType; }
        set { natureType = value; }
    }

    public Team Team
    {
        get { return team; }
        set { team = value; }
    }

   

    public void Initialize(int level, ExperienceType expType, Vector2 pos)
    {
        int factorial = 1;
        for(int i = 1; i <= level; i++)
        {
            factorial = factorial * i;
        }
        this.maxHP = maxHP + (factorial/2);
        this.attack = attack + (factorial/4);
        this.initiative = initiative + (factorial/4);
        this.currentHP = maxHP;
        this.level = level;
        this.expType = expType;
        this.startPosition = pos;        
    }

    public List<MovementBase> Movement
    {
        get { return movement; }
        set { movement = value; }
    }

    public List<AttackBase> AttackMove
    {
        get { return attackMove; }
        set { attackMove = value; }
    }

    public void GetDamage(int dmg)
    {
        currentHP -= dmg;
        Debug.Log(monsterPrefab + " Current HP: " + currentHP);
        //CHECK OB TOT UND LÖSCH AUS LISTE
        if (currentHP <= 0)
        {
            GridManager.GridInstance.SetUnoccupied(new Vector2(monsterPrefab.transform.position.x, monsterPrefab.transform.position.z));
            MonsterManager.MonsterInstance.RemoveMonsterFromAllList(this);
            if (!MonsterManager.MonsterInstance.AreMonstersRemaining(team))
            {
                GameManager.GameInstance.ChangeState(GameState.GameOver);
                UIManager.UiInstance.InteractionButt.SetActive(false);
            }
            Destroy(monsterPrefab);
        }
        
        
        
    }
}

public enum ExperienceType
{
    VeryFast,
    Fast,
    Medium,
    Low,
    VeryLow
}

public enum NatureType
{
    Normal,
    Fire,
    Air,
    Earth,
    Water,
    Grass
}

public enum Team
{
    Own,
    Enemy
}