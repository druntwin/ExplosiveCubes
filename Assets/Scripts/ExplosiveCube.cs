using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCube : MonoBehaviour
{
    [SerializeField] private Rigidbody _explosiveCubePrefab;

    [SerializeField] private Renderer _renderer;
    [SerializeField] private Transform _trarnsform;

    [SerializeField] private float _explosiveRadius;
    [SerializeField] private float _explosiveForce;

    private int _generation = 1;
    private int _minNewCubesCount = 2;
    private int _maxNewCubesCount = 7;
    private List<Rigidbody> _childrenCubes = new();
    private Material _material;
    private Color _color;

    private void Start()
    {
        _material = new Material(Shader.Find("Standard"));
        _color = Random.ColorHSV();
        _material.color = _color;
        _renderer.material = _material;

        SetScale();
    }

    private void OnMouseUpAsButton()
    {
        InstantiateNewCubes();
        Explode();
        Destroy(gameObject);
    }

    public void UpGeneration(int generation)
    {
        _generation = generation;
    }

    private void SetScale()
    {
        _trarnsform.localScale = Vector3.one / _generation;
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in _childrenCubes)
            explodableObject.AddExplosionForce(_explosiveForce, transform.position, _explosiveRadius);
    }

    private void InstantiateNewCubes()
    {
        int newCubesCount = Random.Range(_minNewCubesCount, _maxNewCubesCount);
        int startRangeNumber = 1;
        int dropKey = Random.Range(startRangeNumber, _generation + 1);

        if(dropKey == _generation) 
        {
            for (int i = 0; i < newCubesCount; i++)
                _childrenCubes.Add(Instantiate(_explosiveCubePrefab, transform.position, Random.rotation));

            foreach(Rigidbody cube in _childrenCubes)
            {
                ExplosiveCube explosiveCube = cube.GetComponent<ExplosiveCube>();
                explosiveCube.UpGeneration(_generation + 1);
            }
        }
    }
}
