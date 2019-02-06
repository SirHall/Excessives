using System;

//Modified from: https://github.com/FuzzyHobo/UnityCallbackAndEventTutorial/blob/master/Assets/Scenes/EventCallbackScene/Event.cs
//Now doesn't generate garbage
//How to at bottom

public class GlobalEvent<T> where T : GlobalEvent<T>, IDisposable, new() {
	static T instance = new T();

	public static T Get() {
		//if (instance == null)
		instance = new T();
		if (InUse)
			throw new Exception("Cannot get object twice! Please fire event before grabbing the instance again");
		instance.inUse = true;
		return instance;
	}

	private bool inUse = false;
	public static bool InUse => instance.inUse;

	public delegate void EventListener(T info);
	private static event EventListener listeners;

	public static void RegisterListener(EventListener listener) {
		listeners += listener;
	}

	public static void UnregisterListener(EventListener listener) {
		listeners -= listener;
	}

	public void FireEvent() { //Automatically fires when 'disposed'
		if (!inUse)
			throw new Exception("This event has already fired, to prevent infinite loops you can't refire an event");
		if (listeners != null)
			listeners(this as T);
		inUse = false;
		Reset(); //{TODO} Push this back into the master for Excessives
	}

	protected virtual void Reset() { }

	#region IDisposable Implementation

	bool Disposed { get => !inUse; set => inUse = !value; }

	public void Dispose() {
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	// Protected implementation of Dispose pattern.
	protected virtual void Dispose(bool disposing) {
		if (Disposed)
			return;
		if (disposing) { }

		FireEvent();
		Disposed = true;
	}
	#endregion
}

/*Example event
 * 
 *	public class DebugEvent : GlobalEvent<DebugEvent>, IDisposable {
 *		public int VerbosityLevel;
 *	}
 */

/* Example use case 
 *	using (var e = DebugEvent.Get()) { //Grab ownership of event instance
 *		e.VerbosityLevel = 3;
 *		e.Description = "Something happened!";
 *	} //Event get's fired on dispose
 */
