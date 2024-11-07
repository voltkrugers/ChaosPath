using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpSpawner : MonoBehaviour
{
    public List<GameObject> AsteroidNonMovable;
    public List<GameObject> AsteroidCrossing;
    public List<GameObject> AsteroidAlternating; 
    public List<GameObject> Coins;
    private float camerHeight = 0f;
    private float cameraWidth = 0f;

    public void Start()
    {
        Debug.LogWarning("Faut degager l'update a terme");

        if (AsteroidNonMovable.Count <= 0)
        {
            Debug.LogError("AsteroidNonMovable is Empty");
        }
        if (AsteroidCrossing.Count <= 0)
        {
            Debug.LogError("AsteroidCrossing is Empty");
        }
        if (AsteroidAlternating.Count <= 0)
        {
            Debug.LogError("AsteroidAlternating is Empty");
        }
        if (Coins.Count <= 0)
        {
            Debug.LogError("Coins is Empty");
        }
        camerHeight = 2f * Camera.main.orthographicSize;
        cameraWidth = camerHeight * Camera.main.aspect;

    }

    public void Update() //ca va dégager ca
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnAsteroidNonMovable();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SpawnAsteroidCrossing();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnAsteroidAlternating();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnCoin();
        }
    }

    public void SpawnAsteroidNonMovable() //Asteroid spawn dans la zone vue par la caméra
    {
        if (AsteroidNonMovable.Count > 0)
        {
            Vector3 spawnpos = GetPositionWithinCameraView(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f)));
            spawnpos.z = 0;
            Instantiate(AsteroidNonMovable[Random.Range(0, AsteroidNonMovable.Count)],spawnpos,Quaternion.identity);

            Debug.Log("Spawned NonMovable here: "+ spawnpos);
        }
    }
    
    public void SpawnAsteroidCrossing() //Asteroid spawn hors de l'ecran et se dirige a peu pres vers le centre de la camera
    {
        if (AsteroidCrossing.Count > 0)
        {
            Vector3 spawnpos = GetPositionWithinCameraView(GetPointsOutsideView());
            spawnpos.z = 0;
            Instantiate(AsteroidCrossing[Random.Range(0, AsteroidCrossing.Count)], spawnpos, Quaternion.identity);

            Debug.Log("Spawned Crossing here: " + spawnpos);
        }
    }

    public void SpawnAsteroidAlternating() //Asteroid qui alterne entre deux points
    {
        if (AsteroidAlternating.Count > 0)
        {
            Vector3 spawnpos = GetPositionWithinCameraView(new Vector3(Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f)));
            spawnpos.z = 0;
            Instantiate(AsteroidAlternating[Random.Range(0, AsteroidAlternating.Count)], spawnpos, Quaternion.identity);

            Debug.Log("Spawned Alternating here: " + spawnpos);
        }
    }

    public void SpawnCoin() //Bonus coin
    {
        if (Coins.Count > 0)
        {
            Vector3 spawnpos = GetPositionWithinCameraView(new Vector3(Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f)));
            spawnpos.z = 0;
            Instantiate(Coins[Random.Range(0, Coins.Count)], spawnpos, Quaternion.identity);

            Debug.Log("Spawned Coin here: " + spawnpos);
        }
    }

    private Vector3 GetPointsOutsideView()
    {
        float x = 0;
        float y = 0;
        float xory = Random.Range(0f, 1f);
        float side = Random.Range(0f, 1f);
        if (xory < 0.5f)
        {
            if (side < 0.5f)
            {
                x = Random.Range(-0.1f, 0f);
                y = Random.Range(0f, 1f);
            }
            else
            {
                x = Random.Range(1f, 1.1f) ;
                y = Random.Range(0f, 1f);
            }
            

        }
        else
        {
            if (side < 0.5f)
            {
                y = Random.Range(-0.1f, 0f);
                x = Random.Range(0f, 1f);
            }
            else
            {
                y = Random.Range(1f, 1.1f);
                x = Random.Range(0f, 1f);
            }
        }
        
        return new Vector3(x, y);
    }

    private Vector3 GetPositionWithinCameraView(Vector3 pos)
    {
        float x = pos.x* cameraWidth - cameraWidth / 2;
        float y = pos.y* camerHeight - camerHeight / 2;
        return new Vector3(x, y, 0);
    }
}
