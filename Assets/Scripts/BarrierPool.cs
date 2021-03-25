using UnityEngine;

public class BarrierPool : MonoBehaviour 
{
	public GameObject[] barrierPrefabs;									
	public int barrierPoolSize = 5;									
	public float spawnRate = 3f;									
	public float barrierMin = -1f;									
	public float barrierMax = 3.5f;									

	private GameObject[] barriers;									
	private int currentBarrier = 0;									

	private Vector2 objectPoolPosition = new Vector2 (-15,-25);		
	private float spawnXPosition = 10f;

	private float timeSinceLastSpawned;


	void Start()
	{
		timeSinceLastSpawned = 0f;

		barriers = new GameObject[barrierPoolSize];
		for(int i = 0; i < barrierPoolSize; i++)
		{
			barriers[i] = (GameObject)Instantiate(barrierPrefabs[Random.Range(0, barrierPrefabs.Length)], objectPoolPosition, Quaternion.identity);
		}
	}


	void Update()
	{
		timeSinceLastSpawned += Time.deltaTime;

		if (GameController.instance.gameOver == false && timeSinceLastSpawned >= spawnRate) 
		{	
			timeSinceLastSpawned = 0f;

			float spawnYPosition = Random.Range(barrierMin, barrierMax);

			barriers[currentBarrier].transform.position = new Vector2(spawnXPosition, spawnYPosition);

			currentBarrier ++;
			if (currentBarrier >= barrierPoolSize) 
			{
				currentBarrier = 0;
			}
		}
	}
}