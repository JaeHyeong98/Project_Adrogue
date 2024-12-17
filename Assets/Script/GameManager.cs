using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 20f;

    [Header("# Player Info")]
    public int playerID;
    public int level;
    public int kill;
    public int exp;
    public float health = 100;
    public float maxHealth = 100;
    public int[] nextExp = { 10, 20, 60, 100, 150, 210, 280, 360, 450, 600};

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResulte;
    public GameObject enemyCleaner;

    private void Awake()
    {
        instance = this;
    }

    public void GameStart(int id)
    {
        playerID = id;
        maxHealth = 100;
        health = maxHealth;

        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerID % 2);
        Resume();

        AudioManager.instance.PlayBGM(true);
        AudioManager.instance.PlaySFX(AudioManager.SFX.Select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        AudioManager.instance.PlayBGM(false);
        AudioManager.instance.PlaySFX(AudioManager.SFX.Lose);
        uiResulte.gameObject.SetActive(true);
        uiResulte.Lose();
        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryCoroutine());
    }

    IEnumerator GameVictoryCoroutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        AudioManager.instance.PlayBGM(false);
        AudioManager.instance.PlaySFX(AudioManager.SFX.Win);
        uiResulte.gameObject.SetActive(true);
        uiResulte.Win();
        Stop();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }


    private void Update()
    {
        if (!isLive) return;

        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp()
    {
        if (!isLive) return;

        exp++;

        if(exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
