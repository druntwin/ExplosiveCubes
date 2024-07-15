using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosiveRadius = 2f;
    [SerializeField] private float _explosiveForce = 200f;

    public void Explode(ExplosiveCube explosiveParentCube)
    {
        foreach (Rigidbody explodableObject in GetExplodibleObjects(explosiveParentCube))
        {
            explodableObject.AddExplosionForce(_explosiveForce, explosiveParentCube.transform.position, _explosiveRadius);
        }

        Destroy(explosiveParentCube.gameObject);
    }

    private List<Rigidbody> GetExplodibleObjects(ExplosiveCube explosiveParentCube)
    {
        Collider[] hits = Physics.OverlapSphere(explosiveParentCube.transform.position, _explosiveRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        return cubes;
    }
}
