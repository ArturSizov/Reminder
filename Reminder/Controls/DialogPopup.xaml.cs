using DevExpress.Maui.Controls;

namespace Reminder.Controls;

public partial class DialogPopup : DXPopup
{
    #region Title
    public string Title { get => (string)GetValue(TitleProperty); set { SetValue(TitleProperty, value); } }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(DialogPopup));
    #endregion

    #region Content
    public static readonly BindableProperty CustomContentProperty = BindableProperty.Create(nameof(CustomContent), typeof(object), typeof(DialogPopup), null);
    public static readonly BindableProperty ContentTemplateProperty = BindableProperty.Create(nameof(ContentTemplate), typeof(DataTemplate), typeof(DialogPopup), null);

    public object CustomContent
    {
        get => GetValue(CustomContentProperty);
        set => SetValue(CustomContentProperty, value);
    }

    public DataTemplate ContentTemplate
    {
        get => (DataTemplate)GetValue(ContentTemplateProperty);
        set => SetValue(ContentTemplateProperty, value);
    }

    #endregion

    public DialogPopup()
	{
		InitializeComponent();
	}

    private void DXButton_Clicked(object sender, EventArgs e)
    {
        self.IsOpen = false;
    }
}