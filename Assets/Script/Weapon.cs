using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;


            default:
                break;
        }

        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(11, 1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();
    }

    public void Init()
    {
        switch(id)
        {
            case 0:
                speed = -150; // 시계방향 회전을 위해 음수
                Batch();

                break;

            case 1:
                break;

            case 2:
                break;

            case 3:
                break;

            default: 
                break;
        }
    }

    void Batch()
    {
        for(int i = 0; i < count; i++)
        {
            Transform bullet;
            
            if(i<transform.childCount)
                bullet = transform.GetChild(i);
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId, false).transform;
                bullet.parent = transform;
            }
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1);
        }
    }
}
