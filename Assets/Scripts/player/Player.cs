using UnityEngine;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class Player : PlayerBehavior
{

    private string[] nameParts = new string[] { "crazy", "cat", "dog", "homie", "bobble", "mr", "ms", "mrs", "castle", "flip", "flop" };

    public string Name { get; private set; }

    protected override void NetworkStart()
    {
        base.NetworkStart();

        if(!networkObject.IsOwner)
        {
            GetComponent<PlayerController>().enabled = false;

            Destroy(GetComponent<Rigidbody>());
        }

        ChangeName();
    }

    public void ChangeName()
    {
        // Only the owning client of this object can assign the name
        if (!networkObject.IsOwner)
            return;
        // Get a random index for the first name
        int first = Random.Range(0, nameParts.Length - 1);
        // Get a random index for the last name
        int last = Random.Range(0, nameParts.Length - 1);

        // Assign the name to the random selection
        Name = nameParts[first] + " " + nameParts[last];

        // Send an RPC to let everyone know what the name is for this player
        // We use "AllBuffered" so that if people come late they will get the
        // latest name for this object
        // We pass in "Name" for the args because we have 1 argument that is to
        // be a string as it is set in the NCW
        networkObject.SendRpc(RPC_UPDATE_NAME, Receivers.AllBuffered, Name);
    }

    private void Update()
    {
        // If this is not owned by the current network client then it needs to
        // assign it to the position and rotation specified
        if (!networkObject.IsOwner)
        {
            // Assign the position of this player to the position sent on the network
            transform.position = networkObject.position;

            // Assign the rotation of this player to the rotation sent on the network
            transform.rotation = networkObject.rotation;

            // Assign the animation values of this player to the values sent on the network
            GetComponent<PlayerController>().anim.SetFloat("Speed", networkObject.speed);
            GetComponent<PlayerController>().anim.SetFloat("AnimationSpeed", networkObject.animationSpeed);

            // Stop the function here and don't run any more code in this function
            return;
        }
        
        // Since we are the owner, tell the network the updated position
        networkObject.position = transform.position;

        // Since we are the owner, tell the network the updated rotation
        networkObject.rotation = transform.rotation;

        // Since we are the owner, tell the network the updated speed
        networkObject.speed = GetComponent<PlayerController>().anim.GetFloat("Speed");
        networkObject.animationSpeed = GetComponent<PlayerController>().anim.GetFloat("AnimationSpeed");

        // Note: Forge Networking takes care of only sending the delta, so there
        // is no need for you to do that manually
    }

    public override void UpdateName(RpcArgs args)
    {
        Name = args.GetNext<string>();
    }
}