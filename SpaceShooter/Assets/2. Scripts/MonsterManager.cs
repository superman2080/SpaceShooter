using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public int monsterCnt;
    public float monsterItv;
    public GameObject[] spawnTr;
    public GameObject monster;
    private bool isStarting;

    private void Start()
    {
        FindAllSpawnPos();
    }

    public void FindAllSpawnPos()
    {
        spawnTr = GameObject.FindGameObjectsWithTag("SPAWNPOS");
    }

    public void SpawnMonsters(int monsterNum, float itv)
    {
        if(!isStarting)
            StartCoroutine(SpawnMonstersCor(monsterNum, itv));
    }

    private IEnumerator SpawnMonstersCor(int mN, float itv)
    {
        isStarting = true;
        int cnt = 0;
        while (true)
        {
            Instantiate(monster, spawnTr[Random.Range(0, spawnTr.Length)].transform.position, Quaternion.identity);
            cnt++;
            yield return new WaitForSeconds(itv);
            if (cnt >= mN)
                break;
        }
        isStarting = false;
    }
}
