using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class AddVector3Value : Action
{
	public SharedVector3 Offset;

	public SharedVector3 InputVector;

	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		InputVector.Value += Offset.Value;
		return TaskStatus.Success;
	}
}