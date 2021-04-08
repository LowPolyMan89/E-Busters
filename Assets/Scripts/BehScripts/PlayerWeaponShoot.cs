using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PlayerWeaponShoot : Action
{
	DataProvider DataProvider;

	public override void OnStart()
	{
		DataProvider = DataProvider.Instance;
	}

	public override TaskStatus OnUpdate()
	{
		if(DataProvider.Player.CurrentWeapon)
		{
			DataProvider.Player.CurrentWeapon.Shoot();
		}

		return TaskStatus.Success;
	}
}