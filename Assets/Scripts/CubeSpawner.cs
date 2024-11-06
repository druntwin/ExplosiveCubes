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
        bool isSpawn = CalculateChance(explosiveParentCube);

        if (isSpawn)
        {
            int newCubesCount = Random.Range(_minNewCubesCount, _maxNewCubesCount);
            int scaleDivider = 2;

            for (int i = 0; i < newCubesCount; i++)
                childrenCubes.Add(Instantiate(_explosiveCubePrefab, explosiveParentCube.transform.position, Random.rotation));

            foreach (Rigidbody newCube in childrenCubes)
            {
                newCube.TryGetComponent(out ExplosiveCube explosiveCube);
                explosiveCube.SetMaxDropChance(explosiveParentCube.MaxDropChance);
                explosiveCube.SetGeneration(explosiveParentCube.Generation);

                newCube.transform.localScale = explosiveParentCube.transform.localScale / scaleDivider;
            }

            _explosion.Explode(explosiveParentCube, isSpawn);
        }
        else
        {
            _explosion.Explode(explosiveParentCube, isSpawn);
        }
    }

    private bool CalculateChance(ExplosiveCube explosiveParentCube)
    {
        int fullDropChancePercent = 100;
        int currentDropChance = Random.Range(0, fullDropChancePercent + 1);

        return currentDropChance <= explosiveParentCube.MaxDropChance;
    }
}
