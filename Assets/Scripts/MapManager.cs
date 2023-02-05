using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    [SerializeField]
    private List<TileData> tileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;

    private void Awake()
    {
        RenderMap();
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    public static void RenderMap(){
        map.ClearAllTiles();
        for(int x = 0; x<map.GetUpperBound(0); x++){
            for (int y = 0; y < map.GetUpperBound(1);y++){
                tilemap.SetTile();
            }
        }
    }


    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition);


        if (Input.GetMouseButton(0))
        {
            TileBase clickedTile = map.GetTile(gridPosition);

            float Toughness = dataFromTiles[clickedTile].Toughness;

            print("At position " + gridPosition + " there is a " + clickedTile + " with toughness " + Toughness);
        }
    }

    public float GetTileToughness(Vector2 worldPosition)
    {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);
        TileBase tile = map.GetTile(gridPosition);

        if (tile == null)
            return 1f;

        float Toughness = dataFromTiles[tile].Toughness;
        return Toughness;
    }

    public float GetTileNutrientGatherSpeed(Vector2 worldPosition)
    {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);
        TileBase tile = map.GetTile(gridPosition);

        if (tile == null)
            return 1f;

        float NutrientGatherSpeed = dataFromTiles[tile].NutrientGatherSpeed;
        return NutrientGatherSpeed;
    }
}
