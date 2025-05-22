using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float Speed = 1f;
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * Speed);
    }
}
