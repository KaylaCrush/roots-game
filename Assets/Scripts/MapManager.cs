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

    [SerializeField]
    private TileData DefaultTile;

    [SerializeField]
    private TileData BorderTile;

    [SerializeField]
    private int width=100, height=50;

    private BoundsInt area;

    private Dictionary<TileBase, TileData> dataFromTiles;

    private float seed = 420.69f;

    private void Awake()
    {
        area = new BoundsInt(-width/2, -height/2, 0, width, height, 1);
        // Cover map with fog of war
        TileBase[] tileArray = new TileBase[area.size.x * area.size.y];

        for (int index = 0; index < tileArray.Length; index++)
        {
            tileArray[index] = fog;
        }
        fogOfWar.SetTilesBlock(area, tileArray);

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
        //mapArray = SetDefaultTile(mapArray);
        mapArray = SetOtherTiles(mapArray);
        mapArray = SetBorder(mapArray);
        mapArray = PlaceGoal(mapArray);
        tilemap.SetTilesBlock(area, mapArray);
    }

    private TileBase[] SetDefaultTile(TileBase[] mapArray){
        for(int i = 0; i < area.size.x*area.size.y; i++){
            mapArray[i]=DefaultTile.tiles[0];
        }
        return mapArray;
    }

    private TileBase[] PlaceGoal(TileBase[] mapArray){
        int x_dim = Random.Range(0, (int)(area.size.x/4));
        int y_dim = Random.Range(0, (int)(area.size.y/4));

        if(Random.value >= 0.5){x_dim = area.size.x - x_dim;}
        if(Random.value >= 0.5){y_dim = area.size.y - y_dim;}

        mapArray[y_dim+(area.size.y*x_dim)] = winTile;
        return mapArray;
    }

    private TileBase[] SetBorder(TileBase[] mapArray){
        int index = 0;
        for(int i=0; i < area.size.y; i++){
            for(int j=0; j<area.size.x;j++){
                if(((i == 0 || j==0) || i ==area.size.y-1) || j==area.size.x-1){
                    mapArray[index] = BorderTile.tiles[0];
                }
                index++;
            }
        }
        return mapArray;
    }

    private TileBase[] SetOtherTiles(TileBase[] mapArray){
        int index;
        float zoom_factor = .25f;
        for(int i = 0; i <tileDatas.Count; i++){
            index = 0;
            for(int j = 0; j < area.size.y; j++){
                for(int k = 0; k < area.size.x; k++){
                    if(Mathf.PerlinNoise((j+seed+i)*zoom_factor, (k+seed+i)*zoom_factor) < tileDatas[i].spawn_weight){
                        mapArray[index] = tileDatas[i].tiles[0];
                    }
                    index++;
                }
            }
        }
        return mapArray;
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);
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
