using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeManager : MonoBehaviour
{
//    public List<Node> nodes;
    public List<int> nodes = new List<int> { 1, 2, 3 }; // temp until we get nodes as a game object
    public BoundsInt gameBounds;
    public Tilemap myTree;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButton(0))
        {
            TileBase[] tileArray = myTree.GetTilesBlock(gameBounds);
            for (int index = 0; index < tileArray.Length; index++)
            {
                print(tileArray[index]);
            }

        }
        */
    }
}
