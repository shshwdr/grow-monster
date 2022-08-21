using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBullet : MonoBehaviour
{
    public AnimationCurve curve;
    public float moveSpeed = 2;
    float time;

    Vector3 startPosition;
    public Transform targetTrans;

    public int damage = 1;



    // Start is called before the first frame update
    void Start()
    {
        var pos = transform.position;
        startPosition = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetTrans)
        {
            Destroy(gameObject);
        }
        time += Time.deltaTime * moveSpeed;
        Vector3 pos = Vector3.Lerp(startPosition, targetTrans.position, time);
        pos.y += curve.Evaluate(time);
        transform.position = pos;

        if (time >= 1)
        {

            targetTrans.GetComponent<Human>().getDamage(-damage);
            //GameLoopManager.Instance.monster.getDamage(damage);
            Destroy(gameObject);

        }
    }
}
