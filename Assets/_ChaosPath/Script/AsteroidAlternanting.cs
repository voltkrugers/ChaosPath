using UnityEngine;

public class AsteroidAlternating : MonoBehaviour
{
    public float BaseSpeed = 1f; //a ne pas toucher, elle sert de sauvegarde
    public float CurrentSpeed = 1f; // c'est celle la a modifier

    public Vector3 FirstPos;
    public GameObject SecondPos;
    public Vector3 TargetPos;
    public float AverageDistanceToSecondPos=10f;

    void Start()
    {
        FirstPos = gameObject.transform.position;
        PickSecondPos();
        TargetPos = SecondPos.transform.position;
        CurrentSpeed = BaseSpeed;
        SecondPos.SetActive(false);
    }

    void Update()
    {
        Vector3 vectorToGo = TargetPos - gameObject.transform.position;
        float PortionDuTrajetRestant = Mathf.Abs(vectorToGo.magnitude) / Mathf.Abs((FirstPos - SecondPos.transform.position).magnitude);
        
        if (PortionDuTrajetRestant < 0.3f)
        {
            CurrentSpeed = Mathf.Clamp(BaseSpeed - BaseSpeed * (0.3f - PortionDuTrajetRestant), 0.2f * BaseSpeed, BaseSpeed);
        }
        else if (PortionDuTrajetRestant > 0.7f)
        {
            CurrentSpeed = Mathf.Clamp(BaseSpeed - BaseSpeed * (PortionDuTrajetRestant-0.7f), 0.2f * BaseSpeed, BaseSpeed);
        }

        Vector3 Movement = vectorToGo.normalized * CurrentSpeed * Time.deltaTime;
        gameObject.transform.position += Movement;

        if (Mathf.Abs(vectorToGo.magnitude) < Movement.magnitude)
        {
            if (TargetPos == FirstPos)
            {
                TargetPos = SecondPos.transform.position;
            }
            else
            {
                TargetPos = FirstPos;
            }
            CurrentSpeed = BaseSpeed;
        }
    }

    public void PickSecondPos()
    {
        float randDist = Random.Range(0.9f,1.1f);
        float randAngle = Random.Range(0f,360f);
        SecondPos.transform.position=new Vector3(AverageDistanceToSecondPos*randDist,0,0);
        SecondPos.transform.position=Quaternion.Euler(0, 0, randAngle) * SecondPos.transform.position;
    }
}
