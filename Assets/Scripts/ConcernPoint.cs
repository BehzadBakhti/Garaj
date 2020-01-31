using System;
using TouchScript.Gestures;
using UnityEngine;

public  abstract class ConcernPoint : MonoBehaviour
{
    public string Name;
    public event Action<ConcernPoint> Spwaned;
    public float Radius;


    [SerializeField] private LineRenderer _lineRenderer;
    protected PressGesture Press;

    private void Awake()
    {
        Press = GetComponent<PressGesture>();
        
    }
    public void OnEnable()
    {
        Press.Pressed += _press_Pressed;
    }
    public void OnDisable()
    {
        Press.Pressed -= _press_Pressed;
    }

    private void _press_Pressed(object sender, EventArgs e)
    {
        Select();
    }

    protected virtual void Select()
    {
    }

    public void RayOut()
    {
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, transform.position* 5);
    }

    public void SetLineColor(Color color)
    {
        _lineRenderer.startColor=color;
        _lineRenderer.endColor = color;
    }


    public virtual void OnSpwaned()
    {
        Spwaned?.Invoke(this);
    }

  
}