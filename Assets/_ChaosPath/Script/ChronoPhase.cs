using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChronoPhase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CountDownTimer;
    [SerializeField] private TextMeshProUGUI textPhase;

    public Sprite DefaultPowerUpPicture;
    public Image imageP1;
    public Image imageP2;
    private float countDown = 0f;

    private void Awake()
    {
        GameManager.Instance.chronophase = this;
    }
    
    void start()
    {
        if (DefaultPowerUpPicture == null)
        {
            Debug.LogError("No default icon for power up");
        }
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

    public void changeImage(Sprite srite, int player)
    {
        
        if (player == 1)
        {
            imageP1.sprite = srite;
        }
        else  //player 2
        {
            imageP2.sprite = srite;
        }
    }
}
