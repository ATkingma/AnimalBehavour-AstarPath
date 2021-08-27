using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StateManagerChicken : MonoBehaviour
{
	public Text activitie;
	public Image ActivitieImage;
	public GameObject ui;
	public StateChicken curentState;
	private CameraSwitcher camSwitch;
	private void Start()
	{
		camSwitch = FindObjectOfType<CameraSwitcher>();
		activitie.text = curentState.stateName;
		ActivitieImage.color = curentState.activitieColor;
	}
	private void Update()
	{
		if (camSwitch.ui)
		{
			if (!ui.active)
			{
				ui.SetActive(true);
			}
		}
		else
		{
			if (ui.active)
			{
				ui.SetActive(false);
			}
		}
		RunStateMachine();
	}
	private void RunStateMachine()
	{
		StateChicken nextState = curentState?.RunCurrentState();

		if(nextState != null)
		{
			SwitchOnNextState(nextState);
		}
	}
	private void SwitchOnNextState(StateChicken nextState)
	{
		curentState = nextState;
		NewState();
	}
	public void NewState()
	{
		activitie.text = curentState.stateName;
		ActivitieImage.color = curentState.activitieColor;
	}
}