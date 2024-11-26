using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    public TextMeshProUGUI J1, J2;

    private void Start()
    {
        J1.text = "J1 : " + GameManager.Instance.pointsJ1;
        J2.text = "J2 : " + GameManager.Instance.pointsJ2;
    }

    public void next()
    {
        GameState.Instance.state = State.Playing;
        SceneManager.LoadScene("Game");
    }
}
