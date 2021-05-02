using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandlerScript : MonoBehaviour
{

    public List<GameObject> commands;

    private bool isRecording;
    private bool isShuffling;
    public int commandLimit;
    public GameObject upCommand;
    public GameObject downCommand;
    public GameObject leftCommand;
    public GameObject rightCommand;

    void Start()
    {
        commands = new List<GameObject>();
        StartCoroutine(Shuffle(50));
        isRecording = false;
    }

    void Update()
    {
        if (isRecording)
        {
            ListenForInput();
        }

        if (commands.Count >= commandLimit)
        {
            isRecording = false;
            Replay();
        }
        else if (commands.Count == 0 && !isShuffling)
        {
            isRecording = true;
        }
    }

    private void ListenForInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            AddCommandToQueue(Instantiate<GameObject>(leftCommand));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            AddCommandToQueue(Instantiate<GameObject>(rightCommand));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            AddCommandToQueue(Instantiate<GameObject>(upCommand));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            AddCommandToQueue(Instantiate<GameObject>(downCommand));
        }
    }

    private void AddCommandToQueue(GameObject command)
    {
        // add to queue
        commands.Add(command);

        // visualize
        command.transform.SetParent(GameObject.Find("CommandQueueBox").transform);
        command.transform.localScale = Vector3.zero;
        LeanTween.scale(command, Vector3.one, 0.25f);
        command.transform.localPosition = new Vector3(100, -125 + 125 * (commands.Count - 1), 0);

    }

    public IEnumerator Shuffle(int n)
    {
        isShuffling = true;
        Command previousCommand = CreateRandomMoveCommand();
        for (int i = 0; i < n; i++)
        {
            Command nextCommand = CreateRandomMoveCommand();
            while (previousCommand.IsInverseOf(nextCommand))
            {
                nextCommand = CreateRandomMoveCommand();
            }

            bool succesfulMove = nextCommand.Execute();

            float waitTime = 0.2f;
            if (succesfulMove)
            {
                previousCommand = nextCommand;
            }
            else // try again
            {
                i--;
                waitTime = 0.0f;
            }
            yield return new WaitForSeconds(waitTime);
        }
        isShuffling = false;
    }

    private Command CreateRandomMoveCommand()
    {
        int randomNumber = Random.Range(0, 4);
        switch (randomNumber)
        {
            case 0:
                return new MoveUpCommand();
            case 1:
                return new MoveRightCommand();
            case 2:
                return new MoveDownCommand();
            default:
                return new MoveLeftCommand();
        }
    }

    private void Replay()
    {
        StartCoroutine(PlayCommands());
    }

    private IEnumerator PlayCommands()
    {
        while (commands.Count > 0)
        {
            GameObject nextCommand = commands[0];
            nextCommand.GetComponent<Command>().Execute();
            LeanTween.scale(nextCommand, Vector3.zero, 0.25f).setDestroyOnComplete(true);
            commands.RemoveAt(0);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
