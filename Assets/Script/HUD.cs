using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        Exp,
        Level,
        Kill,
        Time,
        Health
    }

    public InfoType type;
    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch(type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                mySlider.value = curExp / maxExp;
                break;

            case InfoType.Level:
                myText.text = "Lv. " + GameManager.instance.level;
                break; 

            case InfoType.Kill:
                myText.text = "" + GameManager.instance.kill;
                break; 

            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}",min,sec);
                break;

            case InfoType.Health:
                float curHp = GameManager.instance.health;
                float maxHp = GameManager.instance.maxHealth;
                mySlider.value = curHp / maxHp;
                break;
        }
    }
}
