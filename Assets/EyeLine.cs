using DG.Tweening;
using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLine : MonoBehaviour
{
    public float endRotation = 90;
    public float rotateTime = 3;

    // Start is called before the first frame update
    void Start()
    {

        EventPool.OptIn("startBuild", hide);
        EventPool.OptIn("startBattle", show);
        transform.DOLocalRotate(new Vector3(0, 0, endRotation), rotateTime).SetLoops(-1, LoopType.Yoyo);
    }

    void show()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }
    void hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "human")
        {
            collision.GetComponent<Human>().getDamage(1);
        }
    }
}
