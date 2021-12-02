using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    [SerializeField] private Transform _hitMark;

    [SerializeField] private int _damage;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _hitMark.position = hit.point;

            if (Input.GetKeyDown(KeyCode.Mouse0) && hit.collider.TryGetComponent(out IScattering scattering))
            {
                scattering.ApplyImpairment(_damage);
            }
        }
    }
}
