using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject target;
    public float Speed=1f;
    public float FollowingDistance = 1f;
    public Animator anim;

    void Start()
    {
        target = null;
    }

    void Update()
    {
        if (target != null)
        {
            float distance = (target.transform.position - gameObject.transform.position).magnitude;
            if (distance > FollowingDistance)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, Speed * distance * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerController temp = target.GetComponent<PlayerController>();
            if (target != collision.gameObject)
            {
                anim.SetTrigger("Picked");
            }
            if (target != null)
            {
                temp.bonusPoints -=1 ;
            }

            target = collision.gameObject;
            temp.bonusPoints += 1;
            
        }
    }
}
