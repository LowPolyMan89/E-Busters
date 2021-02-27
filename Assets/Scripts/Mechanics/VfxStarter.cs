using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxStarter : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    public float _vfxTime;
    private bool isEmmite = false;

    public void ChangePos(Vector3 pos)
    {
        foreach (var x in particleSystems)
        {
            x.transform.position = pos;
        }
    }

    public void Activate()
    {
        if(isEmmite)
        {
            return;
        }

        StartCoroutine(ActivateRoot());
    }

    public IEnumerator ActivateRoot()
    {
        isEmmite = true;

        foreach(var x in particleSystems)
        {
            x.Clear();
            x.Play();
        }

        yield return new WaitForSeconds(_vfxTime);

        foreach (var x in particleSystems)
        {
            x.Stop();
        }

        isEmmite = false;
    }

    public void Remove(float time)
    {
        Destroy(this.gameObject, time);
    }
}
