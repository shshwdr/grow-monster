using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager : Singleton<PlayerControlManager>
{
    public float ControlTime;
    public float CooldownTime;
    float timer;
    MonsterManager monster;
    // Start is called before the first frame update
    void Start()
    {
        monster = GameObject.FindObjectOfType<MonsterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EventPool.Trigger("stopControl");
            }
        }
    }

    public void control()
    {
        timer = ControlTime;
    }

    public bool canControl()
    {
        if (!monster.hasAtenna)
        {
            return false;
        }
        if (GameLoopManager.Instance.isInBuildMode)
        {
            return false;
        }
        if (timer <= 0)
        {
            return true;
        }
        return false;
    }
}
