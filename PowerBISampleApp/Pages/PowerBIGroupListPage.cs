using System;
using Xamarin.Forms;
namespace PowerBISampleApp
{
	public class PowerBIGroupListPage : BaseContentPage<PowerBIGroupListViewModel>
	{
		#region Constructors
		public PowerBIGroupListPage()
		{
			var groupListView = new ListView
			{
				Margin = new Thickness(0, 10, 0, 0),
				ItemTemplate = new DataTemplate(typeof(GroupListImageCell)),
				SeparatorVisibility = SeparatorVisibility.None
			};
			groupListView.SetBinding<PowerBIGroupListViewModel>(ListView.ItemsSourceProperty, vm => vm.VisibleGroupValueData);
			groupListView.ItemSelected += (sender, e) => groupListView.SelectedItem = null;


			Content = groupListView;
		}
		#endregion

		#region Methods
		protected override void SubscribeEventHandlers()
		{
			AreEventHandlersSubscribed = true;
		}

		protected override void UnsubscribeEventHandlers()
		{
			AreEventHandlersSubscribed = false;
		}
		#endregion
	}
}
