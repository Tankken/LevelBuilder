using System;
using UnityEngine;

public enum TankIdentifiers { PLAYER, BROWN, GRAY, YELLOW, RED, PURPLE, GREEN, BLUE, WHITE, BLACK, PINK, BOXING, SATELLITE }

public class S_Tank : obj
{
    public ushort type;
}

public class Tank : EditorObjectBase
{
    
    public TankIdentifiers TankType;

    public override obj GetDataToSerialize()
    {
        if (TankType == TankIdentifiers.PLAYER)
        {
            throw new Exception("EnemyTanks cannot be PLAYER type");
        }
        var s = new S_Tank();
        s.type = (ushort)TankType;
        return s;
    }
}
