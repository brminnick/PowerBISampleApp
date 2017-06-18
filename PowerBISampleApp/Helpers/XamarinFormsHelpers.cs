using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public static class XamarinFormsHelpers
	{
        #region Fields
        static readonly object _locker = new object();
        #endregion

        #region Methods
        public static Task<T> BeginInvokeOnMainThreadAsync<T>(Func<T> a)
		{
			lock (_locker)
			{
				var tcs = new TaskCompletionSource<T>();
				
				Device.BeginInvokeOnMainThread(() =>
				{
					try
					{
						var result = a();
						tcs?.SetResult(result);
					}
					catch (Exception ex)
					{
						tcs?.SetException(ex);
						DebugHelpers.PrintException(ex);
					}
				});
				return tcs?.Task;
			}
		}

		public static void BeginInvokeOnMainThread(Action a)
		{
			lock (_locker)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					try
					{
						a?.Invoke();
					}
					catch (Exception ex)
					{
						DebugHelpers.PrintException(ex);
					}
				});
			}
		}
		#endregion
	}
}

