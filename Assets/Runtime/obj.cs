using System.Xml.Serialization;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The editor scripts are wrappers around these objects.
/// To customize what data to include in serialization, subclass this.
/// </summary>

[XmlInclude(typeof(S_Tank))]
[XmlInclude(typeof(S_Wall))]
[XmlInclude(typeof(S_WallBreakable))]
[XmlInclude(typeof(S_Spawnpoint))]
[XmlInclude(typeof(S_Gate))]
[XmlInclude(typeof(S_Hole))]
public class obj
{
    public uint id;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public List<obj> subObjects;
}
