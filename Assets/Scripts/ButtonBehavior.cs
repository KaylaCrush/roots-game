using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ButtonBehavior : MonoBehaviour
{
    public Texture2D splitTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    [SerializeField]
    private Tilemap buildingMap;
    [SerializeField]
    private Tilemap treeMap;

    [SerializeField]
    private List<TileData> tileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    void Update(){

        if(Input.GetMouseButtonDown(1)){
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
        if(Input.GetKeyDown("1")){
            PressSplit();
        }
    }

    private void LateUpdate(){

    }

    public void PressSplit(){
        Cursor.SetCursor(splitTexture, hotSpot, cursorMode);
    }
}
