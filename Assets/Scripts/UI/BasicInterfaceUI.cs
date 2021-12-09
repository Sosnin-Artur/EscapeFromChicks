using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasicInterfaceUI : MonoBehaviour
{
    [SerializeField] private int startPosX = 10;
    [SerializeField] private int posY = 30;
    [SerializeField] private int width = 150;
    [SerializeField] private int height = 90;
    [SerializeField] private int buffer = 10;

    private List<string> _itemList;
        
    private string _item;    
    
    private void OnGUI()
    {                
        int posX = startPosX;
        _itemList = Manager.Inventory.GetItemList();        
        foreach (string item in _itemList)
        {
            int count = Manager.Inventory.GetItemCount(item);
            Texture2D image = Resources.Load<Texture2D>("Icons/" + item);
            
            if (GUI.Button(new Rect(posX, posY, width, height), new GUIContent("(" + count + ")", image)))
            {
                Manager.Inventory.ConsumeItem(item);
                
                if (item == "health")
                {
                    Manager.Player.ChangeHealth(30);
                } 
                else if (item == "energy")
                {
                    Manager.Player.ChangeEnergy(30);
                }
                else if (item == "fire")
                {
                    Messenger.Broadcast(GameEvent.WEAPON_CHANGED);
                }
                else if (item == "key")
                {
                    for (int j = 0, itemCount = Manager.Inventory.GetItemCount(item); j < itemCount; ++j)
                    {
                        Manager.Inventory.ConsumeItem(item);
                    }
                    Messenger.Broadcast(GameEvent.LEVEL_COMPLETED);
                }
            }
            posX += width + buffer;
        }                
    }
}
