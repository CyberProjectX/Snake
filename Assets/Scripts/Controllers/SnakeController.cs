using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float Speed = 0.1f;

    public int StartLength = 3;

    private float LastUpdate;

    public MainController MainController;

    public InputComponent InputComponent;

    public Queue<Transform> Cells;

    public Transform Head;

    public Transform Cell;

    public int HorizontalDirection;

    public int VerticalDirection;

	public void Start ()
    {
        CreateSnake();        
    }
	
	public void Update ()
    {
        if (InputComponent.HorizontalDirection != 0 && InputComponent.HorizontalDirection != -HorizontalDirection)
        {
            VerticalDirection = 0;
            HorizontalDirection = InputComponent.HorizontalDirection;
        }

        if (InputComponent.VerticalDirection != 0 && InputComponent.VerticalDirection != -VerticalDirection)
        {
            VerticalDirection = InputComponent.VerticalDirection;
            HorizontalDirection = 0;
        }
        
        if (LastUpdate + Speed < Time.time)
        {
            Vector3 newPosition = Head.position + 0.5f * new Vector3(HorizontalDirection, VerticalDirection);            

            if (IsFacedWithTail(newPosition))
            {
                CreateSnake();
            }
            else
            {
                Head = Instantiate(Cell, newPosition, Quaternion.identity, Head.parent);

                Cells.Enqueue(Head);

                //eat
                if (Vector3.Distance(MainController.Apple.transform.position, Head.position) < 0.1f)
                {
                    MainController.DestroyApple();
                }
                else
                {
                    var oldTail = Cells.Dequeue();
                    Destroy(oldTail.gameObject);
                }
            }            

            LastUpdate = Time.time + Speed;
        }
	}

    private bool IsFacedWithTail(Vector3 position)
    {
        foreach (var cell in Cells)
        {
            if (Vector3.Distance(cell.position, position) < 0.1f)
            {
                return true;
            }
        }

        return false;
    }

    private void CreateSnake()
    {
        if (Cells != null)
        {
            var cellArray = Cells.ToArray();
            for (int i = 0; i < cellArray.Length; i++)
            {
                Destroy(cellArray[i].gameObject);
            }
        }                

        Cells = new Queue<Transform>();

        var x = 0f;
        var y = -5f;
        for(int i = 0; i < StartLength; i++)
        {
            Head = Instantiate(Cell, new Vector3(x, y), Quaternion.identity, this.transform);

            Cells.Enqueue(Head);

            y += 0.5f;
        }

        HorizontalDirection = 0;
        VerticalDirection = 1;
    }
}
