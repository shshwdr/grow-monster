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

    public ArmJoint parentJoint;

    GameObject armObject;

    public int level = 0;
    public int damage()
    {
        return level + 1;
    }
    public void init()
    {
        parentJoint = transform.parent.parent.GetComponent<ArmJoint>();
        if (parentJoint)
        {
            depth = parentJoint.depth + 1;
        }

        //if (!OnRight)
        //{
        //    transform.localScale = new Vector3(1, -1, 1);
        //}
        armObject = Instantiate(armPrefabs[0], transform.position, transform.rotation, transform);
        armObject.GetComponent<Arm>().init(level,depth);
        armObject.GetComponentInChildren<ControlByPlayer>().armjoint = this;
        if (parentJoint)
        {
            armObject.GetComponentInChildren<ControlByPlayer>().parent = parentJoint.GetComponentInChildren<ControlByPlayer>();
            parentJoint.GetComponentInChildren<ControlByPlayer>().child = armObject.GetComponentInChildren<ControlByPlayer>();
        }
    }

    public void upgrade()
    {

        level++;
        GetComponentInChildren<Arm>().init(level, depth);
    }

    public bool atMaxLevel()
    {
        return level == 2;
    }



    // Start is called before the first frame update
    void Start()
    {

       // transform.DOLocalRotate(new Vector3(0, 0, endRotation), rotateTime).SetLoops(-1, LoopType.Yoyo);
    }
    public float speed = 5;

    

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, (endRotation - startRotation) / 2 * Mathf.Sin(Time.time * speed* speedScale) + startRotation + (endRotation - startRotation) / 2);
    }
    float controlSpeed = 2;
    float normalSpeed = 1;
    float speedScale = 1;
    public void speedUP()
    {
        speedScale = controlSpeed;
    }
    public void speedDown()
    {
        speedScale = normalSpeed;
    }
}
