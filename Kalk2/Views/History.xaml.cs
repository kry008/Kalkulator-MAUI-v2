namespace Kalk2.Views;

public partial class History : ContentPage
{
    string path = Path.Combine(FileSystem.AppDataDirectory, "calc");

    public History()
	{
		InitializeComponent();
		showHistory();
    }
    protected override void OnAppearing()
    {
        showHistory();
    }

    private void btnClaculatorPage(object sender, EventArgs e)
    {

    }

	private void showHistory()
    {
        Layout.Children.Clear();

        if (path.Length == 0 || !File.Exists(path))
        {
            if (!File.Exists(path))
            {
                btnClear.IsVisible = false;
                Label label = new Label
                {
                    Text = "No calculation history",
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                Layout.Children.Add(label);
            }
        }
        else
        {
            btnClear.IsVisible = true;
            string[] lines = File.ReadAllLines(path);
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                string[] parts = lines[i].Split(";");
                Button button = new Button
                {
                    Text = $"{parts[0]} = {parts[1]}",
                    Margin = new Thickness(5),
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                button.Clicked += Button_History;
                Layout.Children.Add(button);
            }
        }
    }
    private void Button_History(object sender, EventArgs e)
    {
        string[] parts = ((Button)sender).Text.Split('=');
        Navigation.PushAsync(new Main(parts[0], parts[1]));
    }

    private void btnClearF(object sender, EventArgs e)
    {
        File.Delete(path);
        showHistory();
    }
}