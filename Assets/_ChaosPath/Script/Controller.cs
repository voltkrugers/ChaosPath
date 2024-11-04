using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerId; 
    public float moveSpeed = 5f;
    private List<Command> commands = new List<Command>();
    private bool isRecording = false;
    
    void Update()
    {
        if (isRecording)
        {
            RecordInput();
        }
    }

    public void StartRecording()
    {
        isRecording = true;
        commands.Clear();
        StartCoroutine(StopRecordingAfterTime(10));
    }

    private void RecordInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            commands.Add(new Command("Right", Time.time));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            commands.Add(new Command("Left", Time.time));
        }        
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            commands.Add(new Command("Up", Time.time));
        }       
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            commands.Add(new Command("Down", Time.time));
        }
    }

    private IEnumerator StopRecordingAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        isRecording = false;
        Debug.Log($"Player {playerId} finished recording: {commands.Count} commands recorded.");
    }

    public void PlayCommands()
    {
        StartCoroutine(ExecuteCommands());
    }

    private IEnumerator ExecuteCommands()
    {
        for (int i = 0; i < commands.Count; i++)
        {
            Command currentCommand = commands[i];
            float commandDuration = (i < commands.Count - 1) ? commands[i + 1].time - currentCommand.time : 0.1f;

            float startTime = Time.time;
            while (Time.time < startTime + commandDuration)
            {
                switch (currentCommand.direction)
                {
                    case "Right":
                        Move(Vector2.right);
                        break;
                    case "Left":
                        Move(Vector2.left);
                        break;
                    case "Up":
                        Move(Vector2.up);
                        break;
                    case "Down":
                        Move(Vector2.down);
                        break;

                }
                yield return null;
            }
        }
    }

    private void Move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
