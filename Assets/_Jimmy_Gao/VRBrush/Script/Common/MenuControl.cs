using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JimmyGao;
using UnityEditor;

public class MenuControl : MonoBehaviour
{

	public Text sizetext; // 笔刷挡位显示
	int size; // 笔刷增大或缩小的挡位(共有6个挡位，默认为1)
	List<GameObject> golist = new List<GameObject> ();

	// Use this for initialization
	void Start ()
	{
		size = 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	/// <summary>
	/// 后退一步操作
	/// </summary>
	public void BtnUndoClicked ()
	{
		golist = BrushManager.Instance.GoList;
		if (golist.Count > 0) {
			GameObject.Destroy (golist [golist.Count - 1]);
			golist.Remove (golist [golist.Count - 1]);
		}
	}

	/// <summary>
	/// 清空
	/// </summary>
	public void BtnNewClicked ()
	{
		golist = BrushManager.Instance.GoList;
		if (golist.Count > 0) {
			foreach (GameObject item in golist) {
				GameObject.Destroy (item);
			}
			golist.Clear ();
		}
	}

	/// <summary>
	/// 加载
	/// </summary>
	public void BtnLoadClicked ()
	{
		print ("BtnLoadClicked!!!");
		#region 加载前先清空
		golist = BrushManager.Instance.GoList;
		if (golist.Count > 0) {
			foreach (GameObject item in golist) {
				GameObject.Destroy (item);
			}
			golist.Clear ();
		}
		#endregion

		BrushManager.Instance.LoadBrushContent ();
	}

	/// <summary>
	/// 保存（json格式保存）
	/// </summary>
	public void BtnSaveClicked ()
	{
		print ("BtnSaveClicked!!!");
		BrushManager.Instance.SaveBrushContent ();
	}

	/// <summary>
	/// 更改画笔颜色
	/// </summary>
	/// <param name="sender">Sender.</param>
	public void BtnChangeColorCliked (GameObject sender)
	{
		// print (sender.GetComponent<Image> ().color);
		BrushManager.Instance.SetColor (sender.GetComponent<Image> ().color);
	}

	/// <summary>
	/// 增大画笔
	/// </summary>
	public void BtnPlusBrushSize ()
	{
		RefreshControl ();
		size++;
		if (size > 6) {
			size = 6;
		} else if (size > 1 && size < 7) {
			BrushManager.Instance.PlusBrushSize (size);
		}
		sizetext.text = size.ToString ();
	}

	/// <summary>
	/// 减小画笔
	/// </summary>
	public void BtnMinusBrushSize ()
	{
		RefreshControl ();
		size--;
		if (size < 1) {
			size = 1;
		}
		sizetext.text = size.ToString ();
		BrushManager.Instance.MinusBrushSize (size);
        print("left");
	}


	void RefreshControl ()
	{
		sizetext = BrushManager.Instance.BrushMenu.transform.GetChild (0).Find ("Content/operation/Text").GetComponent<Text> ();
	}
}
