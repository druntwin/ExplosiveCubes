using UnityEngine;

[RequireComponent(typeof(CubeSpawner))]
public class TargetController : MonoBehaviour
{
    private CubeSpawner _cubeSpawner;

    private void Awake()
    {
        _cubeSpawner = GetComponent<CubeSpawner>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                hitInfo.collider.TryGetComponent(out ExplosiveCube explosiveCube);                

                if (explosiveCube != null)
                {
                    _cubeSpawner.InstantiateNewCubes(explosiveCube);
                }
            }
        }
    }
}
