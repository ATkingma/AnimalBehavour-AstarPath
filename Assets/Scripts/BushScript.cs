using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushScript : MonoBehaviour
{
	public int berryAmount;
	public float TimeToGrowABerry = 25f;
	public float timeToResetBool=0.5f;
	private float timer;
	private bool addedBerry;
	private void Start()
	{
		berryAmount = 10;
	}
	private void Update()
	{
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
}