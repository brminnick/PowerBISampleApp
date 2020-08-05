using Microsoft.PowerBI.Api.Models;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using static Xamarin.Forms.Markup.GridRowsColumns;

namespace PowerBISampleApp
{
    public class GroupListDataTemplate : DataTemplate
    {
        public GroupListDataTemplate() : base(CreateDataTemplate)
        {
        }

        enum Row { Title, Description }
        enum Column { Image, Text }

        static Grid CreateDataTemplate() => new Grid
        {
            Margin = new Thickness(5),
            RowSpacing = 1,

            RowDefinitions = Rows.Define(
                (Row.Title, new GridLength(20, GridUnitType.Absolute)),
                (Row.Description, new GridLength(15, GridUnitType.Absolute))),

            ColumnDefinitions = Columns.Define(
                (Column.Image, new GridLength(50, GridUnitType.Absolute)),
                (Column.Text, new GridLength(1, GridUnitType.Star))),

            Children =
            {
                new PowerBiImage().Row(Row.Title).Column(Column.Image).RowSpan(All<Row>()),

                new PowerBiLabel(FontAttributes.Bold,16){ VerticalTextAlignment = TextAlignment.Start }.Row(Row.Title).Column(Column.Text)
                    .Bind(Label.TextProperty, nameof(Report.Name)),

                new PowerBiLabel(FontAttributes.Italic,13).Row(Row.Description).Column(Column.Text)
                    .Bind(Label.TextProperty, nameof(Report.Id))
            }
        };

        class PowerBiImage : Image
        {
            public PowerBiImage() => Source = "PowerBILogo";
        }

        class PowerBiLabel : Label
        {
            public PowerBiLabel(in FontAttributes fontAttributes, in int fontSize) =>
                (FontAttributes, FontSize) = (fontAttributes, fontSize);
        }
    }
}
