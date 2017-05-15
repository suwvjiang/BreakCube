using UnityEngine;
using System.Collections;

/// <summary>
/// 模型展示用的方块
/// design by:jiangchufei@gmail.com
/// date: 2017-5-14
/// </summary>
/// 

public class ModelCube : Cube 
{
    private CubeModelData m_modelData;

    public CubeModelData ModelData
    {
        get
        {
            return m_modelData;
        }
        set
        {
            m_modelData = value;
            UpdateView();
        }
    }

	// Update is called once per frame
	void Update () 
	{
	
	}

	protected override void UpdateView()
	{

	}

}
