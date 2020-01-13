using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private int i = 1;
    [SerializeField] private bool isOnWall;
    private bool isGrounded;
    private bool isRight;
    private float xAxis;

    [Header("Floats")]
    [SerializeField] float movementSpeed;
    [Range(0f,5f)]
    [SerializeField] float radius;
    [Range(0f, 5f)]
    [SerializeField] float wallJumpRadius;
    [SerializeField] float jumpForce;
    [SerializeField] float wallJumpForce;
    [SerializeField] float sideWallJumpForce;
    [SerializeField] float jumpWaitTime;
    [SerializeField] float wallSlidingForce;
    [SerializeField] float timer;
    [SerializeField] float timeBtwShoot;
    [SerializeField] float slidingParticleDestroyTime;
    [SerializeField] float recoilForce;
    [Range(0,200f)]
    [SerializeField] float rayDistance;

    public int coins = 0;
    public int health = 10;
  //  [SerializeField] float runningParticleDestroyTime;

    [Header("Vector")]
    [SerializeField] Vector2 wallJumpDirection;
    [SerializeField] Vector2 sideWallJumpDirection;

    private GameObject instance;
    private GameObject instanceForRunningParticle;
    [Header("GameObjects")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject slidingParticles;
  //  [SerializeField] GameObject runningParticle;

    [Header("Transforms")]
    [SerializeField] Transform jumpPoint;
    [SerializeField] Transform wallPoint;
    [SerializeField] Transform shootPoint;
    [SerializeField] Transform slidingParticlesPoint;
    [SerializeField] Transform cullingPoint;
  //  [SerializeField] Transform runningParticlePosition;

    [Header("LayerMasks")]
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask Wall;
    [SerializeField] LayerMask Grass;

    [SerializeField] CinemachineVirtualCamera virtaulCamera;
    private CinemachineBasicMultiChannelPerlin virtualNoiseCamera;

    [SerializeField] float elapsedTime;
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeAmplitude;
    [SerializeField] float shakeFrequency;

    Rigidbody2D rb;
    Animator animator;
    DialougeManager dm;
   // RaycastHit2D raycast;
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        dm = GameObject.FindGameObjectWithTag("DialougeManager").GetComponent<DialougeManager>();
        transform.position = gameManager.lastCheckPointPosition;

        if(virtaulCamera != null)
        {
            virtualNoiseCamera = virtaulCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }

        timer = timeBtwShoot;

        sideWallJumpDirection.Normalize();
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(jumpPoint.position, radius, ground);
        isOnWall = Physics2D.OverlapCircle(wallPoint.position, wallJumpRadius, Wall);

        if(xAxis == 0)
        {
            animator.SetBool("isRunning", false);
        }else if(dm.animator.GetBool("IsOpen") == false)
        {
            animator.SetBool("isRunning", true);
           // instanceForRunningParticle = Instantiate(runningParticle, runningParticlePosition.position, runningParticlePosition.rotation);
        }
      //  Destroy(runningParticle, runningParticleDestroyTime);

        if(xAxis < 0 && !isRight)
        {
            Flip();
        }else if(xAxis > 0 && isRight)
        {
            Flip();
        }

        // For sliding..
        if(isOnWall)
        {
            animator.SetBool("isWallSliding", true);
            if(rb.velocity.y < -wallSlidingForce)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingForce);
                instance = Instantiate(slidingParticles, slidingParticlesPoint.position, Quaternion.identity);
            }
        }else
        {
            animator.SetBool("isWallSliding", false);
        }
        Destroy(instance, slidingParticleDestroyTime);

        // For Shooting..
        if(Input.GetKeyDown(KeyCode.X) && xAxis == 0 && timer <= 0 && !isOnWall)
        {
            animator.SetBool("isShooting", true);
            RecoilForce();
            elapsedTime = shakeDuration;
            Shoot();
            //rb.AddForce(Vector3.left * recoilForce*recoilForce, ForceMode2D.Impulse);
            timer = timeBtwShoot;
            
        }else
        {
            timer -= Time.deltaTime;
        }
        if(Input.GetKeyUp(KeyCode.X) && xAxis == 0)
        {
            animator.SetBool("isShooting", false);
        }

        // For running and shooting..
        if(Input.GetKeyDown(KeyCode.X) && xAxis != 0 && timer <= 0)
        {
            animator.SetBool("isRunningShooting", true);
            RecoilForce();
            elapsedTime = shakeDuration;
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

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene(0);
        }

        // Doing this because is both are true then the player will do a double jump..
        if(isGrounded && isOnWall)
        {
            isGrounded = false;
        }
        if(dm.animator.GetBool("IsOpen") == true)
        {
            movementSpeed = 0f;
        }else
        {
            movementSpeed = 250f;
        }

        CameraShake(); // we are calling camera Shake function every frame but setting its value only when triggered.
    }

    private void FixedUpdate()
    {
        // For movement..
        rb.velocity = new Vector2(xAxis * movementSpeed * Time.fixedDeltaTime, rb.velocity.y);

        if(Input.GetKeyDown(KeyCode.Z) && isGrounded && xAxis == 0)
        {
            animator.SetTrigger("TakeOff");
            Invoke("IdleJump", jumpWaitTime);
            Invoke("jumpSound", jumpWaitTime);
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
            FindObjectOfType<AudioManager>().Play("Jump");
        }
        else if(isGrounded)
        {
            animator.SetBool("isRunningJumping", false);
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            WallJump();
        }

        //for side wall jump(just like dash but not that speed)..
        if(Input.GetKey(KeyCode.Z) && xAxis != 0)
        {
            SideWallJump();
        }

        if(health <= 0)
        {
            gameObject.SetActive(false);
        }

        if(transform.position.y <= -12.5f)
        {
            health = 0;
        }
    }

    //For Fall Damage..//
    //
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.transform.CompareTag("Ground") || 
            collision.collider.transform.CompareTag("WallTag"))
        {
            if(collision.relativeVelocity.y > 25f)
            {
                health--;
            }
        }
    }

    private void  jumpSound() // For involing jump sound when we do an idle jump..
    {
        FindObjectOfType<AudioManager>().Play("Jump");
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
            Vector2 addForce = new Vector2(wallJumpDirection.x * wallJumpForce * i * Time.deltaTime, 
                wallJumpDirection.y * wallJumpForce * Time.fixedDeltaTime);
            rb.AddForce(addForce, ForceMode2D.Impulse);
        }else if(isOnWall && !isRight)
        {
            Vector2 addForce = new Vector2(wallJumpDirection.x * wallJumpForce * -1 * Time.deltaTime, 
                wallJumpDirection.y * wallJumpForce * Time.fixedDeltaTime);
            rb.AddForce(addForce, ForceMode2D.Impulse);
        }
      //  Debug.Log("Simple Wall jump performed");
    }

    private void SideWallJump()
    {
        if(isOnWall && isRight && xAxis > 0)
        {
            Vector2 addForce = new Vector2(sideWallJumpForce * sideWallJumpDirection.x,
               sideWallJumpForce * sideWallJumpDirection.y);
            rb.AddForce(addForce, ForceMode2D.Impulse);
        }else if(isOnWall && !isRight && xAxis < 0)
        {
            Vector2 addForce = new Vector2(sideWallJumpForce * -sideWallJumpDirection.x ,
                sideWallJumpForce * sideWallJumpDirection.y);
            rb.AddForce(addForce, ForceMode2D.Impulse);
        }

      //  Debug.Log("Side wall jump performed");
    }

    private void Shoot()
    {
        Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }

    private void RecoilForce()
    {
        if(isRight)
        {
            rb.AddForce(Vector3.right * recoilForce, ForceMode2D.Force);
          //  Debug.Log("Right Force Added..");
        }else if(!isRight)
        {
            rb.AddForce(-Vector3.right * recoilForce, ForceMode2D.Force);
           // Debug.Log("Left Force Added..");
        }
    }

    private void CameraShake()
    {
        if(elapsedTime > 0)
        {
            virtualNoiseCamera.m_AmplitudeGain = shakeAmplitude;
            virtualNoiseCamera.m_FrequencyGain = shakeFrequency;
            elapsedTime -= Time.deltaTime;
        }
        else
        {
            elapsedTime = 0;
            virtualNoiseCamera.m_FrequencyGain = 0;
            virtualNoiseCamera.m_AmplitudeGain = 0;
        }
    }

    private void Flip()
    {
        isRight = !isRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmosSelected()
    {
       // Gizmos.DrawWireSphere(jumpPoint.position, radius);
      //  Gizmos.DrawWireSphere(wallPoint.position, wallJumpRadius);
    }
}
