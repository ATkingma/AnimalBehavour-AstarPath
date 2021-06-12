using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedingStateChicken : StateChicken
{
	public float timeToBreed=30;
	public float feelingsAddMin=15;
	public GameObject eggPrefab;
	public IdleStateChicken idle;
	private float timer;
	private bool givingBirth, gaveBrith;
	public override StateChicken RunCurrentState()
	{
		Timer();
		if (gaveBrith)
		{
			givingBirth = false;
			gaveBrith = false;
			cm.horneynis = 0;
			timer = 0;
			return idle;
		}
		return this;
	}
	public void Timer()
	{
		timer += Time.deltaTime;
		if(timer>= timeToBreed)
		{
			if (!givingBirth)
			{
				GiveBirth();
			}
		}
	}
	public void GiveBirth()
	{
		givingBirth = true;
		int randomNumb = Random.Range(0, 200);
		if (randomNumb <= 190)
		{
			cm.feelingsValue += feelingsAddMin;
			Instantiate(eggPrefab, transform.position, Quaternion.identity);
			gaveBrith = true;
		}
		else
		{
			cm.feelingsValue -= feelingsAddMin;
			gaveBrith = true;
		}
	}
}