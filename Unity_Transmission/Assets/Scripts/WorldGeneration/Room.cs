﻿using UnityEngine;
using System.Collections;
using System.Text;

public class Room : MonoBehaviour {

	public bool north;
	public bool east;
	public bool south;
	public bool west;

	public int[,] grid {
		get {
			if (_grid == null) {
				_grid = new int[,] {
					{ 0, 			east ? 1 : 0, 				0 },
					{ south ? 1 : 0		, 1, 		north ? 1 : 0 },
					{ 0, 			west ? 1 : 0, 				0 }
				};
			}

			return _grid;
		}
	}

	private int[,] _grid;

	internal float rotation;
	//public Collider[] possibleExits;

	public void Rotate () {
		//Could be more optimized but who cares
		int[,] newGrid = new int[3, 3];
		for (int i = 0; i < 3; ++i) {
			for (int j = 0; j < 3; ++j) {
				newGrid[i, j] = grid[2-j, i];
			}
		}
		_grid = newGrid;
		transform.Rotate (0, 90, 0);
	}

	void Start()
	{
		//possibleExits = GetComponentsInChildren<SphereCollider> ();

	}

	public Exit[] getExits ()
	{
		return GetComponentsInChildren<Exit> ();

		//return GetComponent<Room> ();
	}


}
