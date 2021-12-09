using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{    
    [SerializeField] int energyConsume = 1;
                
    [SerializeField] private SceneController sceneController;        
    [SerializeField] private Light torch;      

    private bool _isAciveEnergyConsume = true;
    private IEnumerator _energyConsuming;
    
    public void Toggle()
    {
        if (Manager.Player.Energy > 0 && !sceneController.isPaused)
        {
            ToggleEnergyConsume();            
        }
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENERGY_ENDED, OnEnergyEnded);
    }
    
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENERGY_ENDED, OnEnergyEnded);
    }

    private void Start()
    {
        Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
        Messenger.Broadcast(GameEvent.SCORE_UPDATED);
        _energyConsuming = ConsumeEnergy();
        StartCoroutine(_energyConsuming);
    }    
     
    private void Update()
    {
       if (Input.GetKeyUp(KeyCode.F))
        {
            Toggle();            
        }
    }
    
    private void ToggleEnergyConsume()
    {
        _isAciveEnergyConsume = !_isAciveEnergyConsume;
        if (_isAciveEnergyConsume)
        {
            StartCoroutine(_energyConsuming);            
        }
        else
        {   
            StopCoroutine(_energyConsuming);         
        }        
       torch.enabled = _isAciveEnergyConsume;        
    }

    private IEnumerator ConsumeEnergy()
    {
        while(_isAciveEnergyConsume && !sceneController.isPaused)
        {
            Manager.Player.ChangeEnergy(-energyConsume);
            yield return new WaitForSeconds(1);            
        }
    }

    private void OnEnergyEnded()
    {    
        torch.enabled = false;        
    }
}
