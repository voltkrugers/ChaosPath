using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChronoPhase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CountDownTimer;
    [SerializeField] private TextMeshProUGUI textPhase;
    [SerializeField] private float timeBetweenPhase = 10f;
    private bool Record;
    private float countDown = 0f;
    // Update is called once per frame
    void Update()
    {
        if (countDown <= 0f)
        {
            if (Record)
            {
                Record = !Record;
                textPhase.text = "Phase de Play";
                countDown = timeBetweenPhase+5f;
            }
            else
            {
                Record = !Record;
                textPhase.text = "Phase de Record";
                countDown = timeBetweenPhase;
            }
        }

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        CountDownTimer.text = string.Format("{0:00.00}", countDown);
    }
}
