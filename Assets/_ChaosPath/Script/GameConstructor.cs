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
    public List<PlayerController> playerControllers;

    void Start()
    {
        GenerateMap();
        StartCoroutine(SequencePlayers());

    }

    private void GenerateMap()
    {

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        Vector3 startPosition = GetRandomPositionWithinCameraView(width, height);
        Vector3 endPosition = GetRandomPositionWithinCameraView(width, height);

        while (Vector3.Distance(startPosition, endPosition) < (width + height) / 3)
        {
            endPosition = GetRandomPositionWithinCameraView(width, height);
        }

        Instantiate(startPrefab, startPosition, Quaternion.identity);
        Instantiate(endPrefab, endPosition, Quaternion.identity);

        foreach (var player in Player)
        {
            Instantiate(player, startPosition, quaternion.identity);
        }
        
        SearchPlayer();
        for (int i = 0; i < numberOfMeteorites; i++)
        {
            Vector3 meteorPosition;
            do
            {
                meteorPosition = GetRandomPositionWithinCameraView(width, height);
            }
            while(Vector3.Distance(meteorPosition, startPosition) < minDistanceFromStartOrEnd ||
                  Vector3.Distance(meteorPosition, endPosition) < minDistanceFromStartOrEnd);

            GameObject randomMeteorite = meteoritePrefabs[Random.Range(0, meteoritePrefabs.Count)];
            Instantiate(randomMeteorite, meteorPosition, Quaternion.identity);
            
        }
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
            playerControllers.Add(player);
        }
    }

    private IEnumerator SequencePlayers()
    {
        foreach (PlayerController player in playerControllers)
        {
            player.StartRecording();
        }
        
        yield return new WaitForSeconds(10);

        foreach (PlayerController player in playerControllers)
        {
            player.PlayCommands();
        }
    }
}