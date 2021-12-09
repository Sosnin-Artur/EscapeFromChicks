using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour, IGameManager
{        
    [SerializeField] private GameObject wall;

    private bool[][] _data;        
    
    public ManagerStatus Status {get; private set;}
    
    public void Startup()
    {        
        Status = ManagerStatus.Started;        
    }  

    public void GenerateNewDataMaze(int sizeRows, int sizeCols, float placementThreshold = 0.1f)
    {
        _data = new bool[sizeRows][];
        for (int i = 0; i < sizeRows; ++i)
        {
            _data[i] = new bool[sizeCols];    
        }
        
        int rMax = sizeRows - 1;
        int cMax = sizeCols - 1;

        for (int i = 0; i < sizeRows; ++i)
        {
            for (int j = 0; j < sizeCols; ++j)
            {
                if (i == 0 || j == 0 || i == rMax || j ==cMax)
                {
                    _data[i][j] = true;
                }
                else if ((i & 1) == 0 && (j & 1) == 0)
                {
                    if (Random.value > placementThreshold)
                    {
                        _data[i][j] = true;
                        
                        int a = Random.value < 0.5f ? 0 : (Random.value < 0.5f ? -1 : 1);
                        int b = a != 0 ? 0 : (Random.value < 0.5f ? -1 : 1);
                        
                        _data[i + a][j + b] = true;
                    }
                }
            }
        }        
    }
    
    public void GenerateMaze(float wallSize, int sizeRows, int sizeCols)
    {        
        int rows = sizeRows - 1;
        int cols = sizeCols - 1;
         
        for (int i = 0; i <= rows; ++i)
        {
            for (int j = 0; j <= cols; ++j)
            {
                if (_data[i][j])
                {
                    Instantiate(wall, new Vector3(wallSize * i, 5, -wallSize * j), wall.transform.rotation);
                }
            }
        }        
    }
    
    public bool[][] GetData()
    {
        return _data;
    }

    public void UpdateData(bool[][] data)
    {
        _data = data;
    }
}
