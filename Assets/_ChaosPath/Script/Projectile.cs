using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 1f;
    public float lifetime = 100f;
    public Vector3 spawnpos;
    public Vector3 target;
    public PlayerController Owner;
    [SerializeField] private GameObject explosion;

    void Start()
    {
        spawnpos = gameObject.transform.position;
        //target = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)) - spawnpos;
        //target.z = 0;
    }

    void Update()
    {
        gameObject.transform.position += target.normalized * speed * Time.deltaTime;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")&& collision.gameObject!=Owner.gameObject)
        {
            Owner.bonusPoints += 1;
            GameObject boom = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(boom, 2f); 
            collision.gameObject.GetComponent<PlayerController>().Die();
            this.gameObject.SetActive(false);
        }
    }
}
