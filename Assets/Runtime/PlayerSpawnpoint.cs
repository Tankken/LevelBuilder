using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Spawnpoint : obj { }

public class PlayerSpawnpoint : EditorObjectBase
{
    public override obj GetDataToSerialize()
    {
        S_Spawnpoint sp = new S_Spawnpoint();
        Vector3 p = transform.position;
        Quaternion r = transform.rotation;
        if (p.y < 0.2f)
        {
            p.y = 0.2f;
        }
        sp.position = p;
        sp.rotation = r.eulerAngles;
        return sp;
    }
}
