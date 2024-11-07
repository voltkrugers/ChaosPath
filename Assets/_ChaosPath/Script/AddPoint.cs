using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController Winner = other.GetComponent<PlayerController>();
            if (Winner.playerId == 1)
            {
                GameManager.Instance.EndManche(1,0);
            }
            else
            {
                GameManager.Instance.EndManche(0, 1);
            }
        }
    }
}
