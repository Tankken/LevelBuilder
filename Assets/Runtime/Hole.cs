using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Hole : obj { }

public class Hole : EditorObjectBase
{
    public override obj GetDataToSerialize()
    {
        return new S_Hole();
    }
}
