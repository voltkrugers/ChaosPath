using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameConstructor : MonoBehaviour
{
    public GameObject startPrefab;
    public GameObject endPrefab;
    public List<GameObject> meteoritePrefabs;  
    public int numberOfMeteorites = 20;
    public float minDistanceFromStartOrEnd = 1.0f;
    public List<GameObject> Player;
    public static Vector3 PosStart;
    public GameObject Coins;
    

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        GameManager.Instance.EndGame = false;
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
            
        PosStart = GetRandomPositionWithinCameraView(width, height);
        Vector3 endPosition = GetRandomPositionWithinCameraView(width, height);

        while (Vector3.Distance(PosStart, endPosition) < (width + height) / 3)
        {
            endPosition = GetRandomPositionWithinCameraView(width, height);
        }

        Instantiate(startPrefab, PosStart, Quaternion.identity);
        Instantiate(endPrefab, endPosition, Quaternion.identity);

        foreach (var player in Player)
        {
            Instantiate(player, PosStart, quaternion.identity);
        }
        
        SearchPlayer();
        for (int i = 0; i < numberOfMeteorites; i++)
        {
            Vector3 meteorPosition;
            do
            {
                meteorPosition = GetRandomPositionWithinCameraView(width, height);
            }
            while(Vector3.Distance(meteorPosition, PosStart) < minDistanceFromStartOrEnd ||
                  Vector3.Distance(meteorPosition, endPosition) < minDistanceFromStartOrEnd);

            GameObject randomMeteorite = meteoritePrefabs[Random.Range(0, meteoritePrefabs.Count)];
            Instantiate(randomMeteorite, meteorPosition, Quaternion.identity);
            
        }

        // float Essaie = Random.Range(0f, 1f);
        // Debug.LogError(Essaie);
        // if (Essaie>0.5f)
        // {
        //     PowerUpSpawner.Instance.SpawnCoin();
        // }
        
        
        StartCoroutine(GameManager.Instance.SequencePlayers());
    }

    private Vector3 GetRandomPositionWithinCameraView(float width, float height)
    {
        float x = Random.Range(-width / 2, width / 2);
        float y = Random.Range(-height / 2, height / 2);
        return new Vector3(x, y, 0);
    }

    public void SearchPlayer()
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();
        
        foreach (PlayerController player in players)
        {
           GameManager.Instance.playerControllers.Add(player);
        }
    }

}