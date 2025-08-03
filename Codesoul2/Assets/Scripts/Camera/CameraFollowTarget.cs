using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] Vector3 cameraOffset = new Vector3(0, 0, -10);
    [SerializeField] float time;

    private void Update()
    {
        CameraFollow(targetPos);
    }

    void CameraFollow(Transform target)
    {
        //Camera.main.transform.position = target.position + camOffset + cameraOffset;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, target.transform.position + cameraOffset, time);
    }
}
