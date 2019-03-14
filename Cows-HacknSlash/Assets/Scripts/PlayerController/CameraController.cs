 using UnityEngine;
 
 public class CameraController : MonoBehaviour {
    [Header("CameraRotation")]
    public Transform target;
    public float angularSpeed;

    [SerializeField][HideInInspector] 
    private Vector3 initialOffset;

    private Vector3 currentOffset;

    private Vector3 offset;
    

    [Header("Zoom Variables")]
    public float Sensitivity = 10f;

    private void SetCurrentOffset () {
        if (target == null) {
            return;
        }

        gameObject.transform.eulerAngles = new Vector3(60,-180,0);
        initialOffset = transform.position - target.position;

    }

    private void Start () {
		transform.LookAt(target);
		SetCurrentOffset(); 
        if (target == null) {
            Debug.LogError ("Assign a target for the camera in Unity's inspector");
        }

        currentOffset = initialOffset;
    }

    private void LateUpdate () {
        transform.position = target.position + currentOffset;
        if (Input.GetMouseButton(2)) {
            float Xmovement = Input.GetAxis ("Mouse X") * angularSpeed * Time.deltaTime;
            if (!Mathf.Approximately (Xmovement, 0f)) {
                transform.RotateAround (target.position, Vector3.up, Xmovement);
                currentOffset = transform.position - target.position;
            }
            /*float Ymovement = Input.GetAxis ("Mouse Y") * angularSpeed * Time.deltaTime;
            if (!Mathf.Approximately (Ymovement, 0f)) {
                transform.RotateAround (target.position, Vector3.left, Ymovement);
                currentOffset = transform.position - target.position;
            }*/
        }
    }
    
    void Update() {
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -Sensitivity;
        fov = Mathf.Clamp(fov, 30f, 110f);
        Camera.main.fieldOfView = fov;

        if (Input.GetKeyDown(KeyCode.X)) {
            ResetCameraLocation();
        }
    }

    public void ResetCameraLocation(){
        Debug.Log("Havent bother implementing this yet :D");
    }
}
