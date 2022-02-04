using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _followTarget;
    [Space]
    [SerializeField] Vector3 _offset;

    void Start()
    {
        _offset = new Vector3(-_followTarget.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(_followTarget.position.x + _offset.x, _offset.y, _offset.z);

        transform.position = targetPosition;
    }
}