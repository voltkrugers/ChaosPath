using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<PlayerController> playerControllers;

    public ChronoPhase chronophase;
    public float timeRecordPhase = 10f;
    public float timePlayPhase = 15f;
    public List<PowerUp> ListPower;
    public bool EndGame = false;
    public int pointsJ1,pointsJ2=0;

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
            player.gameObject.transform.rotation = Quaternion.AngleAxis(0,Vector3.zero);
            player.gameObject.SetActive(true);
        }
    }
    
    public void EndManche(int pointsGagnesJ1, int pointsGagnesJ2)
    {
        EndGame = true;
        pointsJ1 += pointsGagnesJ1;
        pointsJ2 += pointsGagnesJ2;

        playerControllers.Clear();
        GameState.Instance.state = State.EndScreen;
        SceneManager.LoadScene("ScoreBoard");
    }

    public PowerUp getRandomPower( PlayerController playerController)
    {
        
        int randomIndex = Random.Range(0, ListPower.Count);
        chronophase.changeImage(ListPower[randomIndex].sprite,playerController.playerId);
        return ListPower[randomIndex];
    }
}