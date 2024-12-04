using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.155f, -0.15f, 0);
    Quaternion lefrRot = Quaternion.Euler(0, 0, -15);
    Quaternion lefrRotReverset = Quaternion.Euler(0, 0, -180+15);

    private void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool isReverse = player.flipX;

        if(isLeft) //근접무기
        {
            transform.localRotation = isReverse ? lefrRotReverset : lefrRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
