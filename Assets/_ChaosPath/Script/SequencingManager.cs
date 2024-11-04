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
        // Démarrer l'enregistrement pour tous les joueurs
        foreach (PlayerController player in playerControllers)
        {
            player.StartRecording();
        }

        // Attendre le temps d'enregistrement
        yield return new WaitForSeconds(10); // Assure-toi que cela correspond à la durée d'enregistrement dans PlayerController

        // Lecture des commandes pour tous les joueurs
        foreach (PlayerController player in playerControllers)
        {
            player.PlayCommands();
        }
    }
}