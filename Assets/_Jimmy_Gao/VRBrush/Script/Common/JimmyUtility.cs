using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyUtility  {

	public static float VectorAngle(Vector2 from, Vector2 to)
	{  
		float angle;  
		Vector3 cross = Vector3.Cross (from, to);  
		angle = Vector2.Angle (from, to);  
		return cross.z > 0 ? -angle : angle; 
	}


	//1 up 2 down 3 left 4 right
	public static int AngleToPosition(float angle)
	{
		int pos=0;
		if (angle >= 45 && angle <= 135)  
		{  
			pos = 2;
		}  
		else if (angle <= -45 && angle >= -135)  
		{  
			pos = 1;
		}  
		else if ((angle <= 180 && angle >= 135) || (angle <= -135 && angle >= -180))  
		{  
			pos=3;
		}  
		else if ((angle >= 0 && angle <= 45) || (angle >= -45 && angle <= 0))  
		{  
			pos = 4;
		}
		return pos;
	}
}
