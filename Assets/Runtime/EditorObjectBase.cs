using UnityEngine;
using System.Collections.Generic;

//All serializable objects inherit from EditorObjectBase
[System.Serializable]
public abstract class EditorObjectBase : MonoBehaviour
{
    public uint id { get; private set; } = 0;
    protected static uint shared_id = 0;

    /// <summary>
    /// Return the serializable object
    /// </summary>
    public virtual obj GetReadySerialize()
    {
        var data = GetDataToSerialize();
        data.position = transform.position;
        data.rotation = transform.rotation.eulerAngles;
        data.scale = transform.localScale;
        data.id = id;

        data.subObjects = new List<obj>();
        if (transform.childCount > 0)
        {
            for(int i = 0; i < transform.childCount; ++i)
            {
                var e = transform.GetChild(i).GetComponent<EditorObjectBase>();
                if (e != null)
                {
                    //recuse 
                    data.subObjects.Add(e.GetReadySerialize());
                }
            }
        }

        return data;
    }

    /// <summary>
    /// Get the data to serialize. 
    /// </summary>
    /// <returns>The SerializedBase to serialize</returns>
    public abstract obj GetDataToSerialize();

    /// <summary>
    /// Reset the global ID tracker
    /// </summary>
    public static void ResetGlobalID()
    {
        shared_id = 0;
    }

    /// <summary>
    /// give the current object an ID
    /// </summary>
    public void AssignId()
    {
        id = shared_id++;
    }
}
