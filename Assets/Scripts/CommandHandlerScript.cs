using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandlerScript : MonoBehaviour
{
    public int shuffleAmount;
    public int commandLimit;
    public GameObject upCommand;
    public GameObject downCommand;
    public GameObject leftCommand;
    public GameObject rightCommand;
    public GameObject recordText;
    public GameObject replayText;
    public AudioClip tileMoveSuccessSound;
    public AudioClip tileMoveFailSound;
    public AudioClip commandAddSound;

    private List<GameObject> commands;
    private State state;

    void Start()
    {
        commands = new List<GameObject>();
        StartCoroutine(Shuffle(shuffleAmount));
    }

    void Update()
    {
        if (state == State.RECORD)
        {
            ListenForInput();
        }
        if (commands.Count >= commandLimit)
        {
            Replay();
        }
        else if (commands.Count == 0 && state != State.SHUFFLE)
        {
            state = State.RECORD;
        }

        VisualizeState();
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

    private void VisualizeState()
    {
        switch (state)
        {
            case State.RECORD:
                recordText.SetActive(true);
                replayText.SetActive(false);
                break;
            case State.REPLAY:
                replayText.SetActive(true);
                recordText.SetActive(false);
                break;
            default:
                replayText.SetActive(false);
                recordText.SetActive(false);
                break;
        }
    }

    private void AddCommandToQueue(GameObject command)
    {
        // add to queue
        commands.Add(command);
        GetComponent<AudioSource>().clip = commandAddSound;
        GetComponent<AudioSource>().Play();

        // visualize
        command.transform.SetParent(GameObject.Find("CommandQueueBox").transform);
        command.transform.localScale = Vector3.zero;
        LeanTween.scale(command, Vector3.one, 0.25f);
        command.transform.localPosition = new Vector3(100, -140 + 105 * (commands.Count - 1), 0);

    }

    public IEnumerator Shuffle(int n)
    {
        Time.timeScale = 2.0f;
        state = State.SHUFFLE;
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
        state = State.RECORD;
        Time.timeScale = 1.0f;

    }

    private Command CreateRandomMoveCommand()
    {
        int randomNumber = Random.Range(0, 4);
        switch (randomNumber)
        {
            case 0:
                return Instantiate<GameObject>(upCommand).GetComponent<Command>();
            case 1:
                return Instantiate<GameObject>(rightCommand).GetComponent<Command>();
            case 2:
                return Instantiate<GameObject>(downCommand).GetComponent<Command>();
            default:
                return Instantiate<GameObject>(leftCommand).GetComponent<Command>();
        }
    }

    private void Replay()
    {
        state = State.REPLAY;
        StartCoroutine(PlayCommands());
    }

    private IEnumerator PlayCommands()
    {
        while (commands.Count > 0 && state == State.REPLAY)
        {
            GameObject nextCommand = commands[0];
            if (nextCommand.GetComponent<Command>().Execute())
            {
                GetComponent<AudioSource>().clip = tileMoveSuccessSound;
            }
            else
            {
                GetComponent<AudioSource>().clip = tileMoveFailSound;
            }

            GetComponent<AudioSource>().Play();
            LeanTween.scale(nextCommand, Vector3.zero, 0.25f).setDestroyOnComplete(true);
            commands.RemoveAt(0);
            yield return new WaitForSeconds(0.75f);
        }
        state = State.RECORD;
    }

    public State GetState()
    {
        return state;
    }

    public enum State
    {
        SHUFFLE, RECORD, REPLAY
    }
}
