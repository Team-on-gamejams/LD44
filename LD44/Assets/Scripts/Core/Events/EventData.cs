using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventData {
	readonly string eventId = "EventId";
	public Dictionary<string, object> Data;

	public EventData(string eventId) {
		Data = new Dictionary<string, object> {
			[this.eventId] = eventId
		};
	}
}
