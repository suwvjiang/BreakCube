using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏入口
/// design by:jiangchufei@gmail.com
/// date: 2017-5-13
/// </summary>
///

public class GameEnter : MonoBehaviour 
{
	public Transform Container;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		GUI.Box(new Rect(10,10,100,90), "Total Menu");

		// Play Button
		if (GUI.Button(new Rect (20,40,80,20), "Play"))
		{
			
		}

		// Editor Button
		if (GUI.Button (new Rect (20,70,80,20), "Editor")) 
		{
			StartEditor();
		}
	}

	private void StartEditor()
	{
		ModelInfo info = new ModelInfo();
		info.ID = 1;

		CubeModelData cube = new CubeModelData();
		cube.ID = 0;

		info.Cubes.Add(cube);

		CubeModel model =  CubeModel.Create(info);
		if(model != null)
		{
			model.transform.parent = Container;
			model.Editable = true;
			
			InputMgr.Instance.Status = GameStatus.Editor;
			InputMgr.Instance.Target = model;
		}
	}
}
