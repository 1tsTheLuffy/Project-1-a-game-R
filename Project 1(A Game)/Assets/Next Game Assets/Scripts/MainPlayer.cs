using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainPlayer : MonoBehaviour
{
    public int health;
    public int wizardScore;

    private Vector2 mousePos;

    [SerializeField] float elapsedTime;
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeFrequency;
    [SerializeField] float shakeAmplitude;

    public int score = 0;

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI wizardScoreText;

    private GameObject temp;
    [SerializeField] GameObject[] bullet;
    [SerializeField] GameObject particlePrefab;
    [SerializeField] GameObject[] Managers;
    [SerializeField] GameObject destroyParticle;

    [SerializeField] Transform[] gunPoint;

    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] CinemachineBasicMultiChannelPerlin virtualNoiseCamera;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;
    Camera cam;

    private void Start()
    {
        wizardScore = Random.Range(50, 100);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        if(virtualCamera != null)
        {
            virtualNoiseCamera = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }

        healthText.text = health.ToString();
        scoreText.text = score.ToString();
        wizardScoreText.text = wizardScore.ToString();
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;

        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet[0], gunPoint[0].position, gunPoint[0].rotation);
            FindObjectOfType<AudioManager>().Play("Shoot");
            Instantiate(particlePrefab, gunPoint[0].position, gunPoint[0].rotation);
            elapsedTime = shakeDuration;
        }
        else if(Input.GetMouseButtonDown(1))
        {
            Instantiate(bullet[1], gunPoint[1].position, gunPoint[1].rotation);
            FindObjectOfType<AudioManager>().Play("Shoot");
            Instantiate(particlePrefab, gunPoint[1].position, gunPoint[1].rotation);
            elapsedTime = shakeDuration;
        }

        healthText.text = health.ToString();
        
        if(health <= 10 && health > 5)
        {
            healthText.color = Color.yellow;
        }else if(health < 5)
        {
            healthText.color = Color.red;
        }

        // Health Related Stuff..
        //*

        if (health <= 0 && (score < wizardScore))
        {
            temp = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            sr.enabled = false;
            StartCoroutine(LoadLoseScene());
           // Destroy(gameObject);
        }else if(health <= 0 && (score > wizardScore))
        {
            SceneManager.LoadScene(05);
        }
        if(health > 20)
        {
            health = 20;
        }
        if(health < 0)
        {
            health = 0;
        }

        //
        //*
        scoreText.text = score.ToString();
        Shake();

    }

    private void OnMouseDrag()
    {
        
        transform.position = mousePos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("RedEnemy"))
        {
            health -= 2;
            elapsedTime = shakeDuration;
            FindObjectOfType<AudioManager>().Play("Hit");
            StartCoroutine(Damage());
            Destroy(collision.transform.gameObject);
        }
        if(collision.CompareTag("YellowCircleEnemy"))
        {
            health -= 1;
            FindObjectOfType<AudioManager>().Play("Hit");
            StartCoroutine(Damage());
            elapsedTime = shakeDuration;
            Destroy(collision.transform.gameObject);
        }
        if(collision.CompareTag("BlueBullet"))
        {
            health -= 1;
            FindObjectOfType<AudioManager>().Play("Hit");
            elapsedTime = shakeDuration;
        }

        if (collision.CompareTag("Health"))
        {
            Destroy(collision.transform.gameObject);
            FindObjectOfType<AudioManager>().Play("Power");
            health += 5;
        }

        if(collision.CompareTag("Border"))
        {
            temp = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            health = 0;
        }
    }

    IEnumerator Damage()
    {
        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(.1f);
        animator.SetBool("Hit", false);
    }

    IEnumerator LoadLoseScene()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
        SceneManager.LoadScene(06);
    }

    void Shake()
    {
        if (elapsedTime > 0)
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

    private void OnDestroy()
    {
        for (int i = 0; i < Managers.Length; i++)
        {
            Managers[i].SetActive(false);
        }
    }
}
