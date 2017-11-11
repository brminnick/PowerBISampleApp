using Xamarin.Forms;

namespace PowerBISampleApp
{
    public abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel, new()
    {
        #region Fields
        T _viewModel;
        #endregion

        #region Constructors
        protected BaseContentPage()
        {
            BindingContext = ViewModel;
            this.SetBinding(IsBusyProperty, nameof(BaseViewModel.IsInternetConnectionActive));
        }
        #endregion

        #region Properties
        protected T ViewModel => _viewModel ?? (_viewModel = new T());
        protected bool AreEventHandlersSubscribed { get; set; }
        #endregion

        #region Methods
        protected abstract void SubscribeEventHandlers();

        protected abstract void UnsubscribeEventHandlers();

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SubscribeEventHandlers();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            UnsubscribeEventHandlers();
        }
        #endregion
    }
}
