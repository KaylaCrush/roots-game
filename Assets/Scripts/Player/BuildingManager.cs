using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    [SerializeField]
    private List<BuildingTile> tileDatas;

    private Dictionary<TileBase, BuildingTile> dataFromTiles;

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, BuildingTile>();

        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
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
}
