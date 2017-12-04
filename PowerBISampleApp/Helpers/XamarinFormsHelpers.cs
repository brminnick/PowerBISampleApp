using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public static class XamarinFormsHelpers
	{
        #region Methods
        public static Task<T> BeginInvokeOnMainThreadAsync<T>(Func<Task<T>> method)
        {
            var tcs = new TaskCompletionSource<T>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var task = method?.Invoke();
                    tcs?.SetResult(await task);
                }
                catch (Exception e)
                {
                    DebugHelpers.PrintException(e);
                    tcs?.SetException(e);
                }
            });

            return tcs?.Task;
        }

        public static Task<T> BeginInvokeOnMainThreadAsync<T>(Func<T> method)
        {
            var tcs = new TaskCompletionSource<T>();

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    var result = method.Invoke();

                    tcs?.SetResult(result);
                }
                catch (Exception e)
                {
                    DebugHelpers.PrintException(e);
                    tcs?.SetException(e);
                }
            });

            return tcs?.Task;
        }

        public static Task BeginInvokeOnMainThreadAsync(Func<Task> method)
        {
            var tcs = new TaskCompletionSource<object>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var task = method?.Invoke();
                    await task;

                    tcs?.SetResult(new object());
                }
                catch (Exception e)
                {
                    DebugHelpers.PrintException(e);
                    tcs?.SetException(e);
                }
            });

            return tcs?.Task;
        }

        public static void BeginInvokeOnMainThread(Action method)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    method?.Invoke();
                }
                catch (Exception e)
                {
                    DebugHelpers.PrintException(e);
                }
            });
        }

        public static void CompressAllLayouts(Layout<View> layout)
        {
            var childLayouts = GetChildLayouts(layout);

            foreach (var childLayout in childLayouts)
                CompressAllLayouts(childLayout);

            if (layout.BackgroundColor == default(Color) && !layout.GestureRecognizers.Any())
                CompressedLayout.SetIsHeadless(layout, true);
        }

        static IList<Layout<View>> GetChildLayouts(Layout<View> layout)
        {
            var childLayouts = layout?.Children?.OfType<Layout<View>>()?.ToList() ?? new List<Layout<View>>();

            var childContentViews = layout?.Children?.OfType<ContentView>()?.ToList() ?? new List<ContentView>();
            var childContentViewLayouts = childContentViews?.Where(x => x?.Content is Layout<View>)?.Select(x => x?.Content as Layout<View>)?.ToList() ?? new List<Layout<View>>();

            return childLayouts?.Concat(childContentViewLayouts)?.ToList() ?? new List<Layout<View>>();
        }
        #endregion
	}
}

