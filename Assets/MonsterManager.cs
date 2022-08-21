using DG.Tweening;
using Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager : HPObject
{
    public List<ArmJoint> startArmJoints;
    public List<ArmJoint> lockedArmJoints;
    public List<ArmJoint> unlockedArmJoints;
    public List<EyeJoint> lockedEyeJoints;
    public List<EyeJoint> unlockedEyeJoints;
    public List<MouthJoint> lockedMouthJoints;
    public List<MouthJoint> unlockedMouthJoints;
    public List<AntennaJoint> lockedAntennaJoints;

    public List<GameObject> bodys;

    public SpriteRenderer[] bodyRenders;
    bool hasMouth = false;
    public bool hasAtenna = false;
    public void upgrade(MonsterUpgradeInfo info)
    {
        Debug.Log("buy " + info.upgradeName);
        switch (info.upgradeName)
        {
            case "Add Arm":
                ArmJoint joint3 = null;
                if (info.currentLevel < 2)
                {
                    joint3 = startArmJoints[info.currentLevel];
                    lockedArmJoints.Remove(startArmJoints[info.currentLevel]);
                }
                else
                {
                    //joint3 = lockedArmJoints[ lockedArmJoints.Count-1];
                    joint3 = lockedArmJoints[Random.Range(0, lockedArmJoints.Count)];

                }
                joint3.init();
                lockedArmJoints.Remove(joint3);
                unlockedArmJoints.Add(joint3);
                var child = joint3.transform.GetChild(0);
                var newJoint = child.GetComponentInChildren<ArmJoint>();
                lockedArmJoints.Add(newJoint);

                UpgradeMonsterManager.Instance.monsterUpgradeDict["Improve Arm"].maxLevel += 2;

                GameLoopManager.Instance.addDialogue(false, "battleWithArm_start");
                break;
            case "Improve Arm":
                var joint4 = unlockedArmJoints[Random.Range(0, unlockedArmJoints.Count)];
                joint4.upgrade();
                if (joint4.atMaxLevel())
                {
                    unlockedArmJoints.Remove(joint4);
                }

                GameLoopManager.Instance.addDialogue(false, "magic");
                //UpgradeMonsterManager.Instance.monsterUpgradeDict["Improve Arm"].maxLevel -= 1;
                break;
            case "Add Eye":

                var joint = lockedEyeJoints[Random.Range(0, lockedEyeJoints.Count)];
                joint.init();
                lockedEyeJoints.Remove(joint);
                unlockedEyeJoints.Add(joint);
                break;

            case "Improve Eye":
                var joint5 = unlockedEyeJoints[Random.Range(0, unlockedEyeJoints.Count)];
                joint5.upgrade();
                if (joint5.atMaxLevel())
                {
                    unlockedEyeJoints.Remove(joint5);
                }
                break;
            case "Add Mouth":
                var joint2 = lockedMouthJoints[Random.Range(0, lockedMouthJoints.Count)];
                joint2.init();
                lockedMouthJoints.Remove(joint2);
                hasMouth = true;
                break;

            //case "Improve Mouth":
            //    var joint6 = unlockedMouthJoints[Random.Range(0, unlockedMouthJoints.Count)];
            //    joint6.upgrade();
            //    if (joint6.atMaxLevel())
            //    {
            //        unlockedMouthJoints.Remove(joint6);
            //    }
            //    break;

            case "Add Antenna":
                var joint7 = lockedAntennaJoints[Random.Range(0, lockedAntennaJoints.Count)];
                joint7.init();
                lockedAntennaJoints.Remove(joint7);
                hasAtenna = true;
                GameLoopManager.Instance.addDialogue(false, "battleWithEar");
                break;
            case "Add Body":
                bodys[info.currentLevel].SetActive(true);
                var body = bodys[info.currentLevel];
                lockedArmJoints.AddRange(body.GetComponentsInChildren<ArmJoint>());
                UpgradeMonsterManager.Instance.monsterUpgradeDict["Add Arm"].maxLevel += body.GetComponentsInChildren<ArmJoint>().Length;
                lockedEyeJoints.AddRange(body.GetComponentsInChildren<EyeJoint>());
                UpgradeMonsterManager.Instance.monsterUpgradeDict["Add Eye"].maxLevel += body.GetComponentsInChildren<EyeJoint>().Length;
                lockedMouthJoints.AddRange(body.GetComponentsInChildren<MouthJoint>());
                UpgradeMonsterManager.Instance.monsterUpgradeDict["Add Mouth"].maxLevel += body.GetComponentsInChildren<MouthJoint>().Length;
                lockedAntennaJoints.AddRange(body.GetComponentsInChildren<AntennaJoint>());
                UpgradeMonsterManager.Instance.monsterUpgradeDict["Add Antenna"].maxLevel += body.GetComponentsInChildren<AntennaJoint>().Length;
                break;
            default:
                Debug.LogError("not support buy");
                break;
        }
        SFXManager.Instance.playGrowClip();
        maxhp += info.hp;
        info.currentLevel++;
        
    }

    void findAllJoints()
    {
        lockedArmJoints = GetComponentsInChildren<ArmJoint>().ToList();
        lockedEyeJoints = GetComponentsInChildren<EyeJoint>().ToList();
        lockedMouthJoints = GetComponentsInChildren<MouthJoint>().ToList();
        lockedAntennaJoints = GetComponentsInChildren<AntennaJoint>().ToList();
        unlockedArmJoints = new List<ArmJoint>();
    }

    public float generalShakeTIme = 3;
    public float generalShakeIntense = 0.2f;
    public float hitShakeIntense = 0.4f;


    public override void Start()
    {
        base.Start();
        findAllJoints();
        transform.DOShakeScale(generalShakeTIme, generalShakeIntense,1,90,false).SetLoops(-1);
    }

    public override void die()
    {
        base.die();
        GameLoopManager.Instance.battleEnd(false);

        SFXManager.Instance.playMonsterDieClip();
    }

    public override void getDamage(float d)
    {
        if (GameLoopManager.Instance.isInBuildMode)
        {
            return;
        }
        base.getDamage(d);

        PopupTextManager.Instance.ShowPopupNumber(bodyRenders[0].transform.position, (int)d, d);
        EventPool.Trigger<float, float>("changeHP", currentHP,maxhp);

        if (hasMouth)
        {

            SFXManager.Instance.playMonsterHurtClip();
        }

        foreach(var render in bodyRenders)
        {
            render.transform.DOKill();
            render.transform.localScale = Vector3.one;
            render.transform.DOShakeScale(0.3f, hitShakeIntense);
        }
    }

    public void  restoreFromBattle()
    {
        currentHP = maxhp;
        isDead = false;
        EventPool.Trigger<float, float>("changeHP", currentHP, maxhp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
