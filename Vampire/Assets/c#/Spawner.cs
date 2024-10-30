using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoint;

    public SpawnData[] SpawnData;


    int level;
    float timer;

    void Awake()
    {
        // GetComponentsInChildren 자식 오브젝트의 전체를 리스트로 가져옴 
        // 단 자기 자신을 0 부터 받가 때문에 자식 오브젝트는 1부터 시작함을 주의해야함
        spawnPoint = GetComponentsInChildren<Transform>();
    } 

    
    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f) ,spawnPoint.Length -1);

        if(timer > SpawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;  // 1부터 자식 오브젝트 시작
        enemy.GetComponent<Enemy>().Init(SpawnData[level]);
    }

}



[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}
