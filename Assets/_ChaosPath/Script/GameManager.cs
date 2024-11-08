using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<PlayerController> playerControllers;
    public ChronoPhase chronophase;
    public float timeRecordPhase = 10f;
    public float timePlayPhase = 15f;

    public bool EndGame = false;
    private int pointsJ1,pointsJ2;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        pointsJ1 = 0;
        pointsJ2 = 0;
    }


    public IEnumerator SequencePlayers()
    {
        foreach (PlayerController player in playerControllers)
        {
            player.StopAllCoroutines();
        }

        chronophase.setText("Enregistrement...");
        chronophase.setCountDown(timeRecordPhase);
        foreach (PlayerController player in playerControllers)
        {
            player.StartRecording();
        }
        yield return new WaitForSeconds(timeRecordPhase);

        chronophase.setText("Action !");
        chronophase.setCountDown(timePlayPhase);
        foreach (PlayerController player in playerControllers)
        {
            player.PlayCommands();
        }
        yield return new WaitForSeconds(timePlayPhase);
        
        if (!EndGame)
        {
            Debug.Log("restart manche");
            ReplacePlayer();
            StartCoroutine(SequencePlayers());
        }
        
    }
    private void ReplacePlayer()
    {
        foreach (var player in playerControllers)
        {
            player.gameObject.transform.position = GameConstructor.PosStart;
        }
    }
    
    public void EndManche(int pointsGagnesJ1, int pointsGagnesJ2)
    {
        EndGame = true;
        pointsJ1 += pointsGagnesJ1;
        pointsJ2 += pointsGagnesJ2;
        //Debug.Log("Points mis à jour : " + pointsJ1 + " vs " + pointsJ2);
        playerControllers.Clear();
        SceneManager.LoadScene("SampleScene");
    }
}