using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager {
	public static event EventController.MethodContainer OnTimeStopChangedEvent;
	public void CallOnTimeStopChangedEvent(EventData ob = null) => OnTimeStopChangedEvent?.Invoke(ob);

	public static event EventController.MethodContainer BloodLevelChangedEvent;
	public void CallOnBloodLevelChangedEvent(EventData ob = null) => BloodLevelChangedEvent?.Invoke(ob);

	public static event EventController.MethodContainer ChangeBuildingsListEvent;
	public void CallOnChangeBuildingsListEvent(EventData ob = null) => ChangeBuildingsListEvent?.Invoke(ob);

	public static event EventController.MethodContainer AddOrRemoveBuildingsEvent;
	public void CallAddOrRemoveBuildingsEvent(EventData ob = null) => AddOrRemoveBuildingsEvent?.Invoke(ob);
}
