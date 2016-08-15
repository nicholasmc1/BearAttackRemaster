//WOW CAMERA BY http://forum.unity3d.com/members/6169-Paintbrush
    
    private var target : Transform;
     
    var targetHeight = 2.0;
    var distance = 5.0;
     
    var maxDistance = 20;
    var minDistance = 2.5;
     
    var xSpeed = 250.0;
    var ySpeed = 120.0;
     
    var yMinLimit = -20;
    var yMaxLimit = 80;
     
    var zoomRate = 20;
     
    var rotationDampening = 3.0;
     
    private var x = 0.0;
    private var y = 0.0;
    
    //public var cameraControl;
     
    @script AddComponentMenu("Camera-Control/WoW Camera")
     
    function Start () 
    {
        target = transform.parent;
        transform.parent = null;
    	PlayerPrefs.SetInt("camControl", 1);
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
     
       // Make the rigid body not change rotation
          if (GetComponent.<Rigidbody>())
          GetComponent.<Rigidbody>().freezeRotation = true;
          
          
          
      //set initial value for camControl
      /*if(PlayerPrefs.GetInt("camControl") == 0)
   			cameraControl = false;
   	  else if(PlayerPrefs.GetInt("camControl") == 1)
   			cameraControl = true;   
   			*/ 
       
    }
     
    function LateUpdate () {
        if(!target)
            return;
       
       /*if(Input.GetKeyDown("f"))
       {
       		if(!cameraControl)
       			cameraControl = true;
       		else
       			cameraControl = false;
       }*/
		
		//toggle camera control
       if(Input.GetKeyDown("f"))
       {
       		if(PlayerPrefs.GetInt("camControl") == 0)
       			PlayerPrefs.SetInt("camControl", 1);
       		else if(PlayerPrefs.GetInt("camControl") == 1)
       			PlayerPrefs.SetInt("camControl", 0);
       }       
       
       if(PlayerPrefs.GetInt("camControl") == 1 && Time.timeScale != 0)
       {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02;
        	y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02;
       
     	}
       else
       {
	        // otherwise, ease behind the target if any of the directional keys are pressed
	       if(Input.GetAxis("Vertical") || Input.GetAxis("Horizontal")) {
	           var targetRotationAngle = target.eulerAngles.y;
	           var currentRotationAngle = transform.eulerAngles.y;
	           x = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);
	       }
	    }
       

       
        distance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
       
       y = ClampAngle(y, yMinLimit, yMaxLimit);
       
        var rotation:Quaternion = Quaternion.Euler(y, x, 0);
        var position = target.position - (rotation * Vector3.forward * distance + Vector3(0,-targetHeight,0));
       
        transform.rotation = rotation;
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 120);
    }
     
    static function ClampAngle (angle : float, min : float, max : float) {
       if (angle < -360)
          angle += 360;
       if (angle > 360)
          angle -= 360;
       return Mathf.Clamp (angle, min, max);
    }