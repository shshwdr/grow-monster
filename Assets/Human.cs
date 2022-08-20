using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : HPObject
{

    public SpriteRenderer spriteRender;

    float damageTime = 0.3f;
    float damageTimer = 0f;
    Animator animator;
    EnemyInfo info;

    bool startAttack = false;

    float attackTimer = 0;
    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        transform.DOMoveX(0, 10f);
        animator = GetComponent<Animator>();
    }

    public void init(string type)
    {
      
        spriteRender.sprite = Resources.Load<Sprite>("human/"+type);
        info = EnemyManager.Instance.getEnemyInfo(type);
        maxhp = currentHP = info.hp;
    }

    // Update is called once per frame
    void Update()
    {
        damageTimer += Time.deltaTime;

        if (Mathf.Abs( transform.position.x) <= info.attackRange)
        {
            startAttack = true;
        }
        if (startAttack)
        {
            //startAttack
            transform.DOKill();
            attackTimer += Time.deltaTime;
            if (attackTimer >= info.attackSpeed)
            {
                attackTimer = 0;
                //attack
                transform.DOShakeRotation(0.3f);
                Debug.Log("attack!");
                // transform.DOJump(new Vector3(0, 0, 0), 1, 1, 0.2f);
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (damageTimer > damageTime)
        {
            damageTimer = 0;
            getDamage(1);
        }
    }

    public override void getDamage(float d)
    {
        base.getDamage(d);
        animator.SetTrigger("hit");
        if (!isDead)
        {

            SFXManager.Instance.playHitClip();
        }
    }

    public override void die()
    {
        transform.DOKill();
        SFXManager.Instance.playDieClip();
        base.die();
        EnemyGeneratorManager.Instance.removeEnemy(gameObject);
        Destroy(gameObject);
    }
}
