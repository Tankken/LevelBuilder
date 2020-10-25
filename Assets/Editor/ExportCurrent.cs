using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Serialization;
using System;
using UnityEditor.SceneManagement;

public class ExportCurrent : EditorWindow
{
    [MenuItem("Level Builder Tools/Quick Export Current Level _%#B")]
    public static void WriteCurrentQuick()
    {
        string outpath;
        try
        {
            outpath = System.IO.File.ReadAllText("BuilderPrefs.txt");
        }
        catch (Exception)
        {
            //could not read
            outpath = EditorUtility.OpenFolderPanel("Select output folder","","");
            if (outpath == "")
            {
                return;
            }
            File.WriteAllText("BuilderPrefs.txt",outpath);
        }
        WriteLevel(SceneManager.GetActiveScene(), outpath);
    }

    [MenuItem("Level Builder Tools/Export Current Level")]
    public static void WriteCurrent()
    {
        string outpath = EditorUtility.OpenFolderPanel("Select output folder", "", "");
        if (outpath == "")
        {
            return;
        }
        File.WriteAllText("BuilderPrefs.txt", outpath);
        WriteLevel(SceneManager.GetActiveScene(), outpath);
    }

    [MenuItem("Level Builder Tools/Export Level Pack")]
    public static void ExportLevelpack()
    {
        //get directory to write to
        string outpath = EditorUtility.OpenFolderPanel("Select output folder", "", "");
        if (outpath == "") { return; }

        string scenepath;
        PackMeta packjson;
        {
            FileInfo currentScene = new FileInfo(SceneManager.GetActiveScene().path);
            scenepath = currentScene.DirectoryName;
            //read the pack.json
            packjson = JsonUtility.FromJson<PackMeta>(File.ReadAllText(Path.Combine(scenepath, "pack.json")));
        }

        //make the directory
        outpath = Path.Combine(outpath,packjson.name);
        Directory.CreateDirectory(outpath);

        //loop through the scenes
        var scenes = Directory.GetFiles(scenepath,"*.unity");
        string pwd = Environment.CurrentDirectory;
        foreach (var scene in scenes)
        {
            string relative = scene.Substring(pwd.Length + 1).Replace(@"\","/");
            EditorSceneManager.OpenScene(relative);
            WriteLevel(SceneManager.GetSceneByPath(relative),outpath);
        }

        //copy the pack json into the levelpack
        File.WriteAllText(Path.Combine(outpath,"pack.json"),JsonUtility.ToJson(packjson));

        //reveal the created level pack
        EditorUtility.RevealInFinder(outpath);
    }

    /// <summary>
    /// Write a given scene to xml. The filename is the same as the scene.
    /// </summary>
    /// <param name="scene">the scene to write</param>
    /// <param name="outputPath">the folder to write the file</param>
    public static void WriteLevel(Scene scene, string outputPath)
    {
        //get scene objects
        List<GameObject> rootObjects = new List<GameObject>();
        scene.GetRootGameObjects(rootObjects);

        //create stream writer at directory
        StreamWriter writer = new StreamWriter(outputPath +  "/" + scene.name + ".xml");
        LevelDescription ld = new LevelDescription();
        uint count = 0;
        EditorObjectBase.ResetGlobalID();

        //assign IDs
        foreach(GameObject ob in rootObjects)
        {
            var subs = ob.GetComponentsInChildren<EditorObjectBase>();
            foreach(var sub in subs)
            {
                sub.AssignId();
            }
        }

        // iterate root objects
        foreach (GameObject ob in rootObjects)
        {
            //only process EditorObjects
            EditorObjectBase editorObject = ob.GetComponent<EditorObjectBase>();
            if (editorObject != null)
            {
                ++count;
                ld.Add(editorObject.GetReadySerialize());
            }
        }
        XmlSerializer xml = new XmlSerializer(ld.GetType());
        xml.Serialize(writer.BaseStream, ld);
        writer.Close();
        Debug.Log("Wrote " + count + " objects to \"" + outputPath + "/" + scene.name + ".xml\"");
    }
}
