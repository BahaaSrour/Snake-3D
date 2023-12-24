using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    public int Id { get; set; }
    private bool _wasClaimed = false;
    [SerializeField] public Transform _textboxContainer;
    [HideInInspector] public Transform mytransform;
    [SerializeField] FoodDetails foodDetails;
    public bool WasClaimed
    {
        get
        {
            return _wasClaimed;
        }
        set
        {
            _wasClaimed = value;
        }
    }
    public float Value { get; private set; }
    private void OnEnable()
    {
        mytransform = transform;
        Value = UnityEngine.Random.value * 100f - 25f;
    }
}
