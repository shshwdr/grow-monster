using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    public float eatTime = 3;
    public float eatTimer;
    public Transform humanParent;
    GameObject eatingHuman;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void upgrade()
    {

    }
    public void eatHuman()
    {
        float cloestDis = 1000;
        GameObject closestEnemy = null;
        foreach(var enemy in EnemyGeneratorManager.Instance.enemies)
        {
            if (Mathf.Abs(enemy.transform.position.x) < cloestDis)
            {
                cloestDis = Mathf.Abs(enemy.transform.position.x);
                closestEnemy = enemy;
            }
        }
        if (closestEnemy)
        {
            closestEnemy.transform.parent = humanParent;
            closestEnemy.transform.localPosition = Vector3.zero;
            closestEnemy.GetComponent<Human>().kill();
            closestEnemy.transform.DOJump(Vector3.zero, 0.5f, 5, eatTime);
            eatingHuman = closestEnemy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        eatTimer += Time.deltaTime;
        if (eatTimer >= eatTime)
        {
            if (eatingHuman)
            {
                eatingHuman.GetComponent<Human>().die();
            }
            eatTimer = 0;
            eatHuman();
        }
    }
}
