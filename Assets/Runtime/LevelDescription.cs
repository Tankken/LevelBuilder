using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDescription
{
    public List<obj> objects = new List<obj>();

    public void Add(obj e){
        objects.Add(e);
    }
}
