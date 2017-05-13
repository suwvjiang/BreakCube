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
	public Camera MainCamera;

	private bool m_clickable;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckInput();
	}

	private void CheckInput()
	{
		if(MainCamera == null)
			return;

		if(m_clickable && Input.GetMouseButtonDown(0))
		{
			//从摄像机发出到点击坐标的射线
			Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
			if(Physics.Raycast(ray, out hitInfo, 100f, 1<<8))
			{
				//划出射线，只有在scene视图中才能看到
				Debug.DrawLine(ray.origin,hitInfo.point);
				BaseCube cube = hitInfo.collider.gameObject.GetComponent<BaseCube>();
				Debug.Log("click object name is " + cube.name);
			}
		}
	}
}
