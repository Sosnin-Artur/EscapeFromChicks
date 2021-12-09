using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(MissionManager))]
[RequireComponent(typeof(DataManager))]
[RequireComponent(typeof(SpawnManager))]
[RequireComponent(typeof(MazeManager))]
public class Manager : MonoBehaviour
{
    public static PlayerManager Player {get; private set;}
    public static InventoryManager Inventory {get; private set;}    
    public static MissionManager Mission {get; private set;}
    public static DataManager Data {get; private set;}
    public static SpawnManager Spawn {get; private set;}
    public static MazeManager Maze {get; private set;}
    
    private List<IGameManager> _startSequence;
    
    public void StartGame()
    {
        StartCoroutine(StartupManagers());
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InventoryManager>();
        Mission = GetComponent<MissionManager>();
        Data = GetComponent<DataManager>();
        Spawn = GetComponent<SpawnManager>();
        Maze = GetComponent<MazeManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Inventory);        
        _startSequence.Add(Data);
        _startSequence.Add(Maze);
        _startSequence.Add(Spawn);
        _startSequence.Add(Mission);                
    }
  
    private IEnumerator StartupManagers()
    {        
        foreach (IGameManager manager in _startSequence)
        {            
            manager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence)
            {
                if (manager.Status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }  

            if (numReady > lastReady)
            {                
                Messenger<int,int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            }         
            yield return null;
        }        
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);        
    }
}