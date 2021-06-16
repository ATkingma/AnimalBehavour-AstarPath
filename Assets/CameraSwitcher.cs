using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
	public GameObject[] cams;
	private int index;
	private void Start()
	{
		cams = GameObject.FindGameObjectsWithTag("MainCamera");
		foreach(GameObject cam in cams)
		{
			cam.SetActive(false);
		}
		cams[index].SetActive(true);
	}
	private void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			Switch();
		}
	}
	public void Switch()
	{
		index++;
		if (index > cams.Length-1)
		{
			index = 0;
		}
		foreach (GameObject cam in cams)
		{
			cam.SetActive(false);
		}
		cams[index].SetActive(true);
	}
}