using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public enum GateType { AND, OR, XOR, NOT, NAND, NOR, XNOR}

public class S_Gate : S_LogicSys {
    public ushort type;
}

public class LogicGate : LogicComponent
{
    public GateType Type;

    public override obj GetDataToSerialize()
    {
        var data = new S_Gate();
        data.type = (ushort)Type;
        data.outputs = new List<uint>();

        //add outputs
        foreach(var output in Outputs)
        {
            if (output != null)
            {
                data.outputs.Add(output.id);
            }
        }

        return data;
    }
}
