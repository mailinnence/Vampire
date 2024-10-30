using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public PoolManager pool;
    public player player;

    // 매 프레임 마다 더해질 변수와 최대 시간을 정해준다.
    public float gameTime;
    public float maxGameTime = 5 * 10f;


    void Awake()
    {
        // 자기 자신을 가리키게 되서 외부에서도 쉽게 가져와 사용할 수 있다.
        instance = this;
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

}


