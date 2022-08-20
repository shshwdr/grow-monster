using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    float damageTime = 0.3f;
    float damageTimer = 0f;
    public int level;

    public Sprite[] colors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void init(int depth)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = colors[depth % 4];
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
