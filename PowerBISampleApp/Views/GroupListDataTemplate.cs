using Microsoft.PowerBI.Api.V2.Models;
using Xamarin.Forms;

namespace PowerBISampleApp
{
    public class GroupListDataTemplate : DataTemplate
    {
        public GroupListDataTemplate() : base(CreateDataTemplate)
        {
        }

        static Grid CreateDataTemplate()
        {
            var image = new Image
            {
                Source = "PowerBILogo",
            };

            var titleLabel = new Label
            {
                FontSize = 16,
                VerticalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold
            };
            titleLabel.SetBinding(Label.TextProperty, nameof(Report.Name));

            var detailLabel = new Label
            {
                FontSize = 13,
                FontAttributes = FontAttributes.Italic
            };
            detailLabel.SetBinding(Label.TextProperty, nameof(Report.Id));

            var grid = new Grid
            {
                Margin = new Thickness(5),
                RowSpacing = 1,

                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(20, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(15, GridUnitType.Absolute) },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(50, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            grid.Children.Add(image, 0, 0);
            Grid.SetRowSpan(image, 2);

            grid.Children.Add(titleLabel, 1, 0);
            grid.Children.Add(detailLabel, 1, 1);

            return grid;
        }
    }
}
