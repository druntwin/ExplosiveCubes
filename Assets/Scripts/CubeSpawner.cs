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

        if (CalculateChance(explosiveParentCube))
        {
            int newCubesCount = Random.Range(_minNewCubesCount, _maxNewCubesCount);
            int scaleDivider = 2;

            for (int i = 0; i < newCubesCount; i++)
                childrenCubes.Add(Instantiate(_explosiveCubePrefab, explosiveParentCube.transform.position, Random.rotation));

            foreach (Rigidbody newCube in childrenCubes)
            {
                newCube.TryGetComponent(out ExplosiveCube explosiveCube);
                explosiveCube.SetMaxDropChance(explosiveParentCube.MaxDropChance);
                explosiveCube.UpgradeGeneration(explosiveParentCube.Generation);

                newCube.transform.localScale = explosiveParentCube.transform.localScale / scaleDivider;
            }

            Destroy(explosiveParentCube.gameObject);
        }
        else
        {
            _explosion.Explode(explosiveParentCube);
        }
    }

    private bool CalculateChance(ExplosiveCube explosiveParentCube)
    {
        int fullDropChancePercent = 100;
        int currentDropChance = Random.Range(0, fullDropChancePercent + 1);

        return currentDropChance <= explosiveParentCube.MaxDropChance;
    }
}
