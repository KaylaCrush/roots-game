using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeManager : MonoBehaviour
{
    public float power;
    public float toughness;

    public MapManager mapManager;
    public Tilemap myTree;
    public Tilemap Environment;
    public Tilemap Buildings;

    public TileBase StraightEW;
    public TileBase StraightNS;
    public TileBase CornerNW;
    public TileBase CornerNE;
    public TileBase CornerSW;
    public TileBase CornerSE;
    public TileBase BranchNEW;
    public TileBase BranchNES;
    public TileBase BranchNWS;
    public TileBase BranchSEW;

    // Start is called before the first frame update
    void Start()
    {
        toughness = mapManager.GetTileToughness(new Vector2(this.transform.position.x, this.transform.position.y));

        //TODO fill in mapmanager, tilemaps on instantiate
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPower(float inputPower)
    {
        power = power + inputPower;
        if (power > toughness)
        {
            power = power - toughness;

            myTree.SetTile(Vector3Int.FloorToInt(this.transform.position), StraightEW);
            this.transform.position = new Vector3(this.transform.position.x-1, this.transform.position.y, this.transform.position.z);
            // TODO
        }
    }
}
