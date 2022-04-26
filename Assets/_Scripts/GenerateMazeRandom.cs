using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMazeRandom : MonoBehaviour{
    [SerializeField][Range(5, 100)] private int gridWidth, gridHeight;
    [SerializeField][Range(5, 100)] private int wallSize, wallHeight;
    [SerializeField] private Transform ground, ceiling;
    [SerializeField] private GameObject verticalWall, horizontalWall;
    GameObject[,] gridObjectsH, gridObjectsV;
    GameObject[] allObjectsInScene;
    private const int N = 1, S = 2, E = 3, W = 4;
	int [,] grid;

    void Start() {
        verticalWall.transform.localScale = new Vector3(.1f, wallHeight, wallSize);
        horizontalWall.transform.localScale = new Vector3(wallSize, wallHeight, .1f);

        grid = new int[gridWidth, gridHeight];
        gridObjectsV = new GameObject[gridWidth + 1, gridHeight + 1];
        gridObjectsH = new GameObject[gridWidth + 1, gridHeight + 1];
        DrawFullGrid();

        ground.transform.localScale = new Vector3((gridWidth + 1) * wallSize, 1, (gridHeight + 1) * wallSize);
        ceiling.transform.localScale = new Vector3((gridWidth + 1) * wallSize, 1, (gridHeight + 1) * wallSize);
        ceiling.transform.position = new Vector3(ceiling.transform.position.x, wallSize * 2 -1, ceiling.transform.position.z);

        GenerateMazeBinary();
        DisplayGrid();

        GetComponent<NavMeshBaker>().BakeMap();
    }
	
	private void DrawFullGrid() {
        float wallSize;
        float xOffset, zOffset;
        for (int i = 0; i <= gridHeight; i++) {
			for (int j = 0; j <= gridWidth; j++) {
				if (i < gridHeight) {
					wallSize = verticalWall.transform.localScale.z;
					xOffset = - (gridWidth * wallSize)/2;
					zOffset = - (gridHeight* wallSize)/2;
					gridObjectsV[j,i] = Instantiate(verticalWall, new Vector3(-wallSize/2+j*wallSize+xOffset,wallSize, i*wallSize+zOffset),Quaternion.identity);
                    gridObjectsV[j,i].SetActive(true);
                    gridObjectsV[j,i].transform.parent = transform;

                }
				
				if (j < gridWidth) {
					wallSize = horizontalWall.transform.localScale.x;
					xOffset = -(gridWidth * wallSize)/2;
					zOffset = -(gridHeight * wallSize)/2;
					gridObjectsH[j,i] = Instantiate(horizontalWall, new Vector3(j*wallSize+xOffset, wallSize, - (wallSize/2) + i*wallSize+zOffset), Quaternion.identity);
                    gridObjectsH[j,i].SetActive(true);
                    gridObjectsH[j,i].transform.parent = transform;
                }
			}
		}
	}

    void GenerateMazeBinary() {
        float randomNumber;
        int carvingDirection = 0;
        for (int row = 0; row < gridHeight; row++) {
            for (int cell = 0; cell < gridWidth; cell++) {
                randomNumber = Random.Range(0, 100);
                if (randomNumber > 50) carvingDirection = N; else carvingDirection = E;
                if (cell == gridWidth - 1) {
                    if (row < gridHeight - 1) carvingDirection = N; else carvingDirection = W;
                }
                else if (row == gridHeight - 1) {
                    if (cell < gridWidth - 1) carvingDirection = E; else carvingDirection = -1;
                }
                grid[cell, row] = carvingDirection;
            }
        }
    }

    void DisplayGrid() {
        for (int row = 0; row < gridHeight; row++) {
            for (int cell = 0; cell < gridWidth; cell++) {
                if (grid[cell, row] == N) gridObjectsH[cell, row + 1].SetActive(false);
                if (grid[cell, row] == S) gridObjectsH[cell, row - 1].SetActive(false);
                if (grid[cell, row] == E) gridObjectsV[cell + 1, row].SetActive(false);
                if (grid[cell, row] == W) gridObjectsV[cell - 1, row].SetActive(false);
            }
        }
    }
}