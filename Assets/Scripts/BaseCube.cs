using UnityEngine;
using System.Collections;

/// <summary>
/// 方块基类
/// design by: 江楚飞
/// date: 2017-5-12
/// </summary>
/// 

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public class BaseCube : MonoBehaviour 
{
    private Mesh m_mesh;

    void Awake()
    {
        m_mesh = GetComponent<MeshFilter>().sharedMesh;
        Vector2[] uvs = SixTexForCube(m_mesh.vertices);
        m_mesh.uv = uvs;
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    Vector2[] SixTexForCube(Vector3[] verticles)  
    {
        Vector2[] uv = new Vector2[verticles.Length];  

        float t = 1 / 3f;  

        //front  
        uv[0] = new Vector2(0, 0); 
        uv[1] = new Vector2(t, 0);
        uv[2] = new Vector2(0, t); 
        uv[3] = new Vector2(t, t);  

        //back  
        uv[4] = new Vector2(t, t);  
        uv[5] = new Vector2(0, t); 
        uv[6] = new Vector2(t, 0);
        uv[7] = new Vector2(0, 0); 

        //top  
        uv[8] = new Vector2(0, 0);  
        uv[9] = new Vector2(t, 0);  
        uv[10] = new Vector2(0, t);  
        uv[11] = new Vector2(t, t);  

        //Bottom  
        uv[12] = new Vector2(t, t);  
        uv[13] = new Vector2(t, 2 * t);  
        uv[14] = new Vector2(0, 2 * t);  
        uv[15] = new Vector2(0, t);  

        //left  
        uv[16] = new Vector2(2 * t, 0);  
        uv[17] = new Vector2(2 * t, t);  
        uv[18] = new Vector2(t, t);  
        uv[19] = new Vector2(t, 0);  

        //right  
        uv[20] = new Vector2(1, t);  
        uv[21] = new Vector2(1, 2 * t);  
        uv[22] = new Vector2(2 * t, 2 * t);  
        uv[23] = new Vector2(2 * t, t); 
        return uv;  
    }  
}
