namespace Kalk2.Views;

public partial class Main : ContentPage
{
    string equation = "";
    string result = "";
    bool isOperator = false;
    bool isDot = false;
    bool isBracket = false;
    int operatorTab = 0;
    static char[] operators1 = { '+', '-', '*', '/', '%' };
    static char[] operators2 = { '+', '-', '*', ')', '(' };
    char[][] operators = { operators1, operators2 };
    bool flagError = false;

    public Main()
    {
        InitializeComponent();
    }
    public Main(string equation, string result)
    {
        InitializeComponent();
        this.equation = equation;
        this.result = result;
        
    }
    public double Evaluate(string expression)
    {
        try
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }
        catch(Exception e)
        {
            flagError = true;
            return -1;
        }
    }

    private void btnMore(object sender, EventArgs e)
    {
        Button b1 = btn1;
        Button b2 = btn2;
        Button b3 = btn3;
        Button b4 = btn4;
        Button b5 = btn5;
        if(operatorTab == 0)
        {
            b1.Text = operators[1][0].ToString();
            b2.Text = operators[1][1].ToString();
            b3.Text = operators[1][2].ToString();
            b4.Text = operators[1][3].ToString();
            b5.Text = operators[1][4].ToString();
            operatorTab = 1;
        }
        else
        {
            b1.Text = operators[0][0].ToString();
            b2.Text = operators[0][1].ToString();
            b3.Text = operators[0][2].ToString();
            b4.Text = operators[0][3].ToString();
            b5.Text = operators[0][4].ToString();
            operatorTab = 0;
        }

    }

    private void btnResult(object sender, EventArgs e)
    {
        if (isDot)
        {
            equation += "0";
        }
        if (isOperator)
        {
            DisplayAlert("Error", "You can't end with an operator", "OK");
        }
        else
        {
            if (isBracket)
            {
                DisplayAlert("Error", "You need to close the bracket", "OK");
            }
            else
            {
                txtEquation.Text = equation;
                result = Evaluate(equation).ToString();
                if (flagError)
                {
                    txtField.Text = "Error";
                }
                else
                {
                    txtField.Text = result;
                    equation = result;
                }
            }
        }
        
    }

    private void btnCalc(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var btnText = btn.Text;
        if (btnText == "1" || btnText == "2" || btnText == "3" || btnText == "4" || btnText == "5" || btnText == "6" || btnText == "7" || btnText == "8" || btnText == "9" || btnText == "0")
        {
            isOperator = false;
            isDot = false;
            equation += btnText;
        }
        if (btnText == "+" || btnText == "-" || btnText == "*" || btnText == "/" || btnText == "%")
        {
            if (isOperator == false)
            {
                isOperator = true;
                isDot = false;
                equation += btnText;
            }
        }
        if (btnText == ".")
        {
            if (isDot == false)
            {
                isDot = true;
                isOperator = false;
                equation += btnText;
            }
        }
        if (btnText == "C")
        {
            isOperator = false;
            isDot = false;
            equation = "";
            result = "";
        }
        if (btnText == "(")
        {
            if (isDot == false && isOperator == true && isBracket == false)
            {
                isDot = false;
                isOperator = false;
                equation += btnText;
                isBracket = true;
            }
        }
        if (btnText == ")")
        {
            if (isDot == false && isOperator == false && isBracket == true)
            {
                isDot = false;
                isOperator = false;
                equation += btnText;
                isBracket = false;
            }
        }
        txtField.Text = equation;

    }

    private async void btnInfo(object sender, EventArgs e)
    {
        try
        {
            Uri uri = new Uri("https://github.com/kry008/Kalkulator-MAUI-v2");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "There was an error", "Ok");
        }
    }

    private void btnC(object sender, EventArgs e)
    {
        isOperator = false;
        isDot = false;
        isBracket = false;
        
        equation = "";
        result = "";
        txtField.Text = equation;
        txtField.Text = result;

    }

    private void showHistory(object sender, EventArgs e)
    {
        Navigation.PushAsync(new History());
    }
}