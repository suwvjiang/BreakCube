using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 模型
/// design by:jiangchufei@gmail.com
/// date: 2017-5-14
/// </summary>
/// 

public class CubeModel:MonoBehaviour
{
	static public CubeModel Create(ModelInfo info)
	{
		if(info == null)
			return null;
		
		GameObject go = new GameObject("model"+info.ID);
		CubeModel model = go.AddComponent<CubeModel>();
		model.Data = info;

		return model;
	}

	private ModelInfo m_info;
	private bool m_editable = false;//可编辑
	private List<Cube> m_cubes;

	private Vector3 m_origin;
	private Vector3 m_peak;

	public ModelInfo Data
	{
		get
		{
			return m_info;
		}
		set
		{
			m_info = value;
			CreateCubes();
		}
	}

	public bool Editable
	{
		get
		{
			return m_editable;
		}
		set
		{
			m_editable = value;
		}
	}

	///创建方块堆
	private void CreateCubes()
	{
		if(m_info == null)
			return;
		
		Cube prefab = Resources.Load<Cube>("Prefabs/Cube");
		if(prefab == null)
			return;

		m_cubes = new List<Cube>();
		for(int i = 0; i < m_info.Cubes.Count; ++i)
		{
			CubeModelData data = m_info.Cubes[i];
			if(data == null)
				continue;
			
			Cube cube = GameObject.Instantiate<Cube>(prefab);
			cube.transform.parent = transform;
			cube.transform.localPosition = data.Pos;
			cube.ModelData = data;
			m_cubes.Add(cube);
			UpdateWHD(cube);
		}
	}

	public bool AddCube(Cube target, CubeFaceType face)
	{
		if(!m_editable)
			return false;
		Cube prefab = Resources.Load<Cube>("Prefabs/Cube");
		if(prefab == null)
			return false;

		int current = 1;
		Vector3 offset = Vector3.right;
		switch(face)
		{
			case CubeFaceType.Top:
				offset = Vector3.up;
				current = m_info.Heigth;
				break;
			case CubeFaceType.Bottom:
				offset = Vector3.down;
				current = m_info.Heigth;
				break;
			case CubeFaceType.Front:
				offset = Vector3.forward;
				current = m_info.Depth;
				break;
			case CubeFaceType.Back:
				offset = Vector3.back;
				current = m_info.Depth;
				break;
			case CubeFaceType.Right:
				offset = Vector3.right;
				current = m_info.Width;
				break;
			case CubeFaceType.Left:
				offset = Vector3.left;
				current = m_info.Width;
				break;
		}

		if(current >= GameConst.CubeMax)
			return false;

		CubeModelData data = new CubeModelData();
		data.Pos = target.ModelData.Pos + offset;
		m_info.Cubes.Add(data);

		Cube cube = GameObject.Instantiate<Cube>(prefab);
		cube.transform.parent = transform;
		cube.transform.localPosition = data.Pos;
		cube.ModelData = data;
		m_cubes.Add(cube);

		UpdateWHD(cube);
		return true;
	}

	//删除方块
	public void DeleteCube(Cube target)
	{
		if(!m_editable)
			return;
		
		if(m_cubes.Contains(target))
		{
			m_cubes.Remove(target);
			UpdateWHD(target);
			target.Break();
		}
	}

	//更新长宽高
	private void UpdateWHD(Cube cube)
	{
		//标记原点或者顶点
		if(cube.ModelData.Pos.x < m_origin.x)
			m_origin.x = cube.ModelData.Pos.x;
		else if(cube.ModelData.Pos.x > m_peak.x)
			m_peak.x = cube.ModelData.Pos.x;
		
		if(cube.ModelData.Pos.y < m_origin.y)
			m_origin.y = cube.ModelData.Pos.y;
		else if(cube.ModelData.Pos.y > m_peak.y)
			m_peak.y = cube.ModelData.Pos.y;;
		
		if(cube.ModelData.Pos.z < m_origin.z)
			m_origin.z = cube.ModelData.Pos.z;
		else if(cube.ModelData.Pos.z > m_peak.z)
			m_peak.z = cube.ModelData.Pos.z;
		
		Vector3 temp = m_peak - m_origin;
		m_info.Width = (int)temp.x + 1;
		m_info.Heigth = (int)temp.y + 1;
		m_info.Depth = (int)temp.z + 1;

		Debug.Log("origin:" + m_origin.ToString() + "peak:" + m_peak.ToString());
	}
}
