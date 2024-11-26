using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    MainMenu,
    Playing,
    EndScreen
}

public class GameState : MonoBehaviour
{
    public State state;
    public static GameState Instance { get; private set; }
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
    void Start()
    {
        state = State.MainMenu;
    }
}
