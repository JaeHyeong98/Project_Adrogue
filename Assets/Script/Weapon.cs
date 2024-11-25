﻿using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

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

            case 1:
                timer += Time.deltaTime;

                if(timer > speed)
                {
                    timer = 0;
                    Fire();
                }
                break;

            default:
                break;
        }

        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(11, 1);
        }
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
                speed = 0.3f; // 발사 속도
                break;

            case 2:
                break;

            case 3:
                break;

            default: 
                break;
        }
    }
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();
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

            bullet.GetComponent<Bullet>().Init(damage, -1,Vector3.zero);
        }
    }

    private void Fire()
    {
        if (player.scanner.nearestTarget == null)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId,false).transform;
        bullet.position = transform.position;

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
