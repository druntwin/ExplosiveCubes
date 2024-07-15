using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Rigidbody _explosiveCubePrefab;
    [SerializeField] private Explosion _explosion;

    private int _minNewCubesCount = 2;
    private int _maxNewCubesCount = 7;

    public void InstantiateNewCubes(ExplosiveCube explosiveParentCube)
    {
        List<Rigidbody> childrenCubes = new();

        if (CalculateInstantiateChance(explosiveParentCube))
        {
            int newCubesCount = Random.Range(_minNewCubesCount, _maxNewCubesCount);
            int scaleDivider = 2;

            for (int i = 0; i < newCubesCount; i++)
                childrenCubes.Add(Instantiate(_explosiveCubePrefab, explosiveParentCube.transform.position, Random.rotation));

            foreach (Rigidbody newCube in childrenCubes)
            {
                newCube.TryGetComponent(out ExplosiveCube explosiveCube);
                explosiveCube.SetMaxDropChance(explosiveParentCube.MaxDropChance);

                newCube.transform.localScale = explosiveParentCube.transform.localScale / scaleDivider;
            }

            Destroy(explosiveParentCube.gameObject);
        }

        _explosion.Explode(explosiveParentCube);
    }

    private bool CalculateInstantiateChance(ExplosiveCube explosiveParentCube)
    {
        int fullDropChancePercent = 100;
        int currentDropChance = Random.Range(0, fullDropChancePercent + 1);

        if (currentDropChance <= explosiveParentCube.MaxDropChance)
            return true;

        return false;
    }
}
