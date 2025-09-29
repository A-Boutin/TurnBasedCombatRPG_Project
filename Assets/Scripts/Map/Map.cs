using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] public List<Node> nodes = new List<Node>();
    // Start is called before the first frame update
    void Start()
    {
        nodes = GetComponentsInChildren<Node>().ToList();
    }

    public static void MoveUnit(Node target, Unit unit)
    {
        Node oldPos = unit.GetCurrentPosition();
        oldPos.RemoveEntity(unit);
        int moved = target.AddEntity(unit);
        if (moved == 0) oldPos.AddEntity(unit);
    }

    public List<Entity> GetMapEntities()
    {
        List<Entity> entities = new List<Entity>();
        foreach(Node node in nodes)
        {
            foreach(Entity e in node.GetEntities())
            {
                entities.Add(e);
            }
        }
        return entities;
    }

    public List<Unit> GetMapUnits()
    {
        List<Unit> units = new List<Unit>();
        foreach (Node node in nodes)
        {
            foreach (Entity e in node.GetEntities())
            {
                if(e.GetType().IsSubclassOf(typeof(Unit)))
                    units.Add((Unit)e);
            }
        }
        return units;
    }

    public List<Enemy> GetMapEnemies()
    {
        List<Enemy> enemies = new List<Enemy>();
        foreach (Node node in nodes)
        {
            foreach (Entity e in node.GetEntities())
            {
                if (e.GetType() == typeof(Enemy))
                    enemies.Add((Enemy)e);
            }
        }
        return enemies;
    }

    public List<Player> GetMapPlayers()
    {
        List<Player> players = new List<Player>();
        foreach (Node node in nodes)
        {
            foreach (Entity e in node.GetEntities())
            {
                if (e.GetType() == typeof(Player))
                    players.Add((Player)e);
            }
        }
        return players;
    }
}
