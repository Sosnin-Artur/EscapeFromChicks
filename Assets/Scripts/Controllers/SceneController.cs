using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour 
{
	[SerializeField] private int mazeSize;
    [SerializeField] private float blockOffset;

	[SerializeField] private GameObject pauseText;
    [SerializeField] private GameObject menu;

    private float _timeScale;
    
	public bool isPaused {get; private set;}

    
	public void Pause()
	{
		if (!isPaused)
		{
			Time.timeScale = 0;			
		}
		else
		{
			Time.timeScale = _timeScale;			
		}
        isPaused = !isPaused;
		pauseText.SetActive(isPaused);
		menu.SetActive(isPaused);
	}	
	
    private void Start() 
	{	   
		Time.timeScale = 1;
		_timeScale = Time.timeScale;				
		Manager.Maze.GenerateMaze(blockOffset, mazeSize * Manager.Mission.CurLevel, mazeSize * Manager.Mission.CurLevel);			
				
		StartCoroutine(Manager.Spawn.SpawnEnemy(Manager.Maze.GetData(), mazeSize, mazeSize));			
		StartCoroutine(Manager.Spawn.SpawnItem(Manager.Maze.GetData(), mazeSize, mazeSize));				
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
            Pause();
		}		
	}
}

