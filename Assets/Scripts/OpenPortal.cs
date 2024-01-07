using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPortal : MonoBehaviour
{

    [Header("Audio")]
    public AudioClip PortalOpenSound;
    public AudioClip ready, notReady, loading, alreadyLoaded;

    [Header("Portal")]
    public GameObject portal;
    public GameObject explosionParticle, portalBall;

    public bool isLoaded = false;

    public void OnOpen(string[] values)
    {
        int i = 0;
        if (values[0] == "left") i = 0;

        else if (values[0] == "middle") i = 1;

        else if (values[0] == "right") i = 2;

        else print("its not working");

        Transform BallSpawn = portal.GetComponentInChildren<AudioSource>().transform;
        GameObject thisBall = Instantiate(portalBall, BallSpawn.position, BallSpawn.rotation);

        Vector3 fwdDir = BallSpawn.transform.forward * 20f;
        thisBall.GetComponent<Rigidbody>().AddForce(fwdDir, ForceMode.Impulse);

        GameObject particleEffect = Instantiate(explosionParticle, BallSpawn.position, BallSpawn.rotation);
        particleEffect.GetComponent<ParticleSystem>().Play();

        BallSpawn.GetComponent<AudioSource>().PlayOneShot(PortalOpenSound);

    }
}
