using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainPlayer : MonoBehaviour
{
    private Vector2 mousePos;

    [SerializeField] float elapsedTime;
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeFrequency;
    [SerializeField] float shakeAmplitude;

    [SerializeField] GameObject[] bullet;

    [SerializeField] Transform[] gunPoint;

    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] CinemachineBasicMultiChannelPerlin virtualNoiseCamera;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        if(virtualCamera != null)
        {
            virtualNoiseCamera = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;

        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet[0], gunPoint[0].position, gunPoint[0].rotation);
            elapsedTime = shakeDuration;
        }
        else if(Input.GetMouseButtonDown(1))
        {
            Instantiate(bullet[1], gunPoint[1].position, gunPoint[1].rotation);
            elapsedTime = shakeDuration;
        }

        Shake();
    }

    private void OnMouseDrag()
    {
        
        transform.position = mousePos;
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
}
