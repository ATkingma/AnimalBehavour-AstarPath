using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public CameraSwitcher camSwitch;
    void Start()
    {
        camSwitch = FindObjectOfType<CameraSwitcher>();
    }
    void Update()
    {
		if (camSwitch.target == null)
		{
            return;
		}
        transform.LookAt(camSwitch.target.transform);
    }
}
