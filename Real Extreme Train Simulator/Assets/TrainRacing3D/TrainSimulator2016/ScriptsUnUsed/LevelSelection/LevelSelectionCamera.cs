using UnityEngine;
using System.Collections;

public class LevelSelectionCamera : MonoBehaviour
{

		public bool isLerp;
		bool isMovementAvaliable	= true;

		public Vector3 newpos;
		Vector3 transformPos;

		public float positionDamping = 2;
		public float newFieldOfView	= 4;
		public float fieldOfViewDamping	= 2f;
		Camera _camera;

		// Use this for initialization
		void Start ()
		{
				newpos	= transform.position;
				_camera	= GetComponent<Camera> ();
				FadeFromLoading ();
		}
	
		// Update is called once per frame
		void LateUpdate ()
		{
//		if(isLerp)
				{
						transform.position	= Vector3.Lerp (transform.position, newpos, Time.deltaTime * positionDamping);
//			_camera.fieldOfView	= Mathf.Lerp(_camera.fieldOfView,newFieldOfView,Time.deltaTime*fieldOfViewDamping);
						_camera.orthographicSize	= Mathf.Lerp (_camera.orthographicSize, newFieldOfView, Time.deltaTime * fieldOfViewDamping);
				}
		}

		public void ChangeCameraToSelected (float xPos, float yPos)
		{
				isMovementAvaliable	= false;
				transformPos	= transform.position;
				newFieldOfView	= 4;
				newpos.x	= xPos + 3f;
				newpos.y	= yPos;
		}

		public void ChangeCameraToNormalMode ()
		{
				newFieldOfView	= 5;
				newpos = transformPos;
				Invoke ("enableMovementAfterDelay", positionDamping);
		}

		void enableMovementAfterDelay ()
		{
				isMovementAvaliable	= true;
		}

		#region Scorlling

		// Update is called once per frame
		[SerializeField] GameObject dragObj;
		public float RightLimit;
		public float LeftLimit;
		private float Pos1, Pos2;
		private bool ishold;
		public float diff;
	
	
		float ydiff, initx, offset, Maxlimit, Minlimit;
		public bool onpress, isMoving;
		// Update is called once per frame
		void Update ()
		{

				if (LevelSelectionHandler.Instance.mPopUpState != e_POPUP_STATE.Levels) {
						offset = 0f;
						return;
				}

				if (Input.GetMouseButtonDown (0) && Input.mousePosition.y < (Screen.height - Screen.height / 10) && Input.mousePosition.y > (Screen.height / 10)) {
						initx = Input.mousePosition.x;
						onpress = true;
				}

				if (Input.GetMouseButtonUp (0)) {
						onpress = false;
				}
		
				if (onpress) {
						ydiff = initx - Input.mousePosition.x;
						initx = Input.mousePosition.x;
						offset = ydiff * 0.0053f;
						if (ydiff < 0.1f && ydiff > -0.1f) {
								isMoving = false;
						} else {
								isMoving = true;
						}
				} else {
						offset = offset * 0.9f;
				}
				float ypos	= Mathf.Lerp (dragObj.transform.position.y, 1f, Time.deltaTime * 5);

				dragObj.transform.position = new Vector3 (dragObj.transform.position.x + (offset * 2f), ypos, dragObj.transform.position.z); //Rotate (new Vector3(-offset,0,0));
				newpos	= dragObj.transform.position;
				if (dragObj.transform.position.x >= RightLimit) {
						dragObj.transform.position = Vector3.Lerp (dragObj.transform.position, new Vector3 (RightLimit, dragObj.transform.position.y, dragObj.transform.position.z), 10 * Time.deltaTime);
				}
				if (dragObj.transform.position.x <= LeftLimit) {
						dragObj.transform.position = Vector3.Lerp (dragObj.transform.position, new Vector3 (LeftLimit, dragObj.transform.position.y, dragObj.transform.position.z), 10 * Time.deltaTime);
				}
		}

		#endregion

		[SerializeField] ButtonAnims _FadeAnimation;

		public void FadeFromLoading ()
		{
				_FadeAnimation.CallAllAnims ();
		}

		public void FadeToLoading ()
		{
				_FadeAnimation.ReverseAll ();
		}
}
