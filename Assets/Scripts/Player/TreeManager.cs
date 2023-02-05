using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeManager : MonoBehaviour
{
    public List<NodeManager> nodes = new List<NodeManager>();
    public BoundsInt gameBounds;
    public Tilemap myTree;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var totalPower = 4; // TEMP TODO: detect how much nutrient power the roots cover

        var powerPerUpdate = totalPower * Time.deltaTime;

        var powerPerNode = powerPerUpdate / nodes.Count;

        foreach(NodeManager node in nodes)
        {
            node.AddPower(powerPerNode);
        }

    }
}
