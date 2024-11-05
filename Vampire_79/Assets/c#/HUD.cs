using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public enum InfoType {Exp , Level , Kill , Time , Health};

    public InfoType type;

    Text myText;
    Slider mySilder;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySilder = GetComponent<Slider>();
    }


    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                // 슬라이더에 적용할 값 : 현재 경험치 / 최대 경험치
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                mySilder.value = curExp / maxExp;
                break;
            case InfoType.Level:
                // 문자열로 들어가야 함 {0:F0} >> {데이터의 인덱스 순서:숫자에 대한 포맷}
                myText.text = string.Format("Lv.{0:F0}",GameManager.instance.level);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}",GameManager.instance.kill);
                break;
            case InfoType.Time:
                // 흐르는 시간이 아닌 남은 시간부터 구하기
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60); 
                int sec = Mathf.FloorToInt(remainTime % 60); 
                myText.text = string.Format("{0:D2}:{1:D2}", min , sec);
                break;
            case InfoType.Health:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySilder.value = curHealth / maxHealth;
                break;                   
        }
    }



}
