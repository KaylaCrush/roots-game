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
    public GameStateManager gameState;
    public TileBase winTile;

    [SerializeField]
    private List<TileData> tileDatas;

    public BoundsInt area;

    private Dictionary<TileBase, TileData> dataFromTiles;

    private float weight = .5f;
    private float seed = 420.69f;
    private float zoom_factor = .24f;

    private void Awake()
    {
        // Cover map with fog of war
        TileBase[] tileArray = new TileBase[area.size.x * area.size.y];

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

    //creates a 2d array corresponding to the tilemap
    //filled with random values between 0 and len(tileDatas)(-1)
    public void GenerateMap(){

        TileBase[] mapArray = new TileBase[area.size.x * area.size.y];

        tilemap.ClearAllTiles();
        SetDefaultTile(mapArray);
        SetOtherTiles(mapArray);
        tilemap.SetTilesBlock(area, mapArray);
    }

    private void SetDefaultTile(TileBase[] mapArray){
        for(int i = 0; i < area.size.x*area.size.y; i++){
            mapArray[i]=tileDatas[0].tiles[0];
        }
    }

    private void SetOtherTiles(TileBase[] mapArray){
        int index=0;
        foreach(TileData tiledata in tileDatas){
            for(int j = 0; j < area.size.x; j++){
                for(int k = 0; k < area.size.y; k++){
                    Debug.Log(index);
                    if(Mathf.PerlinNoise(j*zoom_factor+seed, k*zoom_factor+seed) > weight){mapArray[index] = tiledata.tiles[0];}
                    index++;
                }
            }
        }
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

        if (tilemap.GetTile(location + new Vector3Int(0, 0, 1)) == winTile)
        {
            gameState.WinGame(location);
        }
    }
}
