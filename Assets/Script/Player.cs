using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 2.3f;
    public bool isRight = true;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;

    Rigidbody2D rigid;
    SpriteRenderer spriteR;
    Animator animator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    private void OnEnable()
    {
        speed *= Character.Speed;
        animator.runtimeAnimatorController = animCon[GameManager.instance.playerID];
    }

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive) return;
        rigid.linearVelocity = inputVec * speed;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive) return;
        animator.SetFloat("Speed", inputVec.magnitude);

        if (isRight && inputVec.x < 0)
        {
            spriteR.flipX = true;
            isRight = false;
        }
        else if (!isRight && inputVec.x > 0)
        {
            spriteR.flipX = false;
            isRight = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!GameManager.instance.isLive) return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if(GameManager.instance.health < 0 )
        {
            for(int i = 2; i< transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}
