using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChronoPhase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CountDownTimer;
    [SerializeField] private TextMeshProUGUI textPhase;
    [SerializeField] private float timeBetweenPhase = 10f;
    public Sprite DefaultPowerUpPicture;
    public Image imageP1;
    public Image imageP2;
    private bool Record;
    private float countDown = 0f;

    void start()
    {
        if (DefaultPowerUpPicture == null)
        {
            Debug.LogError("No default icon for power up");
        }
    }

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
