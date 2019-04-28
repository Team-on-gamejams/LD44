using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager {
	public static event EventController.MethodContainer OnTimeStopChangedEvent;
	public void CallOnTimeStopChangedEvent(EventData ob = null) => OnTimeStopChangedEvent?.Invoke(ob);

	public static event EventController.MethodContainer BloodLevelChangedEvent;
	public void CallOnBloodLevelChangedEvent(EventData ob = null) => BloodLevelChangedEvent?.Invoke(ob);
}
