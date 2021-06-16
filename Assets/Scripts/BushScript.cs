using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushScript : MonoBehaviour
{
	public LayerMask tileLayer;
	public GameObject tile;
	public int berryAmount;
	public float distanceToBeClose=2;
	public float TimeToGrowABerry = 25f;
	public float timeToResetBool=0.5f;
	private float timer;
	private float closesRange;
	private bool addedBerry;
	private void Start()
	{
		berryAmount = 10;
		Invoke("Ray", 0.25f);
	}
	public void Ray()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, -Vector3.up, out hit, 100,tileLayer))
		{
			tile = hit.transform.gameObject;
		}
	}
	private void Update()
	{
		Ray();
		if (berryAmount <= 9)
		{
			timer += Time.deltaTime;
			if (timer >= TimeToGrowABerry)
			{
				if (!addedBerry)
				{
					AddBerry();
				}
			}
		}
		else if (berryAmount > 10)
		{
			timer = 0;
		}
	}
	public void AddBerry()
	{
		timer = 0;
		addedBerry = true;
		berryAmount++;
		Invoke("ResetBerryBool", timeToResetBool);
	}
	public void ResetBerryBool()
	{
		addedBerry = false;
	}
	private void OnDrawGizmos()
	{
		//Debug.DrawRay(transform.position, Vector3.up, Color.white);
	}
}