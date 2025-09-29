using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;

public class Node : MonoBehaviour
{
    public enum NodeTypes
    {
        Basic,
        CharacterStart,
        EnemySpawn,
        Terrain,
        Boss
    }

    //A* Algorithm for finding out if something is in range or not
    [HideInInspector] public float g, h, f = 0;
    [HideInInspector] public Node parent;

    public NodeTypes types = NodeTypes.Basic;
    [SerializeField] private List<Node> adjacentNodes = new List<Node>();
    [SerializeField] private float boundingRadius = 4.3f;

    [SerializeField] public int weightCapacity = 3;

    [HideInInspector] public bool selectable = false;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAdjacentNodes();
    }

    private void Update()
    {
        /*
        if(Input.touchCount > 0)
        {
            Touch  touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Ended)
            {
                Controls.SelectNode(this);
            }
        }
        */
    }

    private void FixedUpdate()
    {
        if (selectable)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnMouseDown()
    {
        Controls.SelectNode(this);
    }

    public int AddEntity(Entity entity)
    {
        int currWeight = GetCurrentWeight();
        if (currWeight >= weightCapacity) return 0;

        entity.transform.SetParent(transform);
        entity.transform.localPosition = Vector3.zero;
        return 1;
    }

    public int RemoveEntity(Entity entity)
    {
        entity.transform.SetParent(null);
        return 1;
    }

    public int GetCurrentWeight()
    {
        int currWeight = 0;
        foreach (Entity e in GetEntities())
        {
            currWeight += e.weight;
        }
        return currWeight;
    }

    public List<Entity> GetEntities()
    {
        return transform.GetComponentsInChildren<Entity>().ToList();
    }
    public List<Unit> GetUnits()
    {
        return transform.GetComponentsInChildren<Unit>().ToList();
    }

    public void UpdateAdjacentNodes()
    {
        adjacentNodes = SetAdjacentNodes();
    }

    private List<Node> SetAdjacentNodes()
    {
        List<Node> list = new List<Node>();

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, boundingRadius);
        foreach(Collider2D collider in hitColliders)
        {
            if (collider.transform == transform) continue;
            //will have to check if any walls are in the way too
            if(collider.transform.GetComponent<Node>())
            {
                list.Add(collider.transform.GetComponent<Node>());
            }
        }

        return list;
    }

    public List<Node> GetAdjacentNodes()
    {
        return adjacentNodes;
    }

    public Map GetMap()
    {
        return FindFirstObjectByType<Map>();
    }
}
