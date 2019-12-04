using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System.IO;
using System.Text;
using LitJson;
using System.Linq;
using UnityEngine.UI;
namespace JimmyGao
{
    public class BrushManager : MonoBehaviour
    {

        //public SteamVR_TrackedObject TrackedObj;
		public static BrushManager Instance;
        public GameObject BrushObj;
		public GameObject BrushMenu;
        //public Hand hand2;
        public GameObject InitLineObj;
        public List<Color> BrushColor;

        public static LineRenderer currLine;
        public static Vector3 lastPos;
        public static int segNum = 0;
        // Use this for initialization
        //0 idle  1 draw
        public static  int CurrentState;
        public static int colorIdx;
		public  List<GameObject> GoList = new List<GameObject>();
		string txtname = "brushsavejson.txt";
        public Transform GetTransform;
        public Image img;
        public void Awake()
		{
			Instance = this;
		}

        void Start()
        {
            for (int i = 0; i < GetTransform.childCount; i++)
            {
                BrushColor.Add(GetTransform.GetChild(i).GetComponent<Image>().color);
            }
           
            CurrentState = 0;
            colorIdx = 0;
            SetColor(BrushColor[colorIdx]);
			//ShowBrushMenu ();
        }


        // Update is called once per frame
        //void Update()
        //{

        //    SteamVR_Controller.Device device = BrushHand.controller;
        //    SteamVR_Controller.Device switcherDevice = SwitcherHand.controller;//左手
        //    if (device == null || switcherDevice == null)
        //        return;

        //    //Brush input
        //    VRLaserRay laserRay = LaserBeam.GetComponent<VRLaserRay>();

        //    if (laserRay != null && laserRay.PointAt)
        //    {
        //        if (BrushHand.GetStandardInteractionButtonDown())
        //        {
        //            VRExInputModule.CustomControllerButtonDown = true;
        //        }
        //        else if (BrushHand.GetStandardInteractionButtonUp())
        //        {
        //            VRExInputModule.CustomControllerButtonDown = false;
        //        }
        //    }
        //    else
        //    {
        //        if (BrushHand.GetStandardInteractionButtonDown())//右手扳机按下
        //        {

        //            /*
        //            GameObject line = new GameObject ();

        //            currLine = line.AddComponent<LineRenderer> ();

        //            currLine.startWidth = 0.01f;
        //            currLine.endWidth = 0.01f;
        //            */

        //            GameObject line = GameObject.Instantiate(InitLineObj);
        //            currLine = line.GetComponent<LineRenderer>();
        //            segNum = 1;
        //            currLine.positionCount = segNum;
        //            currLine.SetPosition(segNum - 1, BrushObj.transform.position);
        //            lastPos = BrushObj.transform.position;
        //            CurrentState = 1;
        //            GoList.Add(line);
        //        }
        //        else if (BrushHand.controller.GetTouch(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))//右手扳机长按
        //        {


        //            Vector3 currentPos = BrushObj.transform.position;

        //            if (Vector3.Distance(lastPos, currentPos) > 0.02)
        //            {
        //                segNum++;
        //                currLine.positionCount = segNum;
        //                currLine.SetPosition(segNum - 1, BrushObj.transform.position);
        //                lastPos = currentPos;
        //            }


        //        }
        //        if (BrushHand.GetStandardInteractionButtonUp())//右手扳机抬起
        //        {
        //            print("button up");
        //            CurrentState = 0;
        //        }


        //    }
        //    //Switch Input 左手 触摸板按钮 1:上  2:下  3:左  4:右
        //    if (switcherDevice.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && CurrentState == 0)
        //    {
        //        Vector2 touchposVec = switcherDevice.GetAxis();
        //        //print (touchposVec);
        //        float touchangle = JimmyUtility.VectorAngle(new Vector2(1, 0), touchposVec);
        //        //print (touchangle);
        //        int touchPos = JimmyUtility.AngleToPosition(touchangle);
        //        print("touched:" + touchPos);
        //        if (touchPos == 3)
        //        {
        //            colorIdx--;
        //            if (colorIdx < 0)
        //                colorIdx = BrushColor.Count - 1;
        //        }
        //        else if (touchPos == 4)
        //        {
        //            colorIdx++;
        //            if (colorIdx > BrushColor.Count - 1)
        //                colorIdx = 0;
        //        }
        //        if (touchPos == 3 || touchPos == 4)
        //        {
        //            SetColor(BrushColor[colorIdx]);
        //        }

        //        if (touchPos == 1)
        //        {
        //            BrushObj.transform.localScale *= 2;
        //            LineRenderer lr = InitLineObj.GetComponent<LineRenderer>();
        //            lr.widthMultiplier *= 2;
        //        }
        //        if (touchPos == 2)
        //        {
        //            BrushObj.transform.localScale /= 2;
        //            LineRenderer lr = InitLineObj.GetComponent<LineRenderer>();
        //            lr.widthMultiplier /= 2;
        //        }

        //    }

        //    //菜单键
        //    if (switcherDevice.GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu) && CurrentState == 0)
        //    {

