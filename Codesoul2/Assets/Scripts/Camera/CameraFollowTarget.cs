using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] Vector3 camOffset;
    Vector3 cameraOffset = new Vector3(0, 0, -10);

    private void Update()
    {
        CameraFollow(targetPos);
    }

    void CameraFollow(Transform target)
    {
        Camera.main.transform.position = target.position + camOffset + cameraOffset;
    }
}
