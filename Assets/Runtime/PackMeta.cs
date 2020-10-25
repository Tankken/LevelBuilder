using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackMeta
{ 
    public string name;     //the name of the level pack (display only)
    public string desc;     //description of the level pack to display
    public string version;  //the version of this pack (display only)
    public string author;   //the creator of the pack   (display only)
    public int mode;        //the number corresponding to the gamemode this pack is for
    public int formatVersion; //the version number corresponding to the game levelpack format version, for detecting if packs are readable or not
    public string[] levels; //array of all the levels in this pack, in the desired order
}
