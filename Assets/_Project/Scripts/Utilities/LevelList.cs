using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelList", menuName = "LevelList")]
public class LevelList : ScriptableObject
{
    public List<GameObject> all = new List<GameObject>();

    public GameObject LoopLevelsByIndex(int _level)
    {
        int index = (_level - 1 >= 0) ? (_level - 1) % all.Count : ((_level - 1) % all.Count) + all.Count;

        return all[index];
    }
}
