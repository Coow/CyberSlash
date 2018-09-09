 using UnityEngine;
 
 public class CameraController : MonoBehaviour {
    [Header("CameraRotation")]
    public Transform target;
    public float angularSpeed;

    [SerializeField][HideInInspector] 
    private Vector3 initialOffset;

    private Vector3 currentOffset;

    [Header("Zoom Variables")]
    public float Sensitivity = 10f;

    private void SetCurrentOffset () {
        if (target == null) {
            return;
        }

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
            float movement = Input.GetAxis ("Mouse X") * angularSpeed * Time.deltaTime;
            if (!Mathf.Approximately (movement, 0f)) {
                transform.RotateAround (target.position, Vector3.up, movement);
                currentOffset = transform.position - target.position;
            }
        }
    }

    void Update() {
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -Sensitivity;
        fov = Mathf.Clamp(fov, 30f, 90f);
        Camera.main.fieldOfView = fov;
    }
}
