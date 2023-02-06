using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    public Tilemap map;

    [SerializeField]
    private List<BuildingTile> tileDatas;
    [SerializeField]
    public Stack<(TileBase from, TileBase to, Vector3 position)> historyStack;

    private Dictionary<BuildingTile.Instruction, TileBase> tilePalette;
    private Dictionary<TileBase, BuildingTile> dataFromTiles;

    public Tilemap treeMap;

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, BuildingTile>();
        tilePalette = new Dictionary<BuildingTile.Instruction, TileBase>();
        historyStack = new Stack<(TileBase from, TileBase to, Vector3 position)>();

        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
                tilePalette[tileData.instructionType] = tile;
            }
        }
    }

    public BuildingTile.Instruction GetTileInstruction(Vector2 worldPosition)
    {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);
        TileBase tile = map.GetTile(gridPosition);

        if (tile == null)
            return BuildingTile.Instruction.NONE;

        return dataFromTiles[tile].instructionType;
    }

    public void PlaceBuildingTile(Vector3 worldPosition, BuildingTile.Instruction type)
    {
        var worldPositionInt = Vector3Int.FloorToInt(worldPosition);
        Debug.Log(tilePalette[type]);
        historyStack.Push((map.GetTile(worldPositionInt), tilePalette[type], worldPosition));
        map.SetTile(worldPositionInt, tilePalette[type]);
    }

    public void Undo()
    {
        var historyFrame = historyStack.Pop();

        if (treeMap.HasTile(Vector3Int.FloorToInt(historyFrame.position)))
        {
            return;
        }

        map.SetTile(Vector3Int.FloorToInt(historyFrame.position), historyFrame.from);
    }
}
