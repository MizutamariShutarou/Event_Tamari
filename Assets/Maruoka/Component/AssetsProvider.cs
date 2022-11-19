using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsProvider : MonoBehaviour
{
    [SerializeField]
    private GameObject _santaPrefab = default;
    [SerializeField]
    private GameObject _deerPrefab = default;
    [SerializeField]
    private GameObject _unionPrefab = default;
    [SerializeField]
    private CinemachineVirtualCamera _cinemachineVirtualCamera = default;

    public GameObject SantaPrefab =>_santaPrefab;
    public GameObject DeerPrefab => _deerPrefab;
    public GameObject UnionPrefab => _unionPrefab;
    public CinemachineVirtualCamera CinemachineVirtualCamera => _cinemachineVirtualCamera;

    private void Start()
    {
        OperableCharacterManager.Instance.SetAssetsProvider(this);
    }
}