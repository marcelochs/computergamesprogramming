using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Movement", menuName = "Movement/Create new Movement")]
public class MovementBase : ScriptableObject
{
    [SerializeField]
    private List<Vector2> moveableTile;
    [SerializeField]
    private int cost;

    public List<Vector2> MoveableTile
    {
        get { return moveableTile; }
        set { moveableTile = value; }
    }

    public int Cost
    {
        get { return cost; }
        set { cost = value; }
    }

}
