using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipRefsSO", menuName = "AudioClipRefs/AudioClipRefsSO")]
public class AudioClipRefsSO : ScriptableObject {

    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySuccess;
    public AudioClip[] footsteps;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;

}
