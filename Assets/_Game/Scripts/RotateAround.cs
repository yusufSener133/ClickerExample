using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * _speed);
    }
}
