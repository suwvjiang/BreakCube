using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 游戏数据结构集合
/// design by:jiangchufei@gmail.com
/// date: 2017-5-13
/// </summary>
/// 

///方块提示信息
[SerializeField]
public class CubeCueData 
{
	public int Value;
	public Vector3 Pos = Vector3.zero;
	public FaceCueInfo XCue = new FaceCueInfo();
	public FaceCueInfo YCue = new FaceCueInfo();
	public FaceCueInfo ZCue = new FaceCueInfo();

}

///提示信息
[SerializeField]
public class FaceCueInfo
{
	public CueNumType Type = CueNumType.None;
	public int Num;
}

///方块模型数据
[SerializeField]
public class CubeModelData
{
	public int ID;
	public int Index;
	public Vector3 Pos = Vector3.zero;
	public Color Color = Color.white;
	public int FrontTextureID = -1;
	public int BackTextureID = -1;
	public int LeftTextureID = -1;
	public int RightTextureID = -1;
	public int TopTextureID = -1;
	public int BottomTextureID = -1;
}

///模型数据
[SerializeField]
public class ModelInfo
{
	public int ID;
	public int Bgm;
	public int BgImg;
	public int Width;
	public int Heigth;
	public int Depth;
	public List<CubeModelData> Cubes = new List<CubeModelData>();
	public Vector3 Angle = Vector3.zero;
}

[SerializeField]
public class PuzzleInfo
{
	public int ID;
	public int ModelID;
	public int Level;
	public int BestCost;
	public int BetterCost;
	public int GoodCost;
	public int Tolerant;
	public List<CubeCueData> Cubes = new List<CubeCueData>();
}