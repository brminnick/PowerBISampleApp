using System;
namespace PowerBISampleApp
{
	public class PowerBIIFramePage : BaseContentPage<PowerBIIFrameViewModel>
	{

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
