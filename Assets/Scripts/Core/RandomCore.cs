using UnityEngine;
using System.Collections;

public class RandomCore : MonoBehaviour
{
    private ArrayList _x = new ArrayList();
    private ArrayList _y = new ArrayList();
    private GameObject _matrix;
    private void Awake()
    {
        _matrix = GameObject.Find("Matrix");

        float _xMin, _xMax;
        float _yMin, _yMax;

        _yMax = _matrix.transform.GetChild(0).transform.localPosition[1]; 
        _yMin = _matrix.transform.GetChild(1).transform.localPosition[1];
        _xMin = _matrix.transform.GetChild(2).transform.localPosition[0];
        _xMax = _matrix.transform.GetChild(3).transform.localPosition[0];

        float _ySize = _yMax - _yMin;
        float _xSize = _xMax - _xMin;

        for(float i = _yMin + 0.5f; i < _yMax; i += 1.0f)
        {
            _y.Add(i);
        }

        for (float i = _xMin + 0.5f; i < _xMax; i += 1.0f)
        {
            _x.Add(i);
        }
    }

    public Vector2 RandomPosition()
    {
        Vector2 vector = Vector2.zero;
        float x, y;

        y = (float)_y[Random.Range(0, _y.Count)];
        x = (float)_x[Random.Range(0, _y.Count)];
        
        vector[0] = x;
        vector[1] = y;

        return vector;
    }
}
