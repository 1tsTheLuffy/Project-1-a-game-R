using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class MainPlayer : MonoBehaviour
{
    public int health;

    private Vector2 mousePos;

    [SerializeField] float elapsedTime;
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeFrequency;
    [SerializeField] float shakeAmplitude;

    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] GameObject[] bullet;
    [SerializeField] GameObject particlePrefab;
    [SerializeField] GameObject[] Managers;

    [SerializeField] Transform[] gunPoint;

    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] CinemachineBasicMultiChannelPerlin virtualNoiseCamera;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;
    Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        if(virtualCamera != null)
        {
            virtualNoiseCamera = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }

        healthText.text = health.ToString();
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;

        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet[0], gunPoint[0].position, gunPoint[0].rotation);
            Instantiate(particlePrefab, gunPoint[0].position, gunPoint[0].rotation);
            elapsedTime = shakeDuration;
        }
        else if(Input.GetMouseButtonDown(1))
        {
            Instantiate(bullet[1], gunPoint[1].position, gunPoint[1].rotation);
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
        if (health <= 0)
        {
            Destroy(gameObject);
        }

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
            StartCoroutine(Damage());
            Destroy(collision.transform.gameObject);
        }
        if(collision.CompareTag("YellowCircleEnemy"))
        {
            health -= 1;
            StartCoroutine(Damage());
            elapsedTime = shakeDuration;
            Destroy(collision.transform.gameObject);
        }
        if(collision.CompareTag("BlueBullet"))
        {
            health -= 1;
            elapsedTime = shakeDuration;
        }
    }

    IEnumerator Damage()
    {
        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(.1f);
        animator.SetBool("Hit", false);
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
