using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour, IGameManager
{            
    private string _filename;

    public ManagerStatus Status {get; private set;}
    
    public void Startup()
    {                
        _filename = Path.Combine(Application.persistentDataPath, "game.dat");
        Status = ManagerStatus.Started;
    }

    public void SaveGameState()
    {
        Dictionary<string, object> gamestate = new Dictionary<string, object>();
        gamestate.Add("inventory", Manager.Inventory.GetData());
        gamestate.Add("Health", Manager.Player.Health);
        gamestate.Add("MaxHealth", Manager.Player.MaxHealth);
        gamestate.Add("Energy", Manager.Player.Energy);
        gamestate.Add("MaxEnergy", Manager.Player.MaxEnergy);
        gamestate.Add("CurLevel", Manager.Mission.CurLevel);        
        gamestate.Add("MaxLevel", Manager.Mission.MaxLevel);        
        gamestate.Add("mazeData", Manager.Maze.GetData());  

        FileStream stream = File.Create(_filename);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, gamestate);
        stream.Close();
    }

    public void LoadGameState()
    {
        if (!File.Exists(_filename))
        {
            Debug.Log("no saved game");
            return;
        }

        Dictionary<string, object> gamestate;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Open(_filename, FileMode.Open);
        gamestate = formatter.Deserialize(stream) as Dictionary<string, object>;
        stream.Close();

        Manager.Inventory.UpdateData((Dictionary<string, int>)gamestate["inventory"]);
        Manager.Player.UpdateData((int)gamestate["Health"], (int) gamestate["MaxHealth"], (int)gamestate["Energy"], (int) gamestate["MaxEnergy"]);
        Manager.Mission.UpdateData((int)gamestate["CurLevel"], (int) gamestate["MaxLevel"]);
        Manager.Maze.UpdateData((bool[][])gamestate["mazeData"]);        
        Manager.Mission.RestartCurrent();
    }

}
