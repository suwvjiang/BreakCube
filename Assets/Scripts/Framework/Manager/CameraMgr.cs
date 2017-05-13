using UnityEngine;
using System.Collections;

/// <summary>
/// 镜头管理器
/// design by:jiangchufei@gmail.com
/// date: 2017-5-13
/// </summary>
/// 

public class CameraMgr : MonoBehaviour 
{
	//定义鼠标按键枚举
	private enum MouseButton
	{
		//鼠标左键
		MouseButton_Left,
		//鼠标右键
		MouseButton_Right,
		//鼠标中键
		MouseButton_Midle
	}

	//观察目标
	public Transform Target;

	//观察距离
	public float Distance = 5F;

	//旋转速度
	private const float SpeedX = 4.8f;
	private const float SpeedY = 2.4f;

	//角度限制
	private const float  MinLimitY = -90;
	private const float  MaxLimitY = 90;
	
	//鼠标缩放速率
	private const float ZoomSpeed = 2f;
	//鼠标缩放距离最值
	private const float MaxDistance = 10;
	private const float MinDistance = 1.5f;

	//旋转角度
	private float m_x = 0.0f;
	private float m_y = 0.0f;
	//存储角度的四元数
	private Quaternion m_rotation;
	//屏幕坐标
	private Vector3 m_screenPoint;

	void Start () 
	{
		//初始化旋转角度
		m_x = transform.eulerAngles.x;
		m_y = transform.eulerAngles.y;
	}
	
	void LateUpdate () 
	{
		CheckCamera();
	}

	//检测镜头运动
	private void CheckCamera()
	{
		if(Target == null)
			return;
		
		//鼠标右键旋转
		if(Input.GetMouseButton((int)MouseButton.MouseButton_Right))
		{
			//获取鼠标输入
			m_x += Input.GetAxis("Mouse X") * SpeedX;
			m_y -= Input.GetAxis("Mouse Y") * SpeedY;
			//范围限制
			m_y = ClampAngle(m_y,MinLimitY,MaxLimitY);
			//计算旋转
			m_rotation = Quaternion.Euler(m_y, m_x, 0);
			//根据是否插值采取不同的角度计算方式
			transform.rotation = m_rotation;
		}

		//鼠标滚轮缩放
		Distance -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
		Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);
		
		//重新计算位置
		Vector3 pos = m_rotation * new Vector3(0.0F, 0.0F, -Distance) + Target.position;
		//设置相机的位置
		transform.position = pos;
	}
	
	//角度限制
	private float ClampAngle (float angle,float min,float max) 
	{
		if (angle < -360)
			angle += 360;
		if (angle >  360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}