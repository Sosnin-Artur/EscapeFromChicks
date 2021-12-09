using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, IGameManager
{    
    [SerializeField] private float intervalDuration = 3.0f;

    [SerializeField] private GameObject enemy;    
    [SerializeField] private GameObject[] items;    
        
    public ManagerStatus Status {get; private set;}
    
    public void Startup()
    {                        
        Status = ManagerStatus.Started;
    }
    
    public IEnumerator SpawnEnemy(bool[][] data, int blocksCount, float blockSize)
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalDuration / Manager.Mission.CurLevel);
            int spawnX = Random.Range(0, blocksCount);
            int spawnZ = Random.Range(0, blocksCount);

            if (!data[spawnX][spawnZ])
            {
                Instantiate(enemy, new Vector3(spawnX * blockSize, 0, -spawnZ * blockSize), Quaternion.identity);
            }            
        }        
    }    

    public IEnumerator SpawnItem(bool[][] data, int blocksCount, float blockSize)
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalDuration / Manager.Mission.CurLevel);
            int spawnX = Random.Range(0, blocksCount);
            int spawnZ = Random.Range(0, blocksCount);
            int index = Random.Range(0, items.Length);

            if (!data[spawnX][spawnZ])
            {
                Instantiate(items[index], new Vector3(spawnX * blockSize, 1.0f, -spawnZ * blockSize), Quaternion.identity);
            }            
        }        
    }    
}
