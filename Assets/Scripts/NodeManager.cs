using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeManager : MonoBehaviour
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }
    public Dictionary<Direction, Direction> clockwiseTurns = new Dictionary<Direction, Direction>
    {
        { Direction.North, Direction.East },
        { Direction.East,  Direction.South },
        { Direction.South,  Direction.West },
        { Direction.West,  Direction.North },
    };
    public Dictionary<Direction, Direction> ccwTurns = new Dictionary<Direction, Direction>
    {
        { Direction.North, Direction.West },
        { Direction.West,  Direction.South },
        { Direction.South,  Direction.East },
        { Direction.East,  Direction.North },
    };
    public Dictionary<Direction, Vector3> directionVectors = new Dictionary<Direction, Vector3>
    {
        { Direction.North, new Vector3(0, 1, 0) },
        { Direction.East, new Vector3(1, 0, 0) },
        { Direction.South, new Vector3(0, -1, 0) },
        { Direction.West, new Vector3(-1, 0, 0) }
    };

    public Direction nodeDirection;
    public float power;
    public float toughness;

    public MapManager mapManager;
    public BuildingManager buildingManager;
    public Tilemap myTree;
    public Tilemap Environment;
    public Tilemap Buildings;

    public GameObject nodePrefab;
    public TreeManager parentTree;

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
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        parentTree = transform.parent.GetComponent<TreeManager>();

        toughness = mapManager.GetTileToughness(new Vector2(this.transform.position.x, this.transform.position.y));

        //TODO fill in mapmanager, tilemaps on instantiate
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TurnLeft()
    {
        nodeDirection = ccwTurns[nodeDirection];
    }

    void TurnRight()
    {
        nodeDirection = clockwiseTurns[nodeDirection];
    }

    public void AddPower(float inputPower)
    {
        power = power + inputPower;
        if (power > toughness)
        {
            // Advance node one step

            // Power reduced by toughness cost
            power = power - toughness;


            myTree.SetTile(Vector3Int.FloorToInt(transform.position), StraightEW);

            var instruction = buildingManager.GetTileInstruction(transform.position);
            print(instruction);
            switch (instruction)
            {
                case BuildingTile.Instruction.Split:
                    var newNodeDirection = ccwTurns[nodeDirection];
                    var newNode = GameObject.Instantiate(nodePrefab, transform.position + directionVectors[newNodeDirection], transform.rotation).GetComponent<NodeManager>();
                    newNode.nodeDirection = newNodeDirection;
                    newNode.myTree = myTree;
                    parentTree.nodes.Add(newNode);
                    TurnRight();
                    break;
                case BuildingTile.Instruction.Stop:
                case BuildingTile.Instruction.Turn:
                    TurnRight();
                    break;
                case BuildingTile.Instruction.NONE:
                default:
                    break;
            }

            transform.position += directionVectors[nodeDirection];

            toughness = mapManager.GetTileToughness(transform.position);
        }
    }
}
