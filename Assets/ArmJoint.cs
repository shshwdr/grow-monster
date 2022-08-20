using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmJoint : MonoBehaviour
{

    public GameObject[] armPrefabs;
    public int depth = 0;

    public float startRotation = 0;
    public float endRotation = 90;
    public float rotateTime = 5f;
    public bool OnRight = true;

    public void init()
    {
        var parentJoint = transform.parent.parent.GetComponent<ArmJoint>();
        if (parentJoint)
        {
            depth = parentJoint.depth + 1;
        }
        if (depth >= armPrefabs.Length)
        {
            return;
        }

        if (!OnRight)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        var go = Instantiate(armPrefabs[depth], transform.position, transform.rotation, transform);
    }



    void addAllArms()
    {
        //Destroy(transform.GetChild(0).gameObject);

        var parentJoint = transform.parent.parent.GetComponent<ArmJoint>();
        if (parentJoint)
        {
            depth = parentJoint.depth + 1;
        }
        if (depth >= armPrefabs.Length)
        {
            return;
        }


        if (!OnRight)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }

        var go = Instantiate(armPrefabs[depth], transform.position, transform.rotation, transform);
    }

    // Start is called before the first frame update
    void Start()
    {

        transform.DOLocalRotate(new Vector3(0, 0, endRotation), rotateTime).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
