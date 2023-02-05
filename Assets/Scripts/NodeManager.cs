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

    public GameObject nodePrefab;
    public TreeManager parentTree;

    public TileBase Straight;
    public TileBase Corner;
    public TileBase Branch;

    // Start is called before the first frame update
    void Start()
    {
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        parentTree = transform.parent.GetComponent<TreeManager>();

        toughness = mapManager.GetTileToughness(new Vector2(this.transform.position.x, this.transform.position.y));
        RotateTransformToDirection();
    }

    void Update()
    {
        RotateTransformToDirection();
    }

    void RotateTransformToDirection()
    {
        transform.rotation = Quaternion.Euler(eulerRotations[nodeDirection]);
    }
    private Dictionary<Direction, Vector3> eulerRotations = new Dictionary<Direction, Vector3>
        {
            { Direction.East , new Vector3(0, 0, 0) },
            { Direction.West , new Vector3(0, 0, 180) },
            { Direction.South , new Vector3(0, 0, 270) },
            { Direction.North , new Vector3(0, 0, 90) },
        };

    void TurnLeft()
    {
        nodeDirection = ccwTurns[nodeDirection];
    }

    void TurnRight()
    {
        nodeDirection = clockwiseTurns[nodeDirection];
    }

    void SetCurrentTreeTile(TileBase tileType)
    {
        myTree.SetTile(Vector3Int.FloorToInt(transform.position), tileType);
    }
    void ClearCurrentBuildingTile()
    {
        buildingManager.map.DeleteCells(Vector3Int.FloorToInt(transform.position), Vector3Int.one);
    }

    public void AddPower(float inputPower)
    {
        var instruction = buildingManager.GetTileInstruction(transform.position);
        if (instruction == BuildingTile.Instruction.Stop)
        {
            return;
        }

        power = power + inputPower;
        if (power > toughness)
        {
            // Advance node one step

            // Power reduced by toughness cost
            power = power - toughness;

            float nutrients = mapManager.GetTileNutrientGatherSpeed(new Vector2(this.transform.position.x, this.transform.position.y));
            parentTree.AddNutrients(nutrients);

            switch (instruction)
            {
                case BuildingTile.Instruction.Split:
                    var newNodeDirection = ccwTurns[nodeDirection];
                    var newNode = GameObject.Instantiate(nodePrefab, transform.position + directionVectors[newNodeDirection], Quaternion.Euler(eulerRotations[newNodeDirection])).GetComponent<NodeManager>();
                    newNode.nodeDirection = newNodeDirection;
                    newNode.myTree = myTree;
                    newNode.parentTree = parentTree;
                    newNode.transform.SetParent(parentTree.transform);
                    parentTree.nodes.Add(newNode);
                    TurnRight();
                    SetCurrentTreeTile(Branch);
                    ClearCurrentBuildingTile();
                    break;
                case BuildingTile.Instruction.Turn:
                    TurnRight();
                    ClearCurrentBuildingTile();
                    break;
                case BuildingTile.Instruction.NONE:
                    SetCurrentTreeTile(Straight);
                    break;
                case BuildingTile.Instruction.Stop:
                default:
                    ClearCurrentBuildingTile();
                    break;
            }

            transform.position += directionVectors[nodeDirection];

            mapManager.RevealTiles(Vector3Int.FloorToInt(transform.position));
            toughness = mapManager.GetTileToughness(transform.position);
        }
    }
}
