using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ContentSaveModel
{
	private string _objInstanceId;
	private string _objType;
	private string _objCode;
	private string _nodeCount;
	private string _positionData;
	private string _rotationData;
	private string _scaleData;
	private string _color;
	private string _width;
	private string _extData;

	public string ObjInstanceId {
		get { 
			return _objInstanceId;
		}
		set { 
			_objInstanceId = value;
		}
	}

	public string ObjType {
		get { 
			return _objType;
		}
		set { 
			_objType = value;
		}
	}

	public string ObjCode {
		get { 
			return _objCode;
		}
		set { 
			_objCode = value;
		}
	}

	public string NodeCount {
		get { 
			return _nodeCount;
		}
		set { 
			_nodeCount = value;
		}
	}

	public string PositionData {
		get { 
			return _positionData;
		}
		set { 
			_positionData = value;
		}
	}

	public string RotationData {
		get { 
			return _rotationData;
		}
		set { 
			_rotationData = value;
		}
	}

	public string ScaleData {
		get { 
			return _scaleData;
		}
		set { 
			_scaleData = value;
		}
	}

	public string Color {
		get { 
			return _color;
		}
		set { 
			_color = value;
		}
	}

	public string Width {
		get { 
			return _width;
		}
		set { 
			_width = value;
		}
	}

	public string ExtData {
		get { 
			return _extData;
		}
		set { 
			_extData = value;
		}
	}
}

public static class OBJContent
{
	public const string objCode = "DEFAULT";
	public const string objType = "BRUSH01";
}
