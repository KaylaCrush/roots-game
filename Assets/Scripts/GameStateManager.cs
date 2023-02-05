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
        if (!paused & gameStarted)
        {
            gameTime = gameTime + Time.deltaTime;
        }
    }

    public void WinGame(Vector3Int location)
    {
        paused = true;
        Camera.main.transform.position = location;
    }


}
