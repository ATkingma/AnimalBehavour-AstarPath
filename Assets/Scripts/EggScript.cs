using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EggScript : MonoBehaviour
{
	public GameObject chickenPrefab;
	public Vector3 grotwAmount= new Vector3(0.05f, 0.05f, 0.05f);
	private Vector3 growVec;
	private bool spawnedChicken;
    void Update()
    {
		if (transform.localScale.x >1)
		{
			transform.localScale = new Vector3(1, 1, 1);
			if (!spawnedChicken)
			{
				SpawnChicken();
			}
		}
		else 
		{
			growVec += grotwAmount * Time.deltaTime;
			transform.localScale = growVec;
		}
    }
	public void SpawnChicken()
	{
		spawnedChicken = true;
		Instantiate(chickenPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}