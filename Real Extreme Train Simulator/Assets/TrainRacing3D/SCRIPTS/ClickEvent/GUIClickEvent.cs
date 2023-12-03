using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GUIClickEvent : MonoBehaviour
{
	 
	Image[] _textures;
	List<Image> _tempTexture;
	TextMesh[] _textGui;
	Image _selectedTexture;
	AudioSource _audioSource;
	bool checkZVal;
	// GUI Fixer Variables
	// For Landscape
	float GUIPageWidth = 1280;
	float GUIPageHeight = 800;
	GameObject _selected;
	// For Portrait
	//	float GUIPageWidth=800;
	//	float GUIPageHeight=1280;
	public bool manualButtonAdd, popupEffect;
	public Font _newFont;
	GameObject _touchedObj;
	public	GameObject _ref;
	public bool _webBuild;

	void OnEnable ()
	{
		#if !UNITY_EDITOR && !UNITY_WEBPLAYER
		StaticVariables.forMobile = true;
		#elif UNITY_WEBPLAYER
		_webBuild	= true;
		#endif


		checkZVal = true;
		if (manualButtonAdd == false)
			_textures = GameObject.FindObjectsOfType<Image> ();
		_selectedTexture = transform.parent.Find ("SelectionEffect").GetComponent<Image> ();
		_audioSource = transform.parent.Find ("AudioEffect").GetComponent<AudioSource> ();
		_textGui = GameObject.FindObjectsOfType<TextMesh> ();
	}

	void Start ()
	{

				 
		SetGUITextures ();
		
		if (_selectedTexture != null)
			_selectedTexture.enabled = false;
		
		SetGuiTextSize ();
		
	}

	void OnDisable ()
	{
		_textures = null;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			//ClickEventManager.CheckGUIClick ("UnityBackButton", null);
			return;
		}

		if (StaticVariables.forMobile) {
			var touches = Input.touches;
			foreach (var touch in touches) {
				if (touch.phase == TouchPhase.Began) {
					SetSelectedTexture (touch.position);
				}
			}
			foreach (var touch in touches) {		
				if (touch.phase == TouchPhase.Ended) {
					if (_selectedTexture.enabled)
						_selectedTexture.enabled = false;
					if (_ref != null)
						_ref.transform.localScale = new Vector3 (0.001f, 0.001f, 1);
					string clickedGUI = CheckClick (touch.position);
					ClickEventManager.CheckGUIClick (clickedGUI, _touchedObj);
				}
			}
		} else {
			if (Input.GetMouseButtonDown (0)) {
				SetSelectedTexture (Input.mousePosition);
			}
			
			if (Input.GetMouseButtonUp (0)) {
			//	if (_selectedTexture.enabled)
				//	_selectedTexture.enabled = false;
				if (_ref != null)
					_ref.transform.localScale = new Vector3 (0.001f, 0.001f, 1);
				
				string clickedGUI = CheckClick (Input.mousePosition);
				ClickEventManager.CheckGUIClick (clickedGUI, _touchedObj);
				
				
			}
		}
	}

	void SetGUITextures ()
	{
		foreach (var item in _textures) {
		//	GuiTextureSizer (item.GetComponent<Image> ());
		}
	}

	public string CheckClick (Vector2 inputPos)
	{
		_tempTexture = null;
		
		foreach (var item in _textures) {
		//	if (item.gameObject.layer != GlobalVariables.iInvisibleLayer && item.GetComponent<Image> ().GetScreenRect ().Contains (inputPos))
		//	if (item.tag != "BG" && item.enabled == true && item.tag != "NotUi" && item.transform.gameObject.activeInHierarchy && item.GetComponent<GUITexture> ().color.a != 0)
			if (item.transform.parent != null) {
				if (item.transform.parent.gameObject.activeInHierarchy) {
				//	AddGuiButtonsToList (item);
				}
			} else {
					
			//	AddGuiButtonsToList (item);
			}
		}
		
		return selectedButton ();
	}

	public void AddGuiButtonsToList (TextMesh _tex)
	{
		//if (_tempTexture == null)
		//	_tempTexture = new List<GUITexture> ();
		//if (_tex.color.a != 0)
		//	_tempTexture.Add (_tex);
		
	}

	string selectedButton ()
	{
		if (_tempTexture != null && _tempTexture.Count > 0) {
			_selected = _tempTexture [0].transform.gameObject;
			
			if (_tempTexture.Count == 1) {
				_tempTexture = null;
				return _selected.name;
			} else {
				for (int i = 0; i < _tempTexture.Count; i++) {
					if (_selected.transform.position.z < _tempTexture [i].transform.position.z)
						_selected = _tempTexture [i].transform.gameObject;
				}
				_tempTexture = null;
				return _selected.name;
			}
			
		} else
			return null;
	}

	void selectedButtonForTexture ()
	{
		if (_tempTexture != null && _tempTexture.Count > 0) {
			_selected = _tempTexture [0].transform.gameObject;
			if (_tempTexture.Count == 1) {
				if (_tempTexture [0].tag != "ButtonWithNoTouchEffect") {//"BOARD" && _tempTexture[0].name != "LOCK")
					if (popupEffect) {
						_tempTexture [0].transform.localScale = new Vector3 (0.2f, 0.2f, 1);
						_ref = _tempTexture [0].gameObject;
					} else {
						_selectedTexture.enabled = true;
					//	_selectedTexture.pixelInset = _tempTexture [0].pixelInset;
						_selectedTexture.transform.parent = _tempTexture [0].transform;
						_selectedTexture.transform.localPosition = new Vector3 (0, 0, 10);
					}
				}
			} else {
				for (int i = 0; i < _tempTexture.Count; i++) {
					if (_selected.transform.position.z < _tempTexture [i].transform.position.z) {
						 
						_selected = _tempTexture [i].transform.gameObject;
					}


				}
				if (_selected.tag != "ButtonWithNoTouchEffect") {//&&_selected.name != "LOCK")
					if (popupEffect) {
						_selected.transform.localScale = new Vector3 (0.2f, 0.2f, 1);
						_ref = _selected;
					} else {
						_selectedTexture.enabled = true;
					//	_selectedTexture.pixelInset = _selected.GetComponent<GUITexture> ().pixelInset;
						_selectedTexture.transform.parent = _selected.transform;
						_selectedTexture.transform.localPosition = new Vector3 (0, 0, 10);
					}
				}
			}
			_tempTexture = null;
		}  
	}

	public void SetSelectedTexture (Vector2 inputPos)
	{
		foreach (var item in _textures) {
			//if (item.gameObject.layer != GlobalVariables.iInvisibleLayer && item.GetComponent<GUITexture> ().GetScreenRect ().Contains (inputPos))
			if (item.tag != "BG" && item.enabled == true && item.tag != "Blood")
			if (item.transform.parent != null) {
				if (item.transform.parent.gameObject.activeInHierarchy) {
					if (item.name != "BOARD" && item.color.a != 0) {
					//	AddGuiButtonsToList (item);
							
						if (_audioSource.isPlaying == false)
							_audioSource.Play ();
					}
				}
			} else {
				if (item.name != "BOARD" && item.color.a != 0) {
				//	AddGuiButtonsToList (item);
					if (item.transform.gameObject.activeInHierarchy)
					if (_audioSource.isPlaying == false)
						_audioSource.Play ();
				}
				
				
			}
		}
		
		selectedButtonForTexture ();
		
	}
	
	
	//	void GuiTextureSizer(GUITexture GUITextureName)
	//	{
	//
	//		if(GUITextureName.guiTexture.texture!=null&&GUITextureName.tag!= "NotUi")
	//		if(_webBuild)
	//		{
	//			if(GUITextureName.texture.width!= 1280&&GUITextureName.texture.width!=1024)
	//				GUITextureName.pixelInset=new Rect (0, 0, Screen.width * (GUITextureName.texture.width/GUIPageWidth),Screen.height *(GUITextureName.texture.height/GUIPageHeight));
	//			else
	//				GUITextureName.pixelInset=new Rect (0, 0, 960,600);
	//		}
	//		else
	//		{
	//			GUITextureName.pixelInset=new Rect (0, 0, Screen.width * (GUITextureName.texture.width/GUIPageWidth),Screen.height *(GUITextureName.texture.height/GUIPageHeight));
	//		}
	//
	//	}
	void GuiTextureSizer (Image GUITextureName)
	{
		float extra1 = 1, extra2 = 1;
		if ((float)Screen.width / (float)Screen.height < 1.5) {
			GUIPageWidth = 1024;
			GUIPageHeight = 768;
			extra1 = 0.8f;
			extra2 = 0.8f;//0.96f;
		} else {
			GUIPageWidth = 1280;
			GUIPageHeight = 800;
		}
	//	if (GUITextureName.GetComponent<Image> ().texture != null && GUITextureName.tag != "NotUi")
		if (_webBuild) {
		//	if (GUITextureName.texture.width != 1280 && GUITextureName.texture.width != 1024)
				//GUITextureName.pixelInset = new Rect (0, 0, (Screen.width * ((GUITextureName.texture.width * extra1) / GUIPageWidth)), (Screen.height * ((GUITextureName.texture.height * extra2) / GUIPageHeight)));
		//	else
			//	GUITextureName.pixelInset = new Rect (0, 0, 960, 600);
		} else {
		//	float width	= (Screen.width * ((GUITextureName.texture.width * extra1) / GUIPageWidth));
		//	float height	= (Screen.height * ((GUITextureName.texture.height * extra2) / GUIPageHeight));

		//	GUITextureName.pixelInset = new Rect (0, 0, width, height);
		}
	}

	void SetGuiTextSize ()
	{
		float aspectRatio	= (float)Screen.width / (float)Screen.height;

		foreach (var item in _textGui) {
			if (item.fontSize == 0)
				continue;
		//	if (aspectRatio < 1.5f)
			//	item.pixelOffset	= new Vector2 (0, -7f);//  -12.8f;//-(GUIPageWidth)/100f;

		//	float val = 1280 / item.fontSize;
		//	item.fontSize = (int)(Screen.width / val);
//			item.fontStyle = FontStyle.Normal;
		}
//		if(_newFont !=null)
//			foreach (var item in _textGui) 
//		{
//			item.font  = _newFont;
//		}
	}
}

