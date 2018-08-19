using UnityEngine;

using BeardedManStudios.Forge.Networking.Generated;
public class BasicCube : BasicCubeBehavior
{
    public float speed = 5.0f;

    private void Update()
    {
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            return;
        }
        
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * speed * Time.deltaTime;
        
        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
        
    }
}