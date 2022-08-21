using PixelCrushers.DialogueSystem;
using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopManager : Singleton<GameLoopManager>
{
    public  bool isInBuildMode = false;
    public MonsterManager monster;

    public List<string> battleStartDialogue = new List<string>();
    public List<string> battleEndDialogue = new List<string>();
    public HashSet<string> dialogueSet = new HashSet<string>();

    int battleIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //start dialogue
        //start battle
        //loop:
        //when battle end, start build mode
        // when finish build, start battle
        monster = GameObject.FindObjectOfType<MonsterManager>();
        StartCoroutine(startBuildMode());

        //StartCoroutine(startBattleLoop());
    }

    IEnumerator  startBattleLoop()
    {
        if (battleStartDialogue.Count > 0)
        {
            DialogueManager.StartConversation(battleStartDialogue[0]);
            battleStartDialogue.RemoveAt(0);
        }

        isInBuildMode = false;
        yield return new WaitForSeconds(0.1f);
        EventPool.Trigger("startBattle");
        EventPool.Trigger("updateResource");
        EnemyGeneratorManager.Instance.generate();
        monster.restoreFromBattle();
        MusicManager.Instance.startBattle();

        if(battleIndex == 0)
        {
            addDialogue(true, "battle1_end"); 
        }

        battleIndex++;
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

    public void addDialogue(bool isEnd, string dialogue)
    {
        if(dialogueSet.Contains(dialogue)){
            return;
        }
        if (isEnd)
        {
            battleEndDialogue.Add(dialogue);
        }
        else
        {
            battleStartDialogue.Add(dialogue);
        }
        dialogueSet.Add(dialogue);
    }

    IEnumerator startBuildMode()
    {

        if (battleEndDialogue.Count > 0)
        {
            DialogueManager.StartConversation(battleEndDialogue[0]);
            battleEndDialogue.RemoveAt(0);
        }

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
