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
      //  fogOfWar.BoxFill()

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

    public void GenerateMap(){
        tilemap.ClearAllTiles();
        TileBase tileA = tileDatas[0].tiles[0];
        TileBase tileB = tileDatas[1].tiles[0];
        TileBase[] tileArray = new TileBase[area.size.x * area.size.y * area.size.z];
        for (int index = 0; index < tileArray.Length; index++){
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
        print(location);
        fogOfWar.SetTile(location - new Vector3Int(0,0,1), transparent);
    //    fogOfWar.BoxFill(location, transparent, location.x - 2, location.y - 2, location.x + 2, location.y + 2);
    }
}
