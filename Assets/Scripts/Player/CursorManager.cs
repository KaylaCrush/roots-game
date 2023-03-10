using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CursorManager : MonoBehaviour
{
    public GameObject cursor;
    public GameObject prompt;
    public GameObject ui;
    public GameObject playerTree;
    public GameObject playerTreeGrid;
    public AudioSource music;
    public BuildingManager buildingManager;
    public GameStateManager gameStateManager;
    public MapManager mapManager;

    public static Vector3 snap(Vector3 pos)
    {
        float x = pos.x;
        float y = pos.y;
        float z = pos.z;

        x = Mathf.FloorToInt(x);
        y = Mathf.FloorToInt(y);
        z = Mathf.FloorToInt(z);

        return new Vector3(x, y, z);
    }

    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
    }

    public void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int buildingGridPosition = buildingManager.map.WorldToCell(mousePosition);

        cursor.transform.position = snap(mousePosition) + new Vector3(0.5f, 0.5f, 0.5f);

        if (Input.GetMouseButtonDown(0) & !gameStateManager.gameStarted)
        {
            prompt.SetActive(false);
            ui.SetActive(true);
            playerTree.SetActive(true);
            playerTreeGrid.SetActive(true);

            //       playerTree.transform.position = buildingManager.map.WorldToCell(mousePosition);// + new Vector3(8f, 1f, -0.5f);
            //       playerTreeGrid.transform.position = buildingManager.map.WorldToCell(mousePosition);// + new Vector3(7f,2f,-0.5f);
            mapManager.RevealTiles(new Vector3Int(-8, -2, -1));
            gameStateManager.gameStarted = true;
            music.Play();
        }

        if (gameStateManager.gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                buildingManager.PlaceBuildingTile(buildingGridPosition, BuildingTile.Instruction.Split);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                buildingManager.PlaceBuildingTile(buildingGridPosition, BuildingTile.Instruction.Turn);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                buildingManager.PlaceBuildingTile(buildingGridPosition, BuildingTile.Instruction.Stop);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                buildingManager.Undo();
            }
        }
    }
}
