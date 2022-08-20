using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthJoint : MonoBehaviour
{
    public GameObject[] eyePrefabs;
    int currentLevel = 0;
    int maxLevel = 2;
    public void init()
    {
        var go = Instantiate(eyePrefabs[0], transform.position, transform.rotation, transform);
    }

    public void upgrade()
    {
        GetComponent<Mouth>().upgrade();
    }
    public bool atMaxLevel()
    {
        return currentLevel >= maxLevel;
    }
}
