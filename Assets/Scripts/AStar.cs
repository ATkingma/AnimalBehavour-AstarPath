using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AStar : MonoBehaviour
{
	//public
    [Header("Settings")]
    public Vector2 gridSize;
    public float tileSize;
    public string[] layersThatCountAsObstacle;
	public string[] layersThatCountAsRoughTerrain;
	[Header("Debugging")]
	public GameObject tile;
    public GameObject startTile;
    public GameObject endTile;
    public GameObject[,] tiles;
	public List<GridTile> tilesToCheck = new List<GridTile>();
	public List<GridTile> path = new List<GridTile>();
	//private
    private bool debugging;
	private int tilesToCheckNum = 0;
	private bool pathmade = false;
    void Start()
    {
        CreateGrid();
    }
    void CreateGrid()
    {
        tiles = new GameObject[(int)gridSize.y,(int)gridSize.x];

        int x = 0;
        int y = 0;

        for (int i = 0; i < (gridSize.x * gridSize.y); i++)
        {
            tiles[y, x] = Instantiate(tile, new Vector3(x * tileSize + transform.position.x, transform.position.y, y * tileSize + transform.position.z), Quaternion.identity, transform);
            tiles[y, x].transform.localScale = new Vector3(tileSize * .1f, tileSize * .1f, tileSize * .1f);
            tiles[y, x].GetComponent<GridTile>().aStar = transform.GetComponent<AStar>();
            if (debugging)
            {
                tiles[y, x].GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                tiles[y, x].GetComponent<MeshRenderer>().enabled = false;
            }
            if (x == (int)gridSize.x -1)
            {
                x = 0;
                y ++;
            }
            else
            {
                x ++;
            }       
        }
    }
    void UpdateGrid(Transform target, Transform start)
    {
        int x = 0;
        int y = 0;

        for (int i = 0; i < (gridSize.x * gridSize.y); i++)
        {
            tiles[y, x].GetComponent<GridTile>().UpdateGrid(target, start);
            if (x == (int)gridSize.x - 1)
            {
                x = 0;
				y++;
            }
            else
            {
				x++;
            }
        }
    }
    public void Pathfind(Transform target, Transform start)	
    {
        pathmade = false;
        tilesToCheckNum = 0;
        path = new List<GridTile>();
        tilesToCheck = new List<GridTile>();
        startTile = null;
        endTile = null;

        UpdateGrid(target, start);
        int x = 0;
        int y = 0;
        for (int i = 0; i < (gridSize.x * gridSize.y); i++)
        {
            tiles[y, x].GetComponent<GridTile>().CalculateDistances();
            if (x == (int)gridSize.x - 1)
            {
                x = 0;
				y++;
            }
            else
            {
				x++;
            }
        }
        startTile.GetComponent<GridTile>().FindLowestdfEnd();
        LookForPath(start);
    }
    public void LookForPath(Transform ai)
    {
        int tilecheckCount = tilesToCheck.Count;
        for (int i = tilesToCheckNum; i < tilecheckCount; i++)
        {
            if (tilesToCheck[i].transform == endTile.transform)
            {
                tilesToCheck[i].MakePath();
                pathmade = true;

                for (int _i = path.Count - 1; _i > -1; _i--)
                {
                    ai.GetComponent<AI>().waypoints.Add(path[_i].transform.position);
                }
            }
            tilesToCheckNum = i;
            tilesToCheck[i].FindLowestdfEnd();
        }
        if (!pathmade && tilecheckCount != tilesToCheck.Count)
        {
            LookForPath(ai);
        }
    }
	#region debugging;
	public void ChangeDebugMode()
	{
		int x = 0;
		int y = 0;

		debugging = !debugging;

		if (tiles == null)
		{
			return;
		}
		for (int i = 0; i < (gridSize.x * gridSize.y); i++)
		{
			if (debugging)
			{
				tiles[y, x].GetComponent<MeshRenderer>().enabled = true;
			}
			else
			{
				tiles[y, x].GetComponent<MeshRenderer>().enabled = false;
			}
			if (x == (int)gridSize.x - 1)
			{
				x = 0;
				y++;
			}
			else
			{
				x++;
			}
		}
	}
	#endregion 
}