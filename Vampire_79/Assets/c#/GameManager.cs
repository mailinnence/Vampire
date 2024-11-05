using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    
    [Header("# GameObject")]
    public PoolManager pool;
    public player player;

    [Header("# Game Controll")]
    // 매 프레임 마다 더해질 변수와 최대 시간을 정해준다.
    public float gameTime;
    public float maxGameTime = 5 * 10f;

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = {10, 30, 60, 100, 150, 210, 280, 360, 450 ,600};


    void Awake()
    {
        // 자기 자신을 가리키게 되서 외부에서도 쉽게 가져와 사용할 수 있다.
        instance = this;
    }


    void Start()
    {
        health = maxHealth;
    }



    void Update()
    {
        // 최대 시간보다 커지면 20으로 초기화한다.
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }


    }



    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }



}


