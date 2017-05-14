using UnityEngine;
using System.Collections;

/// <summary>
/// 输入管理器
/// design by:jiangchufei@gmail.com
/// date: 2017-5-13
/// </summary>
/// 

public class InputMgr : MonoBehaviour 
{
	#region Static
	static private InputMgr m_instance;
	static public InputMgr Instance
	{
		get
		{
			return m_instance;
		}
	}

	#endregion

	public Camera MainCamera;
	public GameStatus Status = GameStatus.None;
	public CubeModel Target;

	private bool m_clickable = true;
	private InputState m_inputState = InputState.None;
	
	// Use this for initialization
	void Awake () 
	{
		m_instance = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckInput();
	}

	private void CheckInput()
	{
		if(MainCamera == null || Target == null)
			return;

		if(Input.GetKey(KeyCode.D))
		{
			m_inputState = InputState.Deletion;
		}
		else if(Status == GameStatus.Editor && Input.GetKey(KeyCode.A))
		{
			m_inputState = InputState.Addition;
		}
		else if(Input.GetKey(KeyCode.W))
		{
			m_inputState = InputState.Mark;
		}
		else
			m_inputState = InputState.None;

		if(m_clickable && Input.GetMouseButtonDown(0))
		{
			//从摄像机发出到点击坐标的射线
			Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
			if(Physics.Raycast(ray, out hitInfo, 100f, 1<<8))
			{
				//划出射线，只有在scene视图中才能看到
				Debug.DrawLine(ray.origin, hitInfo.point);
				Cube target = hitInfo.collider.gameObject.GetComponent<Cube>();
				if(target != null)
				{
					Vector3 point = target.transform.InverseTransformPoint(hitInfo.point);

					switch(m_inputState)
					{
						case InputState.Mark:
							//cube.Mark();
							break;
						case InputState.Deletion:
							Target.DeleteCube(target);
							break;
						case InputState.Addition:
							CubeFaceType face = ClickFace(point);
							Target.AddCube(target, face);
							break;
					}//end switch
				}//end null
			}//end ray
		}//end buttondown
	}//end function

	//检测点击面
	private CubeFaceType ClickFace(Vector3 point)
	{
		float x = Mathf.Abs(point.x);
		float y = Mathf.Abs(point.y);
		float z = Mathf.Abs(point.z);

		if((x >= y) && (x >= z))
		{
			if(point.x > 0)
				return CubeFaceType.Right;
			 else
			 	return CubeFaceType.Left;
		}
		else if((y > x) && (y >= z))
		{
			if(point.y > 0)
				return CubeFaceType.Top;
			else
				return CubeFaceType.Bottom;
		}
		else
		{
			if(point.z > 0)
				return CubeFaceType.Front;
			else
				return CubeFaceType.Back;
		}
	}
}
