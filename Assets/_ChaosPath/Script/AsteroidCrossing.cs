using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCrossing : MonoBehaviour
{
    public float speed = 1f;
    public float lifetime=100f;
    public Vector3 spawnpos;
    public float randomFactor = 1f;
    public Vector3 target;

    private float camerHeight = 0f;
    private float cameraWidth = 0f;

    void Start()
    {
        spawnpos = gameObject.transform.position;
        randomFactor = Random.Range(-30f, 30f);

        target = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)) - spawnpos;
        target.z = 0;
        target = Quaternion.Euler(0, 0, randomFactor) * target;
    }

    void Update()
    {
        gameObject.transform.position += target.normalized*speed * Time.deltaTime;
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        } 
    }
    private Vector3 GetPositionWithinCameraView(Vector3 pos)
    {
        float x = pos.x * cameraWidth - cameraWidth / 2;
        float y = pos.y * camerHeight - camerHeight / 2;
        return new Vector3(x, y, 0);
    }
}
