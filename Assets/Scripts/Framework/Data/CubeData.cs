using UnityEngine;
using System.Collections;

/// <summary>
/// 方块信息
/// design by:jiangchufei@gmail.com
/// date: 2017-5-13
/// </summary>
/// 

public class CubeData 
{
	public int Value;
	public Vector3 Pos;
	public CueInfo XCue;
	public CueInfo YCue;
	public CueInfo ZCue;

}

///提示数字类型
public enum CueNumType
{
	None,
	NoBreak,
	OneBreak,
	MultBreak,
}

///提示信息
public class CueInfo
{
	public CueNumType Type;
	public int Num;
}

public enum CubeFaceType
{
    Top,
    Bottom,
    Left,
    Right,
    Front,
    Back
};
