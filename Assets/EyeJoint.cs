using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeJoint : MonoBehaviour
{
    public GameObject[] eyePrefabs;
    public bool OnRight = true;
    int currentLevel = 0;
    int maxLevel = 2;
    public void init()
    {

        if (!OnRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);

        }
        var go = Instantiate(eyePrefabs[0], transform.position, transform.rotation, transform);

    }


    public void upgrade()
    {
        GetComponentInChildren<ControlByPlayerEye>().upgrade();
    }
    public bool atMaxLevel()
    {
        return currentLevel >= maxLevel;
    }
}
