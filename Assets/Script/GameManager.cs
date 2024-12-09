using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 20f;

    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int health = 100;
    public int maxHealth = 100;
    public int[] nextExp = { 10, 20, 60, 100, 150, 210, 280, 360, 450, 600};

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;

    private void Awake()
    {
        instance = this;
        maxHealth = 100;
        health = maxHealth;
    }


    private void Update()
    {
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
            uiLevelUp.Show();
        }
    }
}
