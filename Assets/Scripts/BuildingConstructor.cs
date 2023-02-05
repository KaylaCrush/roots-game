using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstructor : MonoBehaviour
{
    enum ConstructionMode
    {
        Branch,
        Turn,
        End,
        Delete,
    }

    void Update()
    {
        var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(worldPoint, Vector2.down);

            if (hit.collider != null)
            {
                Debug.Log("click on " + hit.collider.name);
                Debug.Log(hit.point);
            }
        }
    }
}
