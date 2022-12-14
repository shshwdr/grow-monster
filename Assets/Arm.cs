using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    float damageTime = 0.3f;
    float damageTimer = 0f;
    public int level;

    public Sprite[] colors1;
    public Sprite[] colors2;
    public Sprite[] colors3;

    public float specialStart = 0;
    public float specialEnd = 90;

    // Start is called before the first frame update
    void Awake()
    {
        if (transform.parent. GetComponent<ArmJoint>().OnRight)
        {
            GetComponentInChildren<ArmJoint>().OnRight = true;
            //GetComponentInChildren<ArmJoint>().transform.localRotation.SetEulerAngles( new Vector3(0, 0, 30));
            GetComponentInChildren<ArmJoint>().startRotation = specialStart;
            GetComponentInChildren<ArmJoint>().endRotation = specialEnd;
        }
        else
        {

            GetComponentInChildren<ArmJoint>().OnRight = false;
        }
    }

    public void init(int level,int depth)
    {
        switch(level)
        {
            case 0:
                GetComponentInChildren<SpriteRenderer>().sprite = colors1[depth % 4];
                break;
            case 1:
                GetComponentInChildren<SpriteRenderer>().sprite = colors2[depth % 4];
                break;
            case 2:
                GetComponentInChildren<SpriteRenderer>().sprite = colors3[depth % 4];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        damageTimer += Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (damageTimer > damageTime)
        {

            damageTimer = 0;
            if (collision.tag == "human")
            {
                collision.GetComponent<Human>().getDamage(GetComponentInParent<ArmJoint>().damage());
                if (GetComponentInParent<ArmJoint>().damage() > 1)
                {
                    Debug.Log("do damage " + GetComponentInParent<ArmJoint>().damage());
                }
            }
        }
    }
}
