﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedingStateChicken : StateChicken
{
	public GameObject chicken;
	public AStar astar;
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
		if(timer>= timeToBreed/2)
		{
			if (!givingBirth)
			{
				StartCoroutine("GiveBirth");
			}
		}
	}
	public IEnumerator GiveBirth()
	{
		givingBirth = true;
		int randomPos = Random.Range(0, astar.tilesToCheck.Count);
		chicken.GetComponent<AI>().target = astar.tilesToCheck[randomPos].transform.gameObject;
		yield return new WaitForSeconds(timeToBreed/2);
		int randomNumb = Random.Range(0, 200);
		if (randomNumb <= 140)
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