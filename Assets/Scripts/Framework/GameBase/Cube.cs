using UnityEngine;
using System.Collections;

/// <summary>
/// 方块基类
/// design by:jiangchufei@gmail.com
/// date: 2017-5-12
/// </summary>
/// 
[ExecuteInEditMode]
public class Cube : MonoBehaviour 
{
    public Vector2 FrontPoint;
    public Vector2 BackPoint;
    public Vector2 TopPoint;
    public Vector2 BottomPoint;
    public Vector2 LeftPoint;
    public Vector2 RightPoint;

    private Mesh m_mesh;
    private Color m_color;
    

    // Use this for initialization
    protected void Start () 
	{
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null) 
		{
            Debug.LogError("Script needs MeshFilter component");
            return;
        }

        #if UNITY_EDITOR
        // Make a deep copy
        Mesh meshCopy = Mesh.Instantiate(meshFilter.sharedMesh) as Mesh;    
        meshCopy.name = "Cube";
        // Assign the copy to the meshes
        m_mesh = meshFilter.mesh = meshCopy;                                
        #else
        m_mesh = meshFilter.mesh;
        #endif
        if (m_mesh == null || m_mesh.uv.Length != 24) 
		{
            Debug.LogError("Script needs to be attached to built-in cube");
            return;
        }

        UpdateMeshUVS();
    }

    // Update is called once per frame
    void Update () 
    {
#if UNITY_EDITOR
        UpdateMeshUVS();
#endif
    }

    //更新方块显示
    protected virtual void UpdateView()
    {
        UpdateMeshUVS();
    }

    //标记颜色
    public void MarkColor(Color color)
    {
        if(m_color == color)
            return;

        m_color = color;
        UpdateColor();
    }

    public virtual bool Break()
    {
        GameObject.Destroy(this.gameObject);
        return true;
    }

    protected void UpdateMeshUVS()
    {
        Vector2[] uvs = m_mesh.uv;
        // Front
        SetFaceTexture(CubeFaceType.Front, uvs);
        // Back
        SetFaceTexture(CubeFaceType.Back, uvs);
        // Top
        SetFaceTexture(CubeFaceType.Top, uvs);
        // Bottom
        SetFaceTexture(CubeFaceType.Bottom, uvs);
        // Left
        SetFaceTexture(CubeFaceType.Left, uvs);  
        // Right        
        SetFaceTexture(CubeFaceType.Right, uvs);   
        m_mesh.uv = uvs;
    }

    protected virtual Vector2[] GetUVS(float originX, float originY)
    {
        originX %= 10;
        originY %= 2;
        
        Vector2[] uvs = new Vector2[4];
        uvs[0] = new Vector2(originX / 10.0f, originY / 2.0f);
        uvs[1] = new Vector2((originX + 1) / 10.0f, originY / 2.0f);
        uvs[2] = new Vector2(originX / 10.0f, (originY + 1) / 2.0f);
        uvs[3] = new Vector2((originX + 1) / 10.0f, (originY + 1) / 2.0f);
        return uvs;
    }

    protected void SetFaceTexture(CubeFaceType faceType, Vector2[] uvs)
    {
        if (faceType == CubeFaceType.Front) 
		{
            Vector2[] newUVS = GetUVS(FrontPoint.x, FrontPoint.y);
            uvs[0]  = newUVS[0]; 
            uvs[1]  = newUVS[1];
            uvs[2]  = newUVS[2];
            uvs[3]  = newUVS[3];
        }
		else if (faceType == CubeFaceType.Back) 
		{
            Vector2[] newUVS = GetUVS(BackPoint.x, BackPoint.y);
            uvs[7] = newUVS[0]; 
            uvs[6] = newUVS[1]; 
            uvs[11]  = newUVS[2]; 
            uvs[10]  = newUVS[3]; 
        }
		else if (faceType == CubeFaceType.Top) 
		{
            Vector2[] newUVS = GetUVS(TopPoint.x, TopPoint.y);
            uvs[8] = newUVS[0]; 
            uvs[9] = newUVS[1]; 
            uvs[4]  = newUVS[2]; 
            uvs[5]  = newUVS[3]; 
        }
		else if (faceType == CubeFaceType.Bottom) 
		{
            Vector2[] newUVS = GetUVS(BottomPoint.x, BottomPoint.y);
            uvs[12]  = newUVS[0]; 
            uvs[15] = newUVS[1]; 
            uvs[13]  = newUVS[2]; 
            uvs[14] = newUVS[3]; 
        }
		else if (faceType == CubeFaceType.Left) 
		{
            Vector2[] newUVS = GetUVS(LeftPoint.x, LeftPoint.y);
            uvs[16] = newUVS[0]; 
            uvs[19]  = newUVS[1]; 
            uvs[17]  = newUVS[2]; 
            uvs[18] = newUVS[3]; 
        }
		else if (faceType == CubeFaceType.Right) 
		{
            Vector2[] newUVS = GetUVS(RightPoint.x, RightPoint.y);
            uvs[20] = newUVS[0]; 
            uvs[23]  = newUVS[1]; 
            uvs[21]  = newUVS[2]; 
            uvs[22] = newUVS[3]; 
        }
    }

    //更新颜色
    protected void UpdateColor()
    {
        Debug.Log("you change this cube's color");
    }
}
