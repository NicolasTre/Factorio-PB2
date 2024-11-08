using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class FC_MenuLevelManager : MonoBehaviour
{
    private FC_TileConvoyerSystem convoyerSystem;
    
    [Header("Cam Settings")]
    [SerializeField] private CinemachineVirtualCamera _cam;
    [SerializeField, Min(0)] private float _lensOrtoSize = 10.0f;

    [Header("Convoyer Settings")]
    [SerializeField] private float _convoyerDrawSpeed = 0.03f;
    public List<FC_MenuConvoyer> convoyers { get; private set; }


    void Awake()
    {
        _cam.m_Lens.OrthographicSize = _lensOrtoSize;
        convoyerSystem = GetComponent<FC_TileConvoyerSystem>();

        convoyers = new();
    }

    private void Start()
    {
        StartCoroutine(AnimateCreateTile());
    }

    public void AddConvoyer(FC_MenuConvoyer newConvoyer)
    {
        convoyers.Add(newConvoyer);
    }

    private IEnumerator AnimateCreateTile()
    {
        foreach (FC_MenuConvoyer convoyer in convoyers)
        {
            convoyerSystem.CreateTile(convoyer.data.direction, convoyer.data.position);
            yield return new WaitForSeconds(_convoyerDrawSpeed);
        }
    }
}