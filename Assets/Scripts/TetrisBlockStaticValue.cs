    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockStaticValue : MonoBehaviour
{
    [SerializeField]
    public enum BlockEffect
    {
        Neutral = 0,
        Fire = 1,
        Ice = 2,
        Explosion = 3,
        Enemy = 4,
        Malus = 5
    }

    [SerializeField]
    public List<Color> _effectColor = new List<Color>();

    [SerializeField]
    public List<float> _effectProbabilty = new List<float>();
}
