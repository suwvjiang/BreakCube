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
    private CubeCueData m_cueData;
    private CubeModelData m_modelData;
    private bool m_marked;
    private Color m_color;
    

    public CubeCueData CueData
    {
        get
        {
            return m_cueData;
        }
        set
        {
            m_cueData = value;
            UpdataCueView();
        }
    }

    public CubeModelData ModelData
    {
        get
        {
            return m_modelData;
        }
        set
        {
            m_modelData = value;
            UpdateModelView();
        }
    }

    // Use this for initialization
    void Start () 
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

    ///显示提示数据信息
    public void UpdataCueView()
    {
        FrontPoint.x = m_cueData.ZCue.Num;
        FrontPoint.y = (int)m_cueData.ZCue.Type;
        BackPoint.x = m_cueData.ZCue.Num;
        BackPoint.y = (int)m_cueData.ZCue.Type;

        TopPoint.x = m_cueData.YCue.Num;
        TopPoint.y = (int)m_cueData.YCue.Type;
        BottomPoint.x = m_cueData.YCue.Num;
        BottomPoint.y = (int)m_cueData.YCue.Type;

        LeftPoint.x = m_cueData.XCue.Num;
        LeftPoint.y = (int)m_cueData.XCue.Type;
        RightPoint.x = m_cueData.XCue.Num;
        RightPoint.y = (int)m_cueData.XCue.Type;

        UpdateMeshUVS();
    }

    ///显示模型数据信息
    public void UpdateModelView()
    {
        MarkColor(m_modelData.Color);
    }

    public void Mark()
    {
        m_marked = !m_marked;
        if(m_marked)
        {
            MarkColor(Color.blue);
        }
        else
        {
            MarkColor(Color.white);
        }
    }

    ///标记颜色
    public void MarkColor(Color color)
    {
        if(m_color == color)
            return;

        m_color = color;
        UpdateColor();
    }

    public bool Break()
    {
        if(m_cueData == null)
            return false;

        if(m_cueData.Value == 0)
        {
            BeBreak();
            return true;
        }
        else
        {
            WrongBreak();
            return false;
        }
    }

    protected void UpdateMeshUVS()
    {
        Vector2[] uvs = m_mesh.uv;
        // Front
        SetFaceTexture(CubeFaceType.Front, uvs);
        // Top
        SetFaceTexture(CubeFaceType.Top, uvs);
        // Back
        SetFaceTexture(CubeFaceType.Back, uvs);
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
            uvs[15] = newUVS[1]; 
            uvs[12]  = newUVS[0]; 
            uvs[13]  = newUVS[2]; 
            uvs[14] = newUVS[3]; 
        }
		else if (faceType == CubeFaceType.Left) 
		{
            Vector2[] newUVS = GetUVS(LeftPoint.x, LeftPoint.y);
            uvs[19]  = newUVS[1]; 
            uvs[16] = newUVS[0]; 
            uvs[17]  = newUVS[2]; 
            uvs[18] = newUVS[3]; 
        }
		else if (faceType == CubeFaceType.Right) 
		{
            Vector2[] newUVS = GetUVS(RightPoint.x, RightPoint.y);
            uvs[23]  = newUVS[1]; 
            uvs[20] = newUVS[0]; 
            uvs[21]  = newUVS[2]; 
            uvs[22] = newUVS[3]; 
        }
    }

    ///更新颜色
    protected void UpdateColor()
    {
        Debug.Log("you change this cube's color");
    }

    ///方块被击碎
    protected virtual void BeBreak()
    {
        GameObject.Destroy(this);
    }

    ///方块被误击
    protected virtual void WrongBreak()
    {
        Debug.Log("you have mistake");
    }
}
