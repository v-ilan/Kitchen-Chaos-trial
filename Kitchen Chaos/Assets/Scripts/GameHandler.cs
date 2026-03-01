using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }

    public event EventHandler OnStateChange;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameResume;
    public event EventHandler OnRushHourStarted;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }
    private State state;

    //private const float WAITING_TO_START_TIMER = 1f;
    
    private const float COUNTDOWN_TO_START_TIMER = 3f;
    private const float GAME_PLAYING_TIMER = 3.5f * 60f;

    private const float RUSH_HOUR_THRESHOLD = 30f;

    private float timer = 0f;

    private bool isGamePaused = false;

    private bool isRushHour = false;



    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
        //timer = WAITING_TO_START_TIMER;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInputOnPauseAction;
        GameInput.Instance.OnInteractAction += GameInputOnInteractAction;
    }

    private void GameInputOnInteractAction(object sender, EventArgs e)
    {
        if(state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            timer = COUNTDOWN_TO_START_TIMER;
            OnStateChange?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update()
    {
        switch(state)
        {
            case State.WaitingToStart:
                break; 
            case State.CountdownToStart:
                timer -= Time.deltaTime;
                if (timer < 0f)
                {
                    timer = GAME_PLAYING_TIMER;
                    state = State.GamePlaying;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                    
                }
                break;
            case State.GamePlaying:
                timer -= Time.deltaTime;
                if(!isRushHour && timer < RUSH_HOUR_THRESHOLD)
                {
                    isRushHour = true;
                    OnRushHourStarted?.Invoke(this, EventArgs.Empty);
                    Debug.Log("Rush Hour Event Fired!");
                }
                if (timer < 0f)
                {
                    state = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
            default:
                Debug.LogError("No correct State!");
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdown()
    {
        return state == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float getCountdownTimer()
    {
        float timer = 0f;
        if (state == State.CountdownToStart)
        {
            timer = this.timer;
        }
        return timer;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (timer / GAME_PLAYING_TIMER);
    }

    private void GameInputOnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }
    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameResume?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnDestroy() 
    {
        if (GameInput.Instance != null) 
        {
            GameInput.Instance.OnPauseAction -= GameInputOnPauseAction;
            GameInput.Instance.OnInteractAction -= GameInputOnInteractAction;
        }
    }
}
