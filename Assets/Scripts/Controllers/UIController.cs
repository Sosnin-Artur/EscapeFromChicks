using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text healthLabel;
    [SerializeField] private Text energyLabel;
    [SerializeField] private Text scoreLabel;    
    [SerializeField] private Text levelEnding;
        
    public void FinishGame()
    {
        Manager.Data.SaveGameState();
        Application.Quit();
    }

    public void SaveGame()
    {
        Manager.Data.SaveGameState();
    }

    public void LoadGame()
    {
        Manager.Data.LoadGameState();
    }
    
    private void Awake()
    {        
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.SCORE_UPDATED, OnScoreUpdated);
        Messenger.AddListener(GameEvent.ENERGY_UPDATED, OnEnergyUpdated);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETED, OnLevelComplete);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.AddListener(GameEvent.GAME_COMPLETE, OnGameComplete);
    }
    
    private void OnDestroy()
    {        
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.RemoveListener(GameEvent.SCORE_UPDATED, OnScoreUpdated);
        Messenger.RemoveListener(GameEvent.ENERGY_UPDATED, OnEnergyUpdated);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETED, OnLevelComplete);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.RemoveListener(GameEvent.GAME_COMPLETE, OnGameComplete);
    }

    private void Start()
    {
        OnHealthUpdated();
        levelEnding.gameObject.SetActive(false);        
    }     
    
    private void OnHealthUpdated()
    {
        if (Manager.Player == null)
        {
            Debug.Log("no player");
        }
        string message = "Health: " + Manager.Player.Health + "/" + Manager.Player.MaxHealth;
        healthLabel.text = message;
    }

    private void OnScoreUpdated()
    {
        if (Manager.Player == null)
        {
            Debug.Log("no player");
        }
        string message = "Score: " + Manager.Player.Score;
        scoreLabel.text = message;
    }

    private void OnEnergyUpdated()
    {
        if (Manager.Player == null)
        {
            Debug.Log("no player");
        }
        string message = "Energy: " + Manager.Player.Energy + "/" + Manager.Player.MaxEnergy;
        energyLabel.text = message;
    }

    private void OnLevelComplete()
    {
        StartCoroutine(CompleteLevel());
    }
    
    private void OnLevelFailed()
    {
        StartCoroutine(FailLevel());
    }

    private void OnGameComplete()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "you finished the game";
    }

    private IEnumerator CompleteLevel()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "level Complete!";
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        Manager.Mission.GoToNext();
    }

    private IEnumerator FailLevel()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "level Failed!";
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        Manager.Player.Respawn();
        Manager.Mission.RestartCurrent();
    }
}
