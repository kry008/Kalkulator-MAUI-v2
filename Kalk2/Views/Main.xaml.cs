namespace Kalk2.Views;

public partial class Main : ContentPage
{
	public Main()
	{
		InitializeComponent();
	}

    private void btnMore(object sender, EventArgs e)
    {
        //get btn text
        var btn = (Button)sender;
        var btnText = btn.Text;
        //show modal box with it
        DisplayAlert("More", btnText, "OK");
    }

    private void btnResult(object sender, EventArgs e)
    {

    }

    private void btnCalc(object sender, EventArgs e)
    {
        //get btn text
        var btn = (Button)sender;
        var btnText = btn.Text;
        //show modal box with it
        DisplayAlert("More", btnText, "OK");
    }

    private void btnInfo(object sender, EventArgs e)
    {

    }

    private void btnC(object sender, EventArgs e)
    {

    }

    private void showHistory(object sender, EventArgs e)
    {

    }
}