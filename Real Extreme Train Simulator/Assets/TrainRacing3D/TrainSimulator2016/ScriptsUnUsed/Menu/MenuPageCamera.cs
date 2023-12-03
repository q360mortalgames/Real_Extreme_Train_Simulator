using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuPageCamera : MonoBehaviour
{
		float rotationDamping = 1f;
		float DistanceDamping = 4f;

		public Transform _SettingsTransform;
		public Transform _MenuposTransform;
		Transform _posTransform;
		bool isStartMovement	= false;
		public float timer	= 3f;
		public iTween.EaseType _easetype	= iTween.EaseType.linear;
		public MenuTrainMovement _scr;

		void Awake ()
		{
				
		}

		void Start ()
		{
				float[] distance	= new float[32];
				distance [9]	= 200;
				distance [8]	= 200;
				distance [10]	= 200;
				distance [11]	= 200;
				Camera _Camera	= GetComponent<Camera> ();
				_Camera.layerCullDistances	= distance;

				FadeFromLoading ();

				_posTransform	= _MenuposTransform;
				iTween.MoveTo (gameObject, iTween.Hash ("position", _posTransform.position, "time", timer, "easetype", _easetype));
				iTween.RotateTo (gameObject, iTween.Hash ("rotation", _posTransform.eulerAngles, "time", timer, "easetype", _easetype));
				timer = timer + 0.5f;
				Invoke ("OnCameraMoveAnimationFinish", timer);
		}

		public void OnCameraMoveAnimationFinish ()
		{
				isStartMovement	= true;
				_scr.enabled	= true;
		Debug.Log ("setToMenuPage");
				//MenuPageHandler.Instance.setToMenuPage ();
		}

		public void ChangeCamera (int cameraID)
		{
				if (cameraID == 0)
						_posTransform	= _MenuposTransform;
				else if (cameraID == 1)
						_posTransform	= _SettingsTransform;
		}

		void Update ()
		{
				if (isStartMovement) {
						transform.position	= _posTransform.position;//Vector3.Lerp(transform.position,_posTransform.position,Time.deltaTime*DistanceDamping);
						transform.rotation	= _posTransform.rotation;//Quaternion.Slerp(transform.rotation,_posTransform.rotation,Time.deltaTime*rotationDamping);
				}
		}

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
