using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
	public List<GameObject> cams= new List<GameObject>();
	private int index;
	private void Start()
	{
		Invoke("LateStart", 0.05f);
	}
	public void LateStart()
	{
		cams[0].SetActive(true);
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
		if (index > cams.Count-1)
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