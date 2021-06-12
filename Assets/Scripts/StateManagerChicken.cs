using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerChicken : MonoBehaviour
{
	public StateChicken curentState;
	private void Update()
	{
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
	}
}