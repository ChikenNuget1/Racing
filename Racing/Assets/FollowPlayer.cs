using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float smoothSpeed = 5f; // How smooth the camera feels
    public float playerWeight = 0.2f; // How much the player affects the camera
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    private Camera cam;
    float cameraZ = -10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        cameraZ = transform.position.z;
    }

    private void LateUpdate()
    {
        Vector2 playerSpeed = player.GetComponent<Driving>().getSpeed();

        float zDist = Mathf.Abs(cam.transform.position.z - player.transform.position.z);

        Vector3 playerScreenPosition = player.transform.position;

        Vector3 playerWithOffset = player.transform.position + new Vector3(playerSpeed.x, playerSpeed.y, 0f);

        Vector3 targetPoint = Vector3.Lerp(playerWithOffset, playerScreenPosition, playerWeight);

        Vector3 desired = new Vector3(targetPoint.x, targetPoint.y, cameraZ);

        cam.transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
    }
}

//using UnityEngine;

//public class followPlayer : MonoBehaviour
//{
//    public GameObject player;
//    public float smoothSpeed = 5f; // How smooth the camera feels
//    public float mouseWeight = 0.2f; // How much the mouse affects the camera
//    public Vector3 offset = new Vector3(0f, 1f, -10f);
//    private Camera cam;
//    float cameraZ = -10f;
//    void Start()
//    {
//        cam = Camera.main;
//        cameraZ = transform.position.z; // Store the camera Z

//    }

//    private void LateUpdate()
//    {
//        // Idk what this does tbh
//        float zDist = Mathf.Abs(cam.transform.position.z - player.transform.position.z);

//        // Get mouse position
//        Vector3 mouseScreenPosition = Input.mousePosition;

//        // Convert mouse position to world position
//        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(mouseScreenPosition);

//        // Add offset to the camera
//        Vector3 playerWithOffset = player.transform.position + new Vector3(offset.x, offset.y, 0f);

//        // Calculate the target camera point
//        Vector3 targetPoint = Vector3.Lerp(playerWithOffset, mouseWorldPosition, mouseWeight);

//        // Set desired location
//        Vector3 desired = new Vector3(targetPoint.x, targetPoint.y, cameraZ);

//        // Move camera
//        transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
//    }
//}
