using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class S_LogicSys : obj
{
    public List<uint> outputs;
}

public abstract class LogicComponent : EditorObjectBase{

    public List<LogicComponent> Outputs = new List<LogicComponent>();

    private void OnDrawGizmos()
    {
        foreach(LogicComponent L in Outputs)
        {
            if (L != null)
            {
                Debug.DrawLine(transform.position, L.transform.position, Color.blue);
            }
        }
    }
}