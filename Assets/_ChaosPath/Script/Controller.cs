using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerId;
    public float moveSpeed = 5f;
    private List<Command> commands = new List<Command>();
    private bool isRecording = false;
    private Vector2 lastDirection = Vector2.zero;

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
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(moveX, moveY);

       
        if (direction != lastDirection)
        {
            commands.Add(new Command(direction, Time.time));
            lastDirection = direction;
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
        foreach (Command command in commands)
        {
            float startTime = Time.time;
            while (Time.time < startTime + 0.01f)
            {
                Move(command.direction);
                yield return null;
            }
        }
    }

    private void Move(Vector2 direction)
    {
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
    }
}