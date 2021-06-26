using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(AStar))]
public class CustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		AStar astar = (AStar)target;
		if (GUILayout.Button("Debug"))
		{
			astar.ChangeDebugMode();
		}
	}
}