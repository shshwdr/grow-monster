using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopManager : Singleton<GameLoopManager>
{
    bool isInBuildMode = false;
    public MonsterManager monster;
    // Start is called before the first frame update
    void Start()
    {
        //start dialogue
        //start battle
        //loop:
        //when battle end, start build mode
        // when finish build, start battle
        monster = GameObject.FindObjectOfType<MonsterManager>();
        //StartCoroutine(startBattleLoop());

        StartCoroutine(startBuildMode());
    }

    IEnumerator  startBattleLoop()
    {
        isInBuildMode = false;
        yield return new WaitForSeconds(0.1f);
        EventPool.Trigger("startBattle");
        EventPool.Trigger("updateResource");
        EnemyGeneratorManager.Instance.generate();
        monster.restoreFromBattle();
        MusicManager.Instance.startBattle();
    }

    public void battleEnd(bool win)
    {
        if (isInBuildMode)
        {
            return;
        }
        if (win)
        {
            MessageMenu.Instance.show("Victory!");
            SFXManager.Instance.playMonsterWinClip();
        }
        else
        {

            MessageMenu.Instance.show("Faild!");
            SFXManager.Instance.playHumanWinClip();
        }
        EnemyGeneratorManager.Instance.clear();
        monster.restoreFromBattle();
        StartCoroutine( startBuildMode());
    }

    IEnumerator startBuildMode()
    {
        isInBuildMode = true;
        yield return new WaitForSeconds(0.1f);
        EventPool.Trigger("updateResource");
        monster.restoreFromBattle();
        EventPool.Trigger("startBuild");

        MusicManager.Instance.startBuild();
    }

    public void stopBuildMode()
    {
        StartCoroutine(startBattleLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
