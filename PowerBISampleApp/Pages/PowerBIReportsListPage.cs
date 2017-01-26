using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class PowerBIReportsListPage : BaseContentPage<PowerBIReportsListViewModel>
	{
		#region Constructors
		public PowerBIReportsListPage()
		{
			var groupListView = new ListView
			{
				ItemTemplate = new DataTemplate(typeof(GroupListImageCell)),
				SeparatorVisibility = SeparatorVisibility.None
			};
			groupListView.SetBinding<PowerBIReportsListViewModel>(ListView.ItemsSourceProperty, vm => vm.VisibleReportsListData);
			groupListView.ItemTapped += async (sender, e) =>
			{
				groupListView.SelectedItem = null;

				var selectedReportsModel = e.Item as ReportsModel;

				await Navigation.PushAsync(new PowerBIWebViewPage(selectedReportsModel?.WebUrl));
			};

			Title = "Reports List";

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
