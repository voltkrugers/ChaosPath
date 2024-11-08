using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChronoPhase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CountDownTimer;
    [SerializeField] private TextMeshProUGUI textPhase;
    private float countDown = 0f;

    private void Awake()
    {
        GameManager.Instance.chronophase = this;
    }

    void Update()
    {
        if (countDown > 0f)
        {
            countDown -= Time.deltaTime;
            countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
            CountDownTimer.text = string.Format("{0:00.00}", countDown);
        }
    }
    
    public void setText(string phase)
    {
        textPhase.text = phase;
    }
    public void setCountDown(float value)
    {
        countDown = value;
    }
}
