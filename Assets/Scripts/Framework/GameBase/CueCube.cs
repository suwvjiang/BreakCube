using UnityEngine;
using System.Collections;

/// <summary>
/// 谜题提示用的方块
/// design by:jiangchufei@gmail.com
/// date: 2017-5-14
/// </summary>
/// 

public class CueCube : Cube 
{
    private CubeCueData m_cueData;

    private bool m_marked;

    public CubeCueData CueData
    {
        get
        {
            return m_cueData;
        }
        set
        {
            m_cueData = value;
            UpdateView();
        }
    }

	// Update is called once per frame
	void Update () 
	{
	
	}

    protected override void UpdateView()
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

        base.UpdateView();
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

    public override bool Break()
    {
        if(m_cueData == null)
            return false;

        if(m_cueData.Value == 0)
        {
            return base.Break();
        }
        else
        {
            WrongBreak();
            return false;
        }
    }

    //方块被误击
    protected virtual void WrongBreak()
    {
        Debug.Log("you have mistake");
    }

}
