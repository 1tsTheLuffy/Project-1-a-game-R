using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int i = 1;
    private bool isOnWall;
    private bool isGrounded;
    private bool isRight;
    private float xAxis;
    [SerializeField] float movementSpeed;
    [Range(0f,5f)]
    [SerializeField] float radius;
    [Range(0f, 5f)]
    [SerializeField] float wallJumpRadius;
    [SerializeField] float jumpForce;
    [SerializeField] float wallJumpForce;
    [SerializeField] float jumpWaitTime;
    [SerializeField] float wallSlidingForce;
    [SerializeField] float timer;
    [SerializeField] float timeBtwShoot;

    [SerializeField] Vector2 wallJumpDirection;

    [SerializeField] GameObject bullet;

    [SerializeField] Transform jumpPoint;
    [SerializeField] Transform wallPoint;
    [SerializeField] Transform shootPoint;

    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask Wall;

    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        timer = timeBtwShoot;
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(jumpPoint.position, radius, ground);
        isOnWall = Physics2D.OverlapCircle(wallPoint.position, wallJumpRadius, Wall);

        if(xAxis == 0)
        {
            animator.SetBool("isRunning", false);
        }else
        {
            animator.SetBool("isRunning", true);
        }

        if(xAxis < 0 && !isRight)
        {
            Flip();
        }else if(xAxis > 0 && isRight)
        {
            Flip();
        }

        if(isOnWall)
        {
            animator.SetBool("isWallSliding", true);
            if(rb.velocity.y < -wallSlidingForce)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingForce);
            }
        }else
        {
            animator.SetBool("isWallSliding", false);
        }

        if(Input.GetKeyDown(KeyCode.X) && xAxis == 0 && timer <= 0)
        {
            animator.SetBool("isShooting", true);
            Shoot();
            timer = timeBtwShoot;
        }else
        {
            timer -= Time.deltaTime;
        }
        if(Input.GetKeyUp(KeyCode.X) && xAxis == 0)
        {
            animator.SetBool("isShooting", false);
        }

        if(Input.GetKeyDown(KeyCode.X) && xAxis != 0 && timer <= 0)
        {
            animator.SetBool("isRunningShooting", true);
            Shoot();
            timer = timeBtwShoot;
        }else
        {
            timer -= Time.deltaTime;
        }
        if(Input.GetKeyUp(KeyCode.X) && xAxis != 0)
        {
            animator.SetBool("isRunningShooting", false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xAxis * movementSpeed * Time.fixedDeltaTime, rb.velocity.y);

        if(Input.GetKeyDown(KeyCode.Z) && isGrounded && xAxis == 0)
        {
            animator.SetTrigger("TakeOff");
            Invoke("IdleJump", jumpWaitTime);
        }
        if(isGrounded)
        {
            animator.SetBool("isJumping", false);
        }else
        {
            animator.SetBool("isJumping", true);
        }

        if(Input.GetKeyDown(KeyCode.Z) && isGrounded && xAxis != 0)
        {
            RunningJump();
            animator.SetBool("isRunningJumping", true);
        }
        else if(isGrounded)
        {
            animator.SetBool("isRunningJumping", false);
        }

        if(xAxis != 0)
        {
            Debug.Log(isRight);
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            WallJump();
        }
    }

    private void RunningJump()
    {
        rb.velocity = Vector2.up * jumpForce * Time.fixedDeltaTime;
    }

    private void IdleJump()
    {
        rb.velocity = Vector2.up * jumpForce * Time.fixedDeltaTime;
    }

    private void WallJump()
    {
        if(isOnWall && isRight)
        {
            Vector2 addForce = new Vector2(wallJumpDirection.x * wallJumpForce * i *Time.fixedDeltaTime, wallJumpDirection.y * wallJumpForce * Time.fixedDeltaTime);
            rb.AddForce(addForce, ForceMode2D.Impulse);
        }else if(isOnWall && !isRight)
        {
            Vector2 addForce = new Vector2(wallJumpDirection.x * wallJumpForce * -1 *Time.fixedDeltaTime, wallJumpDirection.y * wallJumpForce * Time.fixedDeltaTime);
            rb.AddForce(addForce, ForceMode2D.Impulse);
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }

    private void Flip()
    {
        isRight = !isRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(jumpPoint.position, radius);
        Gizmos.DrawWireSphere(wallPoint.position, wallJumpRadius);
    }
}
