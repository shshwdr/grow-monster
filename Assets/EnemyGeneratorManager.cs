using Pool;
using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyGeneratorInfo
{
    public int villager;
    public int soldier;
    public int magic;
    public int lady;
}
public class EnemyGeneratorManager : Singleton<EnemyGeneratorManager>
{
    public GameObject enemyPrefab;

    public float generateDistance = 5;
    public int currentLevel = 0;

    List<EnemyGeneratorInfo> enemyGeneratorInfos = new List<EnemyGeneratorInfo>();


    public List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemyGeneratorInfos = CsvUtil.LoadObjects<EnemyGeneratorInfo>("enemyGenerate");
    }

    public void generate()
    {
        StartCoroutine(generateYield());
    }

    void addEnemy(string type)
    {
        var go = Instantiate(enemyPrefab, transform.position + new Vector3(generateDistance + Random.Range(-2f, 2f), Random.Range(-0.3f, 0.3f), 0), Quaternion.identity);
        generateDistance = -generateDistance;
        go.GetComponent<Human>().init(type);
        enemies.Add(go);
        if (generateDistance < 0)
        {
            go.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    IEnumerator generateYield()
    {
        yield return new WaitForSeconds(0.1f);
        enemies = new List<GameObject>();
        var currentInfo = enemyGeneratorInfos[currentLevel];
        for (int i = 0; i < currentInfo.villager; i++)
        {
            yield return new WaitForSeconds(0.1f);
            addEnemy("villager");
        }
        for (int i = 0; i < currentInfo.magic; i++)
        {
            yield return new WaitForSeconds(0.1f);
            addEnemy("magic");
            GameLoopManager.Instance.addDialogue( false,"magic");
        }
        for (int i = 0; i < currentInfo.soldier; i++)
        {
            yield return new WaitForSeconds(0.1f);
            addEnemy("soldier");
        }


        for (int i = 0; i < currentInfo.lady; i++)
        {
            yield return new WaitForSeconds(0.1f);
            addEnemy("lady");
        }

    }

    public void removeEnemy(GameObject go)
    {
        enemies.Remove(go);
        if(enemies.Count == 0)
        {
            GameLoopManager.Instance.battleEnd(true);
            upgradeLevel();
        }
    }

    public void clear()
    {
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public void upgradeLevel()
    {
        currentLevel++;
        EventPool.Trigger("changeLevel");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
