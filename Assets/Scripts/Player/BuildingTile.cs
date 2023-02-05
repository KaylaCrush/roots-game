using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class BuildingTile : ScriptableObject
{
    public TileBase[] tiles;

    enum Instruction
    {
        Split,
        Turn,
        Stop,
    }


    public float NutrientGatherSpeed, Toughness;

}
