using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SendCustomEvent : Action
{
	public enum Event
	{
		InteractiveAction
	}

	DataProvider DataProvider;
	public Event EventType;

	public override void OnStart()
	{
		DataProvider = DataProvider.Instance;
	}

	public override TaskStatus OnUpdate()
	{
		switch (EventType)
		{
			case Event.InteractiveAction:
				DataProvider.Events.EnteractiveAction();
				break;
		}
		return TaskStatus.Success;
	}
}