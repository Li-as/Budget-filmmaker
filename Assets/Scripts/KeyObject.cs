using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    [SerializeField] private KeyObjectGhost _objectGhost;
    [SerializeField] private KeyObjectMover _mover;

    private bool _isInCorrectPlace;

    public bool IsInCorrectPlace => _isInCorrectPlace;
    public KeyObjectGhost ObjectGhost => _objectGhost;

    private void OnEnable()
    {
        _mover.TargetReached += OnTargetReached;
    }

    private void OnDisable()
    {
        _mover.TargetReached -= OnTargetReached;
    }

    private void OnTargetReached()
    {
        StartCoroutine(SuccessMovementRoutine());
    }

    private IEnumerator SuccessMovementRoutine()
    {
        while (transform.position != _objectGhost.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _objectGhost.transform.position, Time.deltaTime);
            yield return null;
        }

        _isInCorrectPlace = true;
        _objectGhost.Disappear();
    }
}
