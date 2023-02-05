using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class BuildingTile : ScriptableObject
{
    public TileBase[] tiles;

    public enum Instruction
    {
        Split,
        Turn,
        Stop,
        NONE,
    }

    public Instruction instructionType = Instruction.NONE;

}
