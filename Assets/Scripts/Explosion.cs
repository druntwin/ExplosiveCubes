using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private int _explosiveDeafaultRadius = 1;
    [SerializeField] private int _explosiveDefaultForce = 100;

    private int _explosiveRadius = 1;

    public void Explode(ExplosiveCube explosiveParentCube)
    {
        int explosiveForce = _explosiveDefaultForce * explosiveParentCube.Generation;

        _explosiveRadius = _explosiveDeafaultRadius * explosiveParentCube.Generation;

        foreach (Rigidbody explodableObject in GetExplodibleObjects(explosiveParentCube))
        {
            explodableObject.AddExplosionForce(explosiveForce, explosiveParentCube.transform.position, _explosiveRadius);
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
