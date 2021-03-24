using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXEvents : MonoBehaviour
{
    [SerializeField] private ParticleSystem part;
    [SerializeField] private List<ParticleCollisionEvent> collisionEvents;
    [SerializeField] private VfxStarter vfxStarter;
    private DataProvider dataProvider;

    void Start()
    {
        dataProvider = DataProvider.Instance;
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        int i = 0;

        while (i < numCollisionEvents)
        {

            if (collisionEvents[i].colliderComponent.gameObject.GetComponent<DamegableObject>())
            {
                DataProvider.Instance.Events.BulletHitEvent(dataProvider.Player.CurrentWeapon.weaponData.DamageNormal, collisionEvents[i].colliderComponent.gameObject.GetComponent<DamegableObject>());
            }

            VfxStarter vfx = Instantiate(vfxStarter, collisionEvents[i].intersection, Quaternion.identity);
            vfx.Activate();
            vfx.Remove(0.5f);
            i++;
        }
    
    }
}
