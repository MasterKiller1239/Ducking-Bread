using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[Header("Objects")]
	[SerializeField] private GameObject spawnObject;
	

	[Header("Spawning parameters")]
	[SerializeField] private float MinSpawnDelay = 1;
	[SerializeField] private float MaxSpawnDelay = 1;
	public bool onCommand = false;
	public bool spawnedAll = false;

	private Mesh planeMesh;
	private Bounds bounds;
	private GameObject plane;
	private List<GameObject> spawnedObjects;
	Timer spawnTimer;
	
	void Start()
    {
		plane = GameObject.FindWithTag("Walkable");
		planeMesh = plane.GetComponent<MeshFilter>().mesh;
		bounds = planeMesh.bounds;

		// Create and start timer
		spawnTimer = gameObject.AddComponent<Timer>();
		spawnTimer.Duration = Random.Range(MinSpawnDelay, MaxSpawnDelay);
		spawnTimer.Run();
	}

    void Update()
    {
		if (!onCommand && spawnTimer.Finished)
        {
			ObjectSpawn();

			// Change spawn timer duration and restart
			spawnTimer.Duration = Random.Range(MinSpawnDelay, MaxSpawnDelay);
			spawnTimer.Run();
		}
	}

	public void DeleteFromList(GameObject givenObject)
    {
		spawnedObjects.Remove(givenObject);

		if (GameController.Instance.feeding && spawnedObjects.Count < 2)
			SpawnNextObject();
    }

	// Spawns number of objects at a random locations on a plane
	public void SpawnOnCommand(int objectsToSpawn)
    {
		spawnedObjects = new List<GameObject>();
		spawnedAll = false;
		IEnumerator spawningObjects = Spawning(objectsToSpawn);
		StartCoroutine(spawningObjects);
	
	}

	IEnumerator Spawning(int objectsToSpawn)
    {
		while(objectsToSpawn > 0)
        {
			spawnedObjects.Add(ObjectSpawn());
			objectsToSpawn--;
			yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
		}
    }

	public void SpawnNextObject()
    {
		spawnedObjects.Add(ObjectSpawn());
	}

	public GameObject ObjectSpawn()
	{
		Vector3 randomPosition = GetARandomPos();                                                  
        return Instantiate<GameObject>(spawnObject, randomPosition, Quaternion.identity);   
	}

	public Vector3 GetARandomPos()
	{ 

		Vector3 randomPosition;
		
		float minX = plane.transform.position.x - plane.transform.localScale.x * bounds.size.x * 0.5f;
		float minZ = plane.transform.position.z - plane.transform.localScale.z * bounds.size.z * 0.5f;

		do
		{
			randomPosition = new Vector3(Random.Range(minX + 1, (-minX) - 1),
									plane.transform.position.y,
									Random.Range(minZ + 1, -minZ - 1));
		} while (!Checkforfreespace(randomPosition));
		
		return randomPosition;
	}

	public bool Checkforfreespace(Vector3 v)
	{
		v.y += 15f;
		float maxDistance = 100f;
	//	RaycastHit hit;

		//bool isHit =
		//	Physics.BoxCast(v, spawnObject.transform.lossyScale, new Vector3(0,-1,0), out hit,
		//	spawnObject.transform.rotation, maxDistance);

		//if (isHit)
		//{
		//	if (hit.transform.tag == "Food" || hit.transform.tag == "Ground" || hit.transform.tag == "Duck")
		//	{
		//		Debug.Log("Spawned object on " + hit.transform.tag + ". Randomising position again.");

		//		return false;
		//	}
		//	else
		//		return true;

		//}
		RaycastHit[] tableofhits = Physics.BoxCastAll(v, spawnObject.transform.lossyScale, new Vector3(0, -1, 0),
			spawnObject.transform.rotation, maxDistance);
		if(tableofhits!=null)
        {
			foreach(RaycastHit hit in tableofhits)
            {
                if (hit.transform.tag == "Food" || hit.transform.tag == "Ground" || hit.transform.tag == "Duck")
                {
                  //  Debug.Log("Spawned object on " + hit.transform.tag + ". Randomising position again.");

                    return false;
                }
            }
			return true;
		}

		return true;
	}
}
