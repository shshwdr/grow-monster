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

    GameObject armObject;

    public int level = 0;
    public int damage()
    {
        return level + 1;
    }
    public void init()
    {
        var parentJoint = transform.parent.parent.GetComponent<ArmJoint>();
        if (parentJoint)
        {
            depth = parentJoint.depth + 1;
        }

        if (!OnRight)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        armObject = Instantiate(armPrefabs[0], transform.position, transform.rotation, transform);
        armObject.GetComponent<Arm>().init(level,depth);
    }

    public void upgrade()
    {
        //transform.GetChild(0) arm in the joint
        //transform.GetChild(0).GetChild(0) arm joint in lower layer
        //transform.GetChild(0).GetChild(0) arm render in lower layer



        //var childJoint = transform.GetChild(0).GetChild(1);

        //childJoint.parent = null;
        //Destroy(transform.GetChild(0).gameObject);
        //level++;
        //init();
        //StartCoroutine(test(childJoint));

        level++;
        GetComponentInChildren<Arm>().init(level, depth);
    }

    //IEnumerator test(Transform childJoint)
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    //remove new added joint
    //    var jointPosition = transform.GetChild(0).GetChild(1).localPosition;
    //    Destroy(transform.GetChild(0).GetChild(1).gameObject);
    //    //move old joint under new added child
    //    childJoint.parent = transform.GetChild(0);
    //    childJoint.transform.localPosition = jointPosition;
    //}

    public bool atMaxLevel()
    {
        return level == 2;
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
