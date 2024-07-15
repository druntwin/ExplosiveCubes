using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                hitInfo.collider.gameObject.TryGetComponent(out ExplosiveCube explosiveCube);                

                if (explosiveCube != null)
                {
                    _cubeSpawner.InstantiateNewCubes(explosiveCube);
                }
            }
        }
    }
}
