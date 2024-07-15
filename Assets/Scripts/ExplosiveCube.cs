using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField][Range(0, 101)] private int _maxDropChance = 100;

    public int MaxDropChance { get => _maxDropChance; }

    private void Start()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    public void SetMaxDropChance(int parentDropChance)
    {
        int dropChanceDivider = 2;

        _maxDropChance = parentDropChance / dropChanceDivider;
    }
}
