using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthJoint : MonoBehaviour
{
    public GameObject[] eyePrefabs;
    public void init()
    {
        var go = Instantiate(eyePrefabs[0], transform.position, transform.rotation, transform);
    }
}
