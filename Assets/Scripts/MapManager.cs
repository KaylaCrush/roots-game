using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    public Tilemap fogOfWar;
    public TileBase transparent;
    public TileBase fog;

    [SerializeField]
    private List<TileData> tileDatas;

    public BoundsInt area;


    private Dictionary<TileBase, TileData> dataFromTiles;

    private void Awake()
    {
        // Cover map with fog of war
        TileBase[] tileArray = new TileBase[area.size.x * area.size.y * area.size.z];
        for (int index = 0; index < tileArray.Length; index++)
        {
            tileArray[index] = fog;
        }
        fogOfWar.SetTilesBlock(area, tileArray);

        for (int i = -5; i < 5; i++)
        {
            for (int j = -5; j < 5; j++)
            {
                fogOfWar.SetTile(new Vector3Int(i, j, 0), null);
            }
        }


        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }

        GenerateMap();
    }

    public void GenerateMap()
    {
        tilemap.ClearAllTiles();
        TileBase tileA = tileDatas[0].tiles[0];
        TileBase tileB = tileDatas[1].tiles[0];
        TileBase[] tileArray = new TileBase[area.size.x * area.size.y * area.size.z];
        for (int index = 0; index < tileArray.Length; index++)
        {
            tileArray[index] = index % 2 == 0 ? tileA : tileB;
        }
        tilemap.SetTilesBlock(area, tileArray);
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);


        if (Input.GetMouseButton(0))
        {
            TileBase clickedTile = tilemap.GetTile(gridPosition);

            float Toughness = dataFromTiles[clickedTile].Toughness;

            print("At position " + gridPosition + " there is a " + clickedTile + " with toughness " + Toughness);
        }
    }

    public float GetTileToughness(Vector2 worldPosition)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile == null)
            return 1f;

        float Toughness = dataFromTiles[tile].Toughness;
        return Toughness;
    }

    public float GetTileNutrientGatherSpeed(Vector2 worldPosition)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile == null)
            return 1f;

        float NutrientGatherSpeed = dataFromTiles[tile].NutrientGatherSpeed;
        return NutrientGatherSpeed;
    }

    public void RevealTiles(Vector3Int location)
    {
        for (int i = -2; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                fogOfWar.SetTile(location + new Vector3Int(i, j, 1), null);
            }
        }
    }
}
