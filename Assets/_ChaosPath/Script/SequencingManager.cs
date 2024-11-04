using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencingManager : MonoBehaviour
{
    public List<PlayerController> playerControllers;

    void Start()
    {
        StartCoroutine(SequencePlayers());
    }

    private IEnumerator SequencePlayers()
    {
        // Start Record
        foreach (PlayerController player in playerControllers)
        {
            player.StartRecording();
        }
        yield return new WaitForSeconds(10);

        // StartPlay
        foreach (PlayerController player in playerControllers)
        {
            player.PlayCommands();
        }
    }
}