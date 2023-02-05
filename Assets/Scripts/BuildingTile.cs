using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class ConstructedTile : ScriptableObject
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
