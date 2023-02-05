using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    public bool gameStarted = false;
    public bool paused = false;
    public float gameTime = 0;

    public void Update()
    {
        // Pausing the game
        if (Input.GetKeyDown("space"))
        {
            if (paused)
            {
                paused = false;
            }
            else
            {
                paused = true;
            }
        }

        if (!paused & gameStarted)
        {
            gameTime = gameTime + Time.deltaTime;
        }
    }

    public void WinGame(Vector3Int location)
    {
        print("winStateDetected");
        paused = true;
        GameObject.Find("PlayerTreeNodes").GetComponent<TreeManager>().PauseState = true;
        Camera.main.transform.position = location;
    }


}
