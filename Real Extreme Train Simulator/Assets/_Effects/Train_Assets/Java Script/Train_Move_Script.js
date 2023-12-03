#pragma strict

var Flag : boolean ;

function Start () {
	GetComponent.<Rigidbody>().maxAngularVelocity = 100 ;
	Physics.IgnoreLayerCollision ( 0 , 8 , true ) ;
	Physics.IgnoreLayerCollision ( 8 , 8 , false ) ;
}

function Update () {
	if ( Flag ) {
			GetComponent.<Rigidbody>().AddRelativeTorque ( Input.GetAxis ( "Vertical" ) * 300 , 0 , 0 ) ;
	}
}