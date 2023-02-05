using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeManager : MonoBehaviour
{
    public List<NodeManager> nodes = new List<NodeManager>();
    public BoundsInt gameBounds;
    public Tilemap myTree;

    public bool PauseState = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        // Pausing the game
        if(Input.GetKeyDown("space"))
        {
            if(PauseState)
            {
                PauseState = false;
            } else
            {
                PauseState = true;
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        var totalPower = 4; // TEMP TODO: detect how much nutrient power the roots cover

        var powerPerUpdate = totalPower * Time.deltaTime;

        var powerPerNode = powerPerUpdate / nodes.Count;

        NodeManager[] nodesForPower = new NodeManager[nodes.Count];
        nodes.CopyTo(nodesForPower);

        if (!PauseState)
        {
            foreach (NodeManager node in nodesForPower)
            {
                node.AddPower(powerPerNode);
            }
        }

    }
}
