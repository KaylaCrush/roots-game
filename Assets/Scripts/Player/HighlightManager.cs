using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightManager : MonoBehaviour
{
    public GameObject cursor;
    public GameObject prompt;
    public GameObject ui;
    public GameObject playerTree;
    public GameObject playerTreeGrid;

    public bool gameStarted = false;

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
        // cursor = GameObject.Find("Player-Highlight");
        // ui = GameObject.Find("Interface");
        // prompt = GameObject.Find("Prompt");
        // playerTreeGrid = GameObject.Find("Player-Tree");
        // playerTree = GameObject.Find("PlayerTree");
    }

    public void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        cursor.transform.position = snap(mousePosition) + new Vector3(0.5f, 0.5f, 0.5f);

        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            prompt.SetActive(false);
            ui.SetActive(true);
            playerTree.SetActive(true);
            playerTreeGrid.SetActive(true);
        }
    }
}
