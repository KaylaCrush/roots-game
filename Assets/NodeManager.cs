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
}
