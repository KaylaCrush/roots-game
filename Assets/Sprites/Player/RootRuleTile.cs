using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class RootRuleTile : RuleTile
{

    // public enum SibingGroup
    // {
    //     Trees,
    // }
    // public SiblingGroup siblingGroup;

    public override bool RuleMatch(int neighbor, TileBase other)
    {
        if (other is RuleOverrideTile)
            other = (other as RuleOverrideTile).m_InstanceTile;

        switch (neighbor)
        {
            case TilingRule.Neighbor.This:
                {
                    return other is RootRuleTile;
                    // && (other as RootRuleTile).sibingGroup == this.sibingGroup;
                }
            case TilingRule.Neighbor.NotThis:
                {
                    return !(other is RootRuleTile);
                    // && (other as RootRuleTile).sibingGroup == this.sibingGroup);
                }
        }

        return base.RuleMatch(neighbor, other);
    }
}
