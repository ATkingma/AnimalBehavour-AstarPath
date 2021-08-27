using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
	[Header("Ui")]
	public GameObject target;
	public GameObject infoPannel;
	public Text buttonTextAutoSwi;
	public Text health;
	public Text food;
	public Text age;
	public Text intestine;
	public Text horney;
	public Text action;
	[Header("Other Values")]
	public List<GameObject> cams= new List<GameObject>();
	public List<GameObject> chickens = new List<GameObject>();
	public float secondsBeforeSwitch=1;
	public bool ui;
	private int index;
	private bool autoSwitch, switching,showInfo;
	private string normalTextBut;
	private void Start()
	{
		UpdateChickenInfo(chickens[index]);
		autoSwitch = false;
		target = cams[0];
		Invoke("LateStart", 0.05f);
		infoPannel.SetActive(showInfo);
		normalTextBut = buttonTextAutoSwi.text;
	}
	public void ShowInfo()
	{
		showInfo =!showInfo;
		infoPannel.SetActive(showInfo);
	}
	public void LateStart()
	{
		cams[0].SetActive(true);
	}
	private void Update()
	{
		if (autoSwitch)
		{
			if (!switching)
			{
				StartCoroutine("AutoSwitch");
			}
		}
		if (Input.GetButtonDown("Jump"))
		{
			AddIndex();
			Switch();
		}
		if (chickens[index] != null)
		{
			UpdateChickenInfo(chickens[index]);
		}
	}
	public void ResetTime()
	{
		Time.timeScale = 1;
	}
	public void TimeTimes2()
	{
		Time.timeScale = 2;
	}
	public void TimeTimes4()
	{
		Time.timeScale = 4;
	}
	public void SwitchUI()
	{
		ui = !ui;
	}
	public IEnumerator AutoSwitch()
	{
		switching = true;
		AddIndex();
		Switch();
		yield return new WaitForSeconds(secondsBeforeSwitch);
		switching = false;
	}
	public void ActivateAutoSwitch()
	{
		autoSwitch = !autoSwitch;
		if (autoSwitch)
		{
			buttonTextAutoSwi.text = "Stop Auto";
		}
		else
		{
			buttonTextAutoSwi.text = normalTextBut;
		}
	}
	public void AddIndex()
	{
		index++;
	}
	public void DecreseIndex()
	{
		index--;
	}
	public void Switch()
	{
		if (index > cams.Count-1)
		{
			index = 0;
		}
		if (index > cams.Count - 1)
		{
			index = cams.Count;
		}
		foreach (GameObject cam in cams)
		{
			cam.SetActive(false);
		}
		cams[index].SetActive(true);
		target = cams[index];
	}
	public void UpdateChickenInfo(GameObject chicken)
	{
		ChickenManager cm = chicken.GetComponent<ChickenManager>();
		health.text = cm.health.ToString();
		food.text = cm.food.ToString();
		age.text = cm.age.ToString();
		intestine.text=cm.intestineAmount.ToString();
		horney.text=cm.horneynis.ToString();
		action.text = chicken.GetComponent<StateManagerChicken>().activitie.text;
	}
}