using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBehavior : MonoBehaviour
{
    public AudioManager audioManager;

    private void OnTriggerEnter(Collider other)
    {
        audioManager = FindObjectOfType<AudioManager>();
        // Perform some action, and add some check logic here such as destory the object
        if (other.name == "player")
        {
            DestoryParent();
            audioManager.PlaySoundEffect(audioManager.audioClips[3]);

}
        //Debug.Log(other + "Collider is inside the trigger of another collider");
    }

    public void DestoryParent()
    {
        Destroy(transform.parent.gameObject);
    }
}
