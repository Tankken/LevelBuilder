using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_WallBreakable : S_Wall {
    public bool isBreakable = true;
}

public class WallBreakable : Wall{
    public override obj GetDataToSerialize()
    {
        return new S_WallBreakable();
    }

}
