using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ContentSavePackage
{
	private List<ContentSaveModel> _contentsave;
	private string _persistentName;
	private string _sceneCode;
	private string _instanceId;
	private string _lastUpdateTimestamp;
	private string _createTimestamp;
	private string _createdBy;
	private string _lastUpdateBy;

	public List<ContentSaveModel> ContentSave {
		get { 
			return _contentsave;
		}
		set { 
			_contentsave = value;
		}
	}

	public string PersistentName {
		get { 
			return _persistentName;
		}
		set { 
			_persistentName = value;
		}
	}

	public string SceneCode {
		get { 
			return _sceneCode;
		}
		set { 
			_sceneCode = value;
		}
	}

	public string InstanceID {
		get { 
			return _instanceId;
		}
		set { 
			_instanceId = value;
		}
	}

	public string LastUpdateTimestamp {
		get {
			return _lastUpdateTimestamp;	
		}
		set { 
			_lastUpdateTimestamp = value;
		}
	}

	public string CreateTimestamp {
		get { 
			return _createTimestamp;
		}
		set { 
			_createTimestamp = value;
		}
	}

	public string CreatedBy {
		get { 
			return _createdBy;
		}
		set { 
			_createdBy = value;
		}
	}

	public string LastUpdateBy {
		get { 
			return _lastUpdateBy;
		}
		set { 
			_lastUpdateBy = value;
		}
	}
}
