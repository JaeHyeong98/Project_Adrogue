using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            GameManager.instance.pool.Get(0);
        }
    }

}
