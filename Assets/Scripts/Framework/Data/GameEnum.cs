using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏枚举集合
/// design by:jiangchufei@gmail.com
/// date: 2017-5-13
/// </summary>
///

///提示数字类型
public enum CueNumType
{
	None,
	NoBreak,
	OneBreak,
	MultBreak,
}

///方块六面
public enum CubeFaceType
{
    Top,
    Bottom,
    Left,
    Right,
    Front,
    Back
}

///当前输入状态
public enum InputState
{
    None,
    Mark,//标记
    Deletion,//删除
    Addition,//添加
}

public enum GameStatus
{
    None,
    Playing,//游戏模式
    Editor,//编辑模型
}