using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    [SerializeField] private float speed = 2.3f;
    public bool isRight = true;


    Rigidbody2D rigid;
    SpriteRenderer spriteR;
    Animator animator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();

    }

    private void FixedUpdate()
    {
        rigid.linearVelocity = inputVec * speed;
    }

    private void LateUpdate()
    {
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
}
