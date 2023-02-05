using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextController : MonoBehaviour
{
    public GameStateManager gameState;
    public TreeManager tree;

    // Update is called once per frame
    void Update()
    {
        if (gameState.gameStarted && tree == null)
        {
            tree = GameObject.Find("PlayerTreeNodes").GetComponent<TreeManager>();
        }

        if (gameState.gameStarted)
        {
            float time = gameState.gameTime;
            float nutrients = tree.totalPower;
            float nodes = tree.nodes.Count;

            GetComponent<TMPro.TextMeshProUGUI>().text = "Game Time (Score) = " + time.ToString() + "\nNutrient Power = " + nutrients.ToString() + "\nLive Nodes = " + nodes.ToString();
        }
    }
}
