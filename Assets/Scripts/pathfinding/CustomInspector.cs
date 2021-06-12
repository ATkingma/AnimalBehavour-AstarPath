using UnityEngine;
using UnityEditor;
//[CustomEditor(typeof(AStarManager))]
public class CustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		//AStarManager astar = (AStarManager)target;
		if (GUILayout.Button("Debug"))
		{
			//astar.Debuging();
		}
	}
}