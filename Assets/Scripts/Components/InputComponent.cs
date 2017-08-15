using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    public int HorizontalDirection;
    public int VerticalDirection;

	public void Start ()
    {
		
	}
	
	public void Update ()
    {        
        var horizontalAxe = Input.GetAxis("Horizontal");
        var verticalAxe = Input.GetAxis("Vertical");

        if(horizontalAxe > 0.2f)
        {
            HorizontalDirection = 1;
        }
        else if(horizontalAxe < -0.2f)
        {
            HorizontalDirection = -1;
        }
        else
        {
            HorizontalDirection = 0;
        }

        if(verticalAxe > 0.2f)
        {
            VerticalDirection = 1;
        }
        else if(verticalAxe < -0.2)
        {
            VerticalDirection = -1;
        }
        else
        {
            VerticalDirection = 0;
        }
        
	}
}
