using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtNet : MonoBehaviour
{
    private Cloth _clothComponent;
    private SphereCollider _ballCollider;
    private GameBehaviour _gameBehaviour;

    public GameObject ballInScene;

    
    
    void Start()
    {
        _gameBehaviour = GameObject.Find("Game Behaviour").GetComponent<GameBehaviour>();
        _clothComponent = GetComponent<Cloth>();

        Invoke("FindBallAndAddToCloth", 2f);


    }

    private void FindBallAndAddToCloth()
    {
        ballInScene = _gameBehaviour.ballsInScene[0];
        _ballCollider =  ballInScene.GetComponent<SphereCollider>();

        var clothColliders = new ClothSphereColliderPair[1];
        clothColliders[0] = new ClothSphereColliderPair(_ballCollider);

        _clothComponent.sphereColliders = clothColliders;


    }
}
