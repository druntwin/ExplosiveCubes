using UnityEngine;

public class ExplosiveCube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField][Range(0, 101)] private int _maxDropChance = 100;

    public int Generation {  get; private set; }

    public int MaxDropChance { get => _maxDropChance; }

    private void Start()
    {
        Generation = 1;
        _renderer.material.color = Random.ColorHSV();
    }

    public void SetMaxDropChance(int parentDropChance)
    {
        int dropChanceDivider = 2;

        _maxDropChance = parentDropChance / dropChanceDivider;
    }

    public void SetGeneration(int parentGeneration)
    {
        Generation = parentGeneration + 1;
    }
}
