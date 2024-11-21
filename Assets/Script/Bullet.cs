using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float per;

    public void Init(float damage, float per)
    {
        this.damage = damage;
        this.per = per;
    }
}
