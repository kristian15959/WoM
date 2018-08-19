using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BeardedManStudios.Forge.Networking.Unity;
public class GameLogic : MonoBehaviour {

    private void Start()
    {
        NetworkManager.Instance.InstantiatePlayer(position: new Vector3(364.5f, 22.66f, 242.1f));
    }
}
