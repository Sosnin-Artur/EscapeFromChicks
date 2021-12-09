using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager
{    
    [SerializeField] private float placementThreshold = 0.1f;
    [SerializeField] private int mazeSize = 10;

    public ManagerStatus Status {get; private set;}
    
    public int CurLevel {get; private set;}
    public int MaxLevel {get; private set;}
    
    public void Startup()
    {        
        UpdateData(0, 3);
        Status = ManagerStatus.Started;
    }

    public void GoToNext()
    {
        if (CurLevel < MaxLevel)
        {                        	
            CurLevel++;
            Manager.Maze.GenerateNewDataMaze(mazeSize * CurLevel, mazeSize * CurLevel, placementThreshold * CurLevel);
            string name = "Level" + CurLevel;            
            SceneManager.LoadScene(name);                    
        }
        else
        {            
            Messenger.Broadcast(GameEvent.GAME_COMPLETE);
        }
    }

    public void ReachObjective()
    {
        Messenger.Broadcast(GameEvent.LEVEL_COMPLETED);
    }

    public void UpdateData(int CurLevel, int MaxLevel)
    {
        this.CurLevel = CurLevel;
        this.MaxLevel = MaxLevel;
    }

    public void RestartCurrent()
    {
        string name = "Level" + CurLevel;        
        SceneManager.LoadScene(name);
    }
    
}