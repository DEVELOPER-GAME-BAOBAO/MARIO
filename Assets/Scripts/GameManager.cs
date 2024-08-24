using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // 4 DAY Loading Game Level and GameOver
    // Start is called before the first frame update
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; } // DAY 6
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        Application.targetFrameRate = 60; // DAY 6 ngôi sao start
        NewGame();
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void NewGame()
    {
        lives = 3;
        coins = 0;

        LoadLevel(1, 1);
    }


    // using Enigne.SceneManagement
    public void LoadLevel(int wrold, int stage)
    {
        this.world = wrold;
        this.stage = stage;

        SceneManager.LoadScene($"{world} - {stage}"); //Scene in the 1-1 and 2-2
    }

    public void NextLevel()
    {
        /*if (world == 1 && stage == 10)
        {

        }*/
        LoadLevel(world, stage + 1);
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;

        if(lives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        NewGame();
    }

    public void AddCoin()
    {
        coins++;

        if(coins == 100)
        {
            AddLife();
            coins = 0;
        }
    }

    public void AddLife()
    {
        lives++;
    }

}
