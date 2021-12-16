using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraBounds : Singleton<CameraBounds>
{

    [Header("Setting player")]
    public Transform Target;
    public Vector3 Offset;
    public Rect _levelBounds;

    private Rect _screenExtents;
    private Camera _camera;

    public Camera Camera => _camera;

    [SerializeField]
    private bool _useBoundRestrictions = false;
    private float depth = 0;



    void Awake()
    {
        _camera = GetComponent<Camera>();
        _camera.orthographic = true;

        _screenExtents = new Rect(-_camera.orthographicSize * _camera.aspect, -_camera.orthographicSize, _camera.aspect * _camera.orthographicSize * 2, _camera.orthographicSize * 2);
    }

    private void LateUpdate()
    {
        if (Target)
        {
            if (_useBoundRestrictions)
            {
                transform.position = ApplyBoundRestrictions(new Vector3(Target.position.x, Target.position.y, depth) + Offset);
            }
            else
            {
                transform.position = new Vector3(Target.position.x, Target.position.y, depth) + Offset;
            }
        }
    }

    private Vector3 ApplyBoundRestrictions(Vector3 position)
    {
        float _boundOffset;

        // Check Right
        _boundOffset = (position.x + _screenExtents.max.x) - _levelBounds.max.x;
        if (_boundOffset > 0)
        {
            position = new Vector3(position.x - _boundOffset, position.y, position.z);
        }
        // Check Left
        _boundOffset = (position.x + _screenExtents.min.x) - _levelBounds.min.x;
        if (_boundOffset < 0)
        {
            position = new Vector3(position.x - _boundOffset, position.y, position.z);
        }
        // Check Top
        _boundOffset = (position.y + _screenExtents.max.y) - _levelBounds.max.y;
        if (_boundOffset > 0)
        {
            position = new Vector3(position.x, position.y - _boundOffset, position.z);
        }
        // Check Bottom
        _boundOffset = (position.y + _screenExtents.min.y) - _levelBounds.min.y;
        if (_boundOffset < 0)
        {
            position = new Vector3(position.x, position.y - _boundOffset, position.z);
        }
        return position;
    }

    private void OnDrawGizmos()
    {
        /*CH02*/
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(_levelBounds.x, _levelBounds.y, 0), new Vector3(_levelBounds.x + _levelBounds.width, _levelBounds.y, 0));
        Gizmos.DrawLine(new Vector3(_levelBounds.x + _levelBounds.width, _levelBounds.y, 0), new Vector3(_levelBounds.x + _levelBounds.width, _levelBounds.y + _levelBounds.height, 0));
        Gizmos.DrawLine(new Vector3(_levelBounds.x + _levelBounds.width, _levelBounds.y + _levelBounds.height, 0), new Vector3(_levelBounds.x, _levelBounds.y + _levelBounds.height, 0));
        Gizmos.DrawLine(new Vector3(_levelBounds.x, _levelBounds.y + _levelBounds.height, 0), new Vector3(_levelBounds.x, _levelBounds.y, 0));
        /*CH02*/

    }

}


