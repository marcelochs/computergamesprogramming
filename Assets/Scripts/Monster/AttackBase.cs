using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Attack", menuName = "Attack/Create new Attack")]
public class AttackBase : ScriptableObject
{
    //TODO accuracy
    //Damage type
    [SerializeField]
    private string attackName;
    [SerializeField]
    private List<Vector2> attackTile;
    [SerializeField]
    private List<Vector2> attackRange;
    [SerializeField]
    private List<int> attackDamage;
    [SerializeField]
    private int cost;
    [SerializeField]
    private GameObject attackButton;


    public string AttackName
    {
        get { return attackName; }
        set { attackName = value; }
    }
    public List<Vector2> AttackTile
    {
        get { return attackTile; }
        set { attackTile = value; }
    }

    public List<Vector2> AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    public List<int> AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }
    public int Cost
    {
        get { return cost; }
        set { cost = value; }
    }
    public GameObject AttackButton
    {
        get { return attackButton; }
        set { attackButton = value; }
    }

    public void triggerButton()
    {
        var buttons = UIManager.UiInstance.AttackButt;
        buttons.SetActive(false);
        AttackBehaviour attackBehaviour = new AttackBehaviour();
        attackBehaviour.activateTiles(this);


        foreach (Transform child in buttons.transform)
        {
            Destroy(child.gameObject);
            
        }
    }


}
