using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Explosion : MonoBehaviour
{
    [SerializeField] private int _explosiveDeafaultRadius = 1;
    [SerializeField] private int _explosiveDefaultForce = 100;

    private int _explosiveRadius = 1;

    public void Explode(ExplosiveCube explosiveParentCube, bool isSpawn)
    {
        int explosiveForce = _explosiveDefaultForce * explosiveParentCube.Generation;

        _explosiveRadius = _explosiveDeafaultRadius * explosiveParentCube.Generation;

        foreach (Rigidbody explodableObject in GetExplodibleObjects(explosiveParentCube))
        {
            if (isSpawn)
            {
                explodableObject.AddExplosionForce(_explosiveDefaultForce, explosiveParentCube.transform.position, _explosiveDeafaultRadius);
            }
            else
            {
                explodableObject.AddExplosionForce(explosiveForce, explosiveParentCube.transform.position, _explosiveRadius);
            }
        }

        Destroy(explosiveParentCube.gameObject);
    }

    private List<Rigidbody> GetExplodibleObjects(ExplosiveCube explosiveParentCube)
    {
        Collider[] hits = Physics.OverlapSphere(explosiveParentCube.transform.position, _explosiveRadius);
        return hits.Where(hit => hit.attachedRigidbody != null).Select(hit => hit.attachedRigidbody).ToList();
    }
}
