using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public Transform ApplePrefab;

    public GameObject Apple;

    public SnakeController SnakeController;

    public float FieldWidth;

    public float FieldHeight;

	void Start ()
    {
        
	}
	
	void Update ()
    {
		if(Apple == null)
        {
            CreateNewApple();
        }
	}

    public void CreateNewApple()
    {
        var applePosition = GetNewRandomPosition();

        while (IsInSnake(applePosition))
        {
            applePosition = GetNewRandomPosition();
        }
        
        Apple = Instantiate(ApplePrefab, applePosition, Quaternion.identity).gameObject;
    }

    public void DestroyApple()
    {
        Destroy(Apple);
        Apple = null;
    }

    private Vector3 GetNewRandomPosition()
    {
        var newX = Mathf.Round(Random.Range(-FieldWidth / 2, FieldWidth / 2));
        var newY = Mathf.Round(Random.Range(-FieldHeight / 2, FieldHeight / 2));

        return new Vector3(newX, newY);
    }

    private bool IsInSnake(Vector3 position)
    {
        var cells = SnakeController.Cells.ToArray();

        foreach(var cell in cells)
        {
            if(Vector3.Distance(cell.position, position) < 0.1f)
            {
                return true;
            }
        }

        return false;
    }
}
