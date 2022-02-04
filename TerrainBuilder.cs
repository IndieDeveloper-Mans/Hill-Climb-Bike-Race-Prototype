using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteShapeController))]
public class TerrainBuilder : MonoBehaviour
{
    Spline _spline;
    [Space]
    [SerializeField] int _mapScale;
    [Space]
    [SerializeField] int _pointsAmount;
    [Space]
    [SerializeField] int _pointDistance;
    [Space]
    [SerializeField] float _minHigh, _maxHigh;

    void Start()
    {
        if (_spline == null)
        {
            _spline = GetComponent<SpriteShapeController>().spline;
        }

        BuildTerrain();
    }

    void BuildTerrain()
    {
        _mapScale = _pointsAmount * _pointDistance;

        _spline.SetPosition(2, _spline.GetPosition(2) + Vector3.right * _mapScale);

        _spline.SetPosition(3, _spline.GetPosition(3) + Vector3.right * _mapScale);


        for (int i = 0; i < _pointsAmount; i++)
        {
            float xPos = _spline.GetPosition(i + 1).x + _pointDistance;

            // более м€гкие углы стыков
            //float yPos = Mathf.PerlinNoise(i * Random.Range(minHigh, maxHigh), 0);

            // грубее и сложнее дл€ прохождени€
            float yPos = Random.Range(_minHigh, _maxHigh);

            _spline.InsertPointAt(i + 2, new Vector3(xPos, yPos));

            _spline.SetTangentMode(i, ShapeTangentMode.Continuous);

            _spline.SetLeftTangent(i, new Vector2(-1, 0));

            _spline.SetRightTangent(i, new Vector2(1, 0));
        }
    }
}