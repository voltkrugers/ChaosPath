using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    
    public static PowerUpSpawner Instance { get; private set; }
    public List<GameObject> AsteroidNonMovable;
    public List<GameObject> AsteroidCrossing;
    public List<GameObject> AsteroidAlternating; 
    public List<GameObject> Projectiles;
    public List<GameObject> Coins;
    private float camerHeight = 0f;
    private float cameraWidth = 0f;
    private int countTime;
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


    public void Start()
    {
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
        if (Projectiles.Count <= 0)
        {
            Debug.LogError("Projectile is Empty");
        }
        camerHeight = 2f * Camera.main.orthographicSize;
        cameraWidth = camerHeight * Camera.main.aspect;

    }

    public void UsePower(PowerUp power, PlayerController owner) //ca va degager ca // finalement non, on l'a réutilisé
    {
        if (power.type == PowerUpType.AsteroidStatic)
        {
            SpawnAsteroidNonMovable();
        }
        if (power.type == PowerUpType.AsteroidCrossing )
        {
            SpawnAsteroidCrossing(owner);
        }
        if (power.type == PowerUpType.AsteroidAlternating)
        {
            SpawnAsteroidAlternating();
        }
        if (power.type == PowerUpType.Coin)
        {
            SpawnCoin();
        }
        if (power.type == PowerUpType.Projectile)
        {
            SpawnProjectile(owner);
        }
    }

    public void SpawnAsteroidNonMovable() //Asteroid spawn dans la zone vue par la cam�ra
    {
        if (AsteroidNonMovable.Count > 0)
        {
            Vector3 spawnpos = GetPositionWithinCameraView(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f)));
            spawnpos.z = 0;
            Instantiate(AsteroidNonMovable[Random.Range(0, AsteroidNonMovable.Count)],spawnpos,Quaternion.identity);
        }
    }
    
    public void SpawnAsteroidCrossing(PlayerController owner) //Asteroid spawn hors de l'ecran et se dirige a peu pres vers le centre de la camera
    {
        if (AsteroidCrossing.Count > 0)
        {
            Vector3 spawnpos = GetPositionWithinCameraView(GetPointsOutsideView());
            spawnpos.z = 0;
            GameObject ast=Instantiate(AsteroidCrossing[Random.Range(0, AsteroidCrossing.Count)], spawnpos, Quaternion.identity);
            ast.GetComponent<AsteroidCrossing>().Owner = owner;
        }
    }

    public void SpawnAsteroidAlternating() //Asteroid qui alterne entre deux points
    {
        if (AsteroidAlternating.Count > 0)
        {
            Vector3 spawnpos = GetPositionWithinCameraView(new Vector3(Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f)));
            spawnpos.z = 0;
            Instantiate(AsteroidAlternating[Random.Range(0, AsteroidAlternating.Count)], spawnpos, Quaternion.identity);
        }
    }

    public void SpawnCoin() //Bonus coin
    {
        
        if (Coins.Count > 0)
        {
            Vector3 spawnpos = GetPositionWithinCameraView(new Vector3(Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f)));
            spawnpos.z = 0;
            GameObject coin = Instantiate(Coins[Random.Range(0, Coins.Count)], spawnpos, Quaternion.identity);
            VerifOtherObj(coin);
        }
    }
    public void SpawnProjectile(PlayerController owner) //Bullets shot by the players
    {
        if (Projectiles.Count > 0)
        {
            float angle = 30f;
            Vector3 spawnpos = new Vector3(owner.transform.position.x, owner.transform.position.y, 0);

            //bullet 1
            GameObject bullet1 = Instantiate(Projectiles[Random.Range(0, Coins.Count)], spawnpos, Quaternion.Euler(0, 0, -angle + owner.transform.rotation.z));
            Vector3 direction1 = Quaternion.Euler(0, 0, -angle + owner.spritePivot.transform.rotation.eulerAngles.z) * Vector3.up ;

            Projectile projectile1 = bullet1.GetComponent<Projectile>();
            projectile1.Owner = owner;
            projectile1.target = direction1;


            //bullet 2
            GameObject bullet2 = Instantiate(Projectiles[Random.Range(0, Coins.Count)], spawnpos, Quaternion.Euler(0, 0, owner.transform.rotation.z));
            Vector3 direction2 = Quaternion.Euler(0, 0, owner.spritePivot.transform.rotation.eulerAngles.z) * Vector3.up;

            Projectile projectile2 = bullet2.GetComponent<Projectile>();
            projectile2.Owner = owner;
            projectile2.target = direction2;


            //bullet 3
            GameObject bullet3 = Instantiate(Projectiles[Random.Range(0, Coins.Count)], spawnpos, Quaternion.Euler(0, 0, angle + owner.transform.rotation.z));
            Vector3 direction3 = Quaternion.Euler(0, 0, angle + owner.spritePivot.transform.rotation.eulerAngles.z) * Vector3.up;

            Projectile projectile3 = bullet3.GetComponent<Projectile>();
            projectile3.Owner = owner;
            projectile3.target = direction3;
        }
    }

    public void VerifOtherObj(GameObject ObjToVerif)
    {
        float checkRadius = 1.0f; 
        Vector2 positionToCheck = ObjToVerif.transform.position;
        Collider2D hit = Physics2D.OverlapCircle(positionToCheck, checkRadius);

        if (hit == null)
        {
            //Debug.Log("Pas d'autre objet à proximité, déplacement possible.");
        }
        else
        {
            //Debug.Log($"Collision détectée avec l'objet : {hit.gameObject.name}. Relocaliser.");
            FindNewPosition(ObjToVerif);
        }
    }

    private void FindNewPosition(GameObject obj)
    {
        countTime++;
        if (countTime>10)
        {
            Destroy(obj);
            countTime = 0;
            return;
        }
        Vector2 newPosition = GetPositionWithinCameraView(new Vector3(Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f)));;
        Collider2D hit = Physics2D.OverlapCircle(newPosition, 1.0f);

        if (hit != null)
        {
            FindNewPosition(obj);
            return;
        }
        obj.transform.position = newPosition;
        //Debug.Log("Objet déplacé vers une position libre.");
        countTime = 0;
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
