using System;
using Foundation;

namespace HueWatchSample.iPhoneWatchKitExtension
{
	public class NSObjectWrapper<T> : NSObject
	{
		public T Context;

		public NSObjectWrapper (T obj) : base()
		{
			Context = obj;
		}
	}
}

