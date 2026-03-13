using UnityEngine;
 
// Attach to a GameObject in the scene (e.g. a "SoundManager" empty GameObject).
// Assign your AudioClips in the Inspector.
public class SoundManager : MonoBehaviour
{
    static private SoundManager S;
 
    [Header("Inscribed")]
    [Tooltip("Short snap/twang sound played when the slingshot fires.")]
    public AudioClip slingshotSnap;
 
    [Tooltip("Looping whoosh sound played while the projectile is in flight.")]
    public AudioClip projectileWhir;
 
    private AudioSource snapSource;
    private AudioSource whirSource;
 
    void Awake()
    {
        S = this;
 
        snapSource = gameObject.AddComponent<AudioSource>();
        snapSource.playOnAwake = false;
 
        whirSource = gameObject.AddComponent<AudioSource>();
        whirSource.playOnAwake = false;
        whirSource.loop = true;
        whirSource.spatialBlend = 0f;
    }
 
    static public void PLAY_SNAP()
    {
        if (S != null && S.slingshotSnap != null)
            S.snapSource.PlayOneShot(S.slingshotSnap);
    }
 
    static public void START_WHIR()
    {
        Debug.Log("PLAY_SNAP called, clip is: " + (S == null ? "S is NULL" : S.slingshotSnap == null ? "clip is NULL" : "clip OK"));
        if (S != null && S.projectileWhir != null && !S.whirSource.isPlaying)
        {
            S.whirSource.clip = S.projectileWhir;
            S.whirSource.Play();
        }
    }
 
    static public void STOP_WHIR()
    {
        if (S != null && S.whirSource.isPlaying)
            S.whirSource.Stop();
    }
}