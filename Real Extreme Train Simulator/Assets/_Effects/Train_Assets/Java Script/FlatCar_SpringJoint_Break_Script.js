#pragma strict

var joint : Joint ;
var ConnectedBody : Transform ;

function Start () {
	joint = GetComponent ( SpringJoint ) ;
	if ( joint.connectedBody ) {
			ConnectedBody = joint.connectedBody.transform ;
	} else {
			Destroy ( GetComponent ( SpringJoint ) ) ;
	}
}
		
function Update () {	
	if ( transform.eulerAngles.z > 30.0 && transform.eulerAngles.z < 330.0 ) {
			Destroy ( joint ) ;
			Destroy ( this ) ;
	}
}