        //        if (GoList.Count > 0)
        //        {
        //            GameObject.Destroy(GoList[GoList.Count - 1]);
        //            GoList.Remove(GoList[GoList.Count - 1]);
        //        }

        //    }
        //    //侧边键
        //    if (switcherDevice.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Dashboard_Back) && CurrentState == 0)
        //    {
        //        print("侧边键");
        //    }
        //}

       

		public void SetColor(Color brushColor)
        {
            LineRenderer lr = InitLineObj.GetComponent<LineRenderer>();
            lr.startColor = lr.endColor = brushColor;
            BrushObj.GetComponent<MeshRenderer>().material.color = brushColor;
            img.color = brushColor;
        }

		/// <summary>
		/// 减小笔刷的size
		/// </summary>
		public void MinusBrushSize(int size)
		{
			BrushObj.transform.localScale /= 1.5f;
			LineRenderer lr = InitLineObj.GetComponent<LineRenderer>();
			lr.widthMultiplier /= 1.5f;
			if (size == 1) {
				BrushObj.transform.localScale = new Vector3 (0.008f, 0.008f, 0.008f);
				lr.widthMultiplier = 0.005f;
			}
		}

		/// <summary>
		/// 增加笔刷的size
		/// </summary>
		public void PlusBrushSize(int size)
		{
			BrushObj.transform.localScale *= 1.5f;
			LineRenderer lr = InitLineObj.GetComponent<LineRenderer>();
			lr.widthMultiplier *= 1.5f;
		}

		/// <summary>
		/// 颜色索引递增
		/// </summary>
		public void PlusColorIdx()
		{
			colorIdx++;
			if (colorIdx > BrushColor.Count - 1)
				colorIdx = 0;
			SetColor (BrushColor [colorIdx]);
		}

		/// <summary>
		/// 颜色索引递减
		/// </summary>
		public void MinusColorIdx()
		{
			colorIdx--;
			if (colorIdx < 0)
				colorIdx = BrushColor.Count - 1;
			SetColor (BrushColor [colorIdx]);
		}

		/// <summary>
		/// 菜单显示
		/// </summary>
		public void ShowBrushMenu()
		{
			SetMenuPosition (BrushMenu);
			BrushMenu.SetActive (true);
		}

		public void SetMenuPosition(GameObject menu)
		{
//			Vector3 menupos = SwitcherHand.transform.position;
//			menu.transform.position = menupos;
//			Quaternion rot = Quaternion.Euler (new Vector3 (25f, SwitcherHand.transform.rotation.eulerAngles.y, 0));
//			menu.transform.rotation = rot;
			//GameObject game = menu.transform.parent.gameObject;
			//game.transform.parent = SwitcherHand.transform;
			//game.transform.localRotation = Quaternion.Euler(new Vector3 (55f, SwitcherHand.transform.rotation.eulerAngles.y, 0));
			//game.transform.localPosition = new Vector3 (0.2f, 0, 0);
//			menu.transform.localPosition = new Vector3 (0.2f, 0, 0);
		}

		/// <summary>
		/// 写入存档文件
		/// </summary>
		/// <param name="json">Json.</param>
		private void WriteTxt(string jsontxt)
		{
			string datapath = GetDataPath () + "/DataJson/";
			string path = datapath + txtname;
			if (!File.Exists(datapath)) {
				Directory.CreateDirectory (datapath);
			}
			StreamWriter sw = new StreamWriter(path, false, Encoding.Default);
			//开始写入
			sw.Write(jsontxt);
			//清空缓冲区
			sw.Flush();
			//关闭流
			sw.Close();
		}

		/// <summary>
		/// 读取存档文件
		/// </summary>
		private string ReadTxt()
		{
			string datapath = GetDataPath () + "/DataJson/";
			string path = datapath + txtname;
			if (File.Exists (path)) {
				//读取文件内容
				StreamReader sr = new StreamReader (path);
				string str_read = sr.ReadToEnd ();
				sr.Close ();
				return str_read;	
			} else {
				return "";
			}
		}

