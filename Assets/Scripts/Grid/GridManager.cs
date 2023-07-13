using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager GridInstance;
    [SerializeField] private int width, height;
    [SerializeField] private Tile grassTile, waterTile;
    [SerializeField] private Transform camera;

    Dictionary<Vector2, Tile> tiles;
    Dictionary<Vector2, GameObject> occupiedTiles;

    public int Width
    {
        get { return width; }
        set { width = value; }
    }
    public int Height
    {
        get { return height; }
        set { height = value; }
    }

    private void Awake()
    {
        GridInstance = this;
    }

    
    public void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        occupiedTiles = new Dictionary<Vector2, GameObject>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var randomTile = Random.Range(0, 10) >= 9 ? waterTile : grassTile;
                var spawnedTile = Instantiate(randomTile, new Vector3(x, 0, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }


        camera.position = new Vector3((float)width / 2 -0.5f, 6, 0);

        GameManager.GameInstance.ChangeState(GameState.SpawnOwnMonster);
    }


    public Tile GetTileAtPos(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        Debug.Log("DID NOT WORK " + pos);
        return null;

    }


    public GameObject GetOccupied(Vector2 pos)
    {
      
            if(occupiedTiles.TryGetValue(pos,out var monster))
        {
            return monster;
        }
        return null;
    }
    public bool IsOccupied(Vector2 pos)
    {
        return occupiedTiles.ContainsKey(pos);
    }

    public void SetOccupied(Vector2 pos, GameObject occObj)
    {
        occupiedTiles[pos] = occObj;
    }

    public void SetUnoccupied(Vector2 pos)
    {
        occupiedTiles.Remove(pos);
    }

    public void SetNotMovable()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tile = GetTileAtPos(new Vector2(x, y));
                tile.IsMovable = false;
            }
        }
    }
    public void SetNotAttackable()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tile = GetTileAtPos(new Vector2(x, y));
                tile.IsAttackable = false;
            }
        }
    }

    public MonsterBase GetMonsterBase(GameObject thisMonster)
    {
        var allMonster = MonsterManager.MonsterInstance.AllMonster;
        for (int i = 0; i < allMonster.Count; i++)
        {
            if (allMonster[i].MonsterPrefab == thisMonster)
            {
                return allMonster[i];
            }
        }
        return null;
    }
}
