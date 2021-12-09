using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{   
    public int Health {get; private set;}
    public int MaxHealth {get; private set;}
    public int Energy {get; private set;}    
    public int MaxEnergy {get; private set;}
    public int Score {get; private set;}

    public ManagerStatus Status {get; private set;}
       
    public void Startup()
    {        
        UpdateData(50, 100, 50, 100);
        Status = ManagerStatus.Started;
    }

    public void ChangeHealth(int value)
    {
        Health += value;

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }

        if (Health <= 0)
        {
            Health = 0;
            Messenger.Broadcast(GameEvent.LEVEL_FAILED);
        }        

        Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
    }

    public void ChangeScore(int value)
    {
        Score += value;
        Messenger.Broadcast(GameEvent.SCORE_UPDATED);
    }
    
    public void ChangeEnergy(int value)
    {
        Energy += value;
        
        if (Energy > MaxEnergy)
        {
            Energy = MaxEnergy;
        }

        if (Energy <= 0)
        {                        
            Energy = 0;
            Messenger.Broadcast(GameEvent.ENERGY_ENDED);
        }        

        Messenger.Broadcast(GameEvent.ENERGY_UPDATED);
    }    

    public void UpdateData(int Health, int MaxHealth, int Energy, int MaxEnergy)
    {
        this.Health = Health;
        this.MaxHealth = MaxHealth;
        this.Energy = Energy;
        this.MaxEnergy = MaxEnergy;
    }

    public void Respawn()
    {
        UpdateData(50, 100, 50, 100);
    }        
}