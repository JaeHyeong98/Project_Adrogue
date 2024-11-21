using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnData[] spawnData;

    int level;
    float timer;

    private void Awake()
    {

    }

    private void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f),spawnData.Length -1);

        if(timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0,true);
        enemy.transform.GetComponent<Enemy>().Init(spawnData[level]);
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