		/// <summary>
		/// 获得存档路径
		/// </summary>
		/// <returns>The data path.</returns>
		private string GetDataPath()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				// iPhone路径
				string path = Application.dataPath.Substring (0, Application.dataPath.Length - 5);
				path = path.Substring (0, path.LastIndexOf ('/'));
				return path + "/Documents";
			} else if (Application.platform == RuntimePlatform.Android) {
				// 安卓路径
				return Application.persistentDataPath + "/";
			} else {
				// 其它路径
				return Application.streamingAssetsPath;
			}
		}

		/// <summary>
		/// 保存Brush
		/// </summary>
		public void SaveBrushContent ()
		{
			List<ContentSaveModel> contents = new List<ContentSaveModel> ();
			ContentSavePackage package = new ContentSavePackage ();
			string objposition = "";
			string objlineposition = "";
			#region 获取物品信息
			foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))) {
				if (obj.GetComponent<LineRenderer> () != null) {
					ContentSaveModel content = new ContentSaveModel ();
					LineRenderer objline = obj.GetComponent<LineRenderer> ();
					content.ObjInstanceId = obj.GetInstanceID ().ToString ();
					content.ObjType = OBJContent.objType;
					content.ObjCode = OBJContent.objCode;
					content.NodeCount = objline.positionCount.ToString ();
					// 位置，大小，旋转信息
					objposition = float.Parse (obj.transform.position.x.ToString ("F5")) + ";" + float.Parse (obj.transform.position.y.ToString ("F5")) + ";" + float.Parse (obj.transform.position.z.ToString ("F5"));
					for (int i = 0; i < objline.positionCount; i++) {
						objlineposition += (objline.GetPosition (i).x.ToString ("E") + ";"+objline.GetPosition (i).y.ToString ("E") + ";"+objline.GetPosition (i).z.ToString ("E") + ";");
					}
					content.PositionData = objposition + ";" + objlineposition;
					content.RotationData = float.Parse (obj.transform.rotation.eulerAngles.x.ToString ("F5")) + ";" + float.Parse (obj.transform.rotation.eulerAngles.y.ToString ("F5")) + ";" + float.Parse (obj.transform.rotation.eulerAngles.z.ToString ("F5"));
					content.ScaleData = float.Parse (obj.transform.localScale.x.ToString ("F5")) + ";" + float.Parse (obj.transform.localScale.y.ToString ("F5")) + ";" + float.Parse (obj.transform.localScale.z.ToString ("F5"));
					content.Color = objline.endColor.ToString ();
					content.Width = objline.endWidth.ToString ();
					contents.Add (content);
					objlineposition = "";
				}
			}
			#endregion
			package.InstanceID = System.Guid.NewGuid ().ToString ();
			package.CreateTimestamp = System.DateTime.Now.ToString ("yyyyMMddHHmmss");
			package.LastUpdateTimestamp = System.DateTime.Now.ToString ("yyyyMMddHHmmss");
			contents.RemoveAt (contents.Count - 1); // 移除最后一个LineInit,也就是初始LineInit
			package.ContentSave = contents;
			//将对象序列化为json字符串
			string json_txt = JsonMapper.ToJson (package);
			WriteTxt (json_txt);
		}

		/// <summary>
		/// 读取Brush
		/// </summary>
		public void LoadBrushContent ()
		{
			string json_txt = ReadTxt ();
			// 反序列化成对象
			ContentSavePackage package = JsonMapper.ToObject<ContentSavePackage> (json_txt);
			List<ContentSaveModel> contents = package.ContentSave;
			if (contents.Count > 0) {
				foreach (ContentSaveModel item in contents) {
					GameObject go = GameObject.Instantiate(InitLineObj);
					InitialBrush (go, item);
					GoList.Add (go);
				}
			}
		}

		private void InitialBrush (GameObject obj, ContentSaveModel model)
		{
			string colorstr = model.Color.Substring (5, model.Color.Length - 6);
			List<float> poslist = SplitStringAndFormatFloat (model.PositionData.Replace("(","").Replace(")","").Replace(",",";").TrimEnd(';'), false);
			List<float> rotlist = SplitStringAndFormatFloat (model.RotationData, false);
			List<float> scalist = SplitStringAndFormatFloat (model.ScaleData, false);
			List<float> colorlist = SplitStringAndFormatFloat (colorstr, true);
			Vector3 pos = new Vector3 (poslist [0], poslist [1], poslist [2]);
			Quaternion rot = Quaternion.Euler (rotlist [0], rotlist [1], rotlist [2]);
			Vector3 sca = new Vector3 (scalist [0], scalist [1], scalist [2]);
			Color c = new Color (colorlist [0], colorlist [1], colorlist [2], colorlist [3]);
			obj.transform.position = pos;
			obj.transform.rotation = rot;
			obj.transform.localScale = sca;
			obj.GetComponent<LineRenderer> ().positionCount = int.Parse (model.NodeCount);
			Vector3 linpos;
			List<Vector3> linposs = new List<Vector3> ();
			for (int i = 1; i < poslist.Count / 3; i++) {
				linpos = new Vector3 (poslist [3 * i], poslist [3 * i + 1], poslist [3 * i + 2]);
				linposs.Add (linpos);
			}
			obj.GetComponent<LineRenderer> ().SetPositions (linposs.ToArray ());
			obj.GetComponent<LineRenderer> ().startColor = obj.GetComponent<LineRenderer> ().endColor = c;
			obj.GetComponent<LineRenderer> ().startWidth = obj.GetComponent<LineRenderer> ().endWidth = float.Parse (model.Width);
		}

		/// <summary>
		/// 分割字符串并将其转化为float类型
		/// </summary>
		/// <returns>The string and format float.</returns>
		/// <param name="str">String.</param>
		/// <param name="flag">If set to <c>true</c> flag.</param>
		private List<float> SplitStringAndFormatFloat (string str, bool flag)
		{
			List<float> strfloat = new List<float> ();
			string[] strArr = new string[]{ };
			if (flag == false) {
				strArr = str.Split (';');
			} else if (flag == true) {
				strArr = str.Split (',');
			}
			foreach (var item in strArr) {
				strfloat.Add (float.Parse(item));
			}
			return strfloat;
		}
    }
}