using UnityEngine;
using System.Collections;

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
	public Vector3 Pos;
	public CueInfo XCue;
	public CueInfo YCue;
	public CueInfo ZCue;

}

///提示信息
[SerializeField]
public class CueInfo
{
	public CueNumType Type;
	public int Num;
}

///方块模型数据
[SerializeField]
public class CubeModelData
{
	public int ID;
	public int Index;
	public Vector3 Pos;
	public Color Color;
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
	public CubeModelData[] Cubes;
	public Vector3 Angle;
}

[SerializeField]
public class PuzzleInfo
{
	public int ID;
	public int ModelID;
	public int Level;
	public CubeCueData[] Cubes;
}