using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{        
    private Dictionary<string, int> _items;
    
    public ManagerStatus Status {get; private set;}
    
    public void Startup()
    {        
        UpdateData(new Dictionary<string, int>());
        Status = ManagerStatus.Started;
    }

    public void AddItem(string name)
    {
        if (_items.ContainsKey(name))
        {
            _items[name] += 1;            
        }
        else
        {
            _items[name] = 1;            
        }
        DisplayItems();
    }

    public List<string> GetItemList()
    {
        return new List<string>(_items.Keys);
    }
    
    public int GetItemCount(string name)
    {
        if (_items.ContainsKey(name))
        {
            return _items[name];
        }
        return 0;
    }        
    
    public bool ConsumeItem(string name)
    {
        if (_items.ContainsKey(name))
        {
            _items[name]--;
            if (_items[name] == 0)
            {
                _items.Remove(name);
            }
            else
            {                
                return false;
            }
        }
        DisplayItems();
        return true;
    }

    public void UpdateData(Dictionary<string, int> items)
    {
        this._items = items;
    }

    public Dictionary<string, int> GetData()
    {
        return _items;
    }
    
    private void DisplayItems()
    {
        string itemDisplay = "Items: ";
        foreach (KeyValuePair<string, int> item in _items)
        {
            itemDisplay += item.Key + "(" + item.Value + ")";
        }                
    }   
}