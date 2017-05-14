using UnityEngine;
using System.Collections;

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

	///创建方块
	private void CreateCubes()
	{
		if(m_info == null)
			return;
		
		Cube prefab = Resources.Load<Cube>("Prefabs/Cube");
		if(prefab == null)
			return;

		for(int i = 0; i < m_info.Cubes.Length; ++i)
		{
			CubeModelData data = m_info.Cubes[i];
			if(data == null)
				continue;
			Cube cube = GameObject.Instantiate<Cube>(prefab);
			cube.transform.parent = transform;
			cube.transform.localPosition = data.Pos;
			cube.ModelData = data;
		}
	}
}
