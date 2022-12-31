namespace Kalk2.Views;

using NCalc;

public partial class Main : ContentPage
{
    string equation = "";
    string result = "";
    bool isOperator = false;
    bool isDot = false;
    bool isBracket = false;
    bool waitingForOperator = false;
    bool lastNumber = false;
    int operatorTab = 0;
    static string[] operators1 = { "+", "-", "*", "/", "%" };
    static string[] operators2 = { "Log", "Sin", "^", ")", "(" };
    string[][] operators = { operators1, operators2 };
    bool flagError = false;
    public Main()
    {
        InitializeComponent();
    }
    public Main(string equation, string result)
    {
        InitializeComponent();
        txtField.Text = result;
        this.equation = result;
        txtEquation.Text = equation;
    }
    public double Evaluate(string expression)
    {
        try
        {
            Expression e = new Expression(expression);

            // Evaluate the expression and return the result
            var a = Convert.ToDouble(e.Evaluate());

            /*
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            var a = double.Parse((string)row["expression"]);*/
            if (a == double.PositiveInfinity)
            {
                flagError = true;
                return 0;
            }
            if (a == double.NegativeInfinity)
            {
                flagError = true;
                return 0;
            }
            if (a != a)
            {
                flagError = true;
                return -2;
            }
            return a;
        }
        catch(Exception e)
        {
            DisplayAlert("Error", e.Message, "OK");
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
                    if (result.Contains(",") || result.Contains(" "))
                    {
                        result = result.Replace(",", ".");
                        result = result.Replace(" ", "");
                    }
                    string path = Path.Combine(FileSystem.AppDataDirectory, "calc");
                    if (!File.Exists(path))
                    {
                        File.Create(path);
                    }
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(equation + ";" + result);
                    }
                    txtField.Text = result;
                    equation = result;
                    isOperator = false;
                    lastNumber = true;
                    waitingForOperator = true;
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
            if (!waitingForOperator)
            {
                isOperator = false;
                equation += btnText;
                lastNumber = true;
            }
        }
        if ((btnText == "+" || btnText == "-" || btnText == "*" || btnText == "/" || btnText == "%") && lastNumber == true)
        {
            if (isOperator == false)
            {
                isOperator = true;
                isDot = false;
                equation += btnText;
                waitingForOperator = false;
                lastNumber = false;
            }
        }
        if (btnText == ".")
        {
            if (isDot == false && isOperator == false && !waitingForOperator && lastNumber == true)
            {
                isDot = true;
                equation += btnText;
                lastNumber = false;
            }
        }
        if (btnText == "C")
        {
            isOperator = false;
            isDot = false;
            waitingForOperator = false;
            equation = "";
            result = "";
        }
        if (btnText == "^" )
        {
            if (isOperator == false && lastNumber == true)
            {
                isOperator = true;
                isDot = false;
                equation = "Pow(" + equation + ",";
                isBracket = true;
                waitingForOperator = false;

                lastNumber = false;
            }
        }
        if ( btnText == "Log")
        {
            if (isOperator == false && lastNumber == true)
            {
                isOperator = true;
                isDot = false;
                equation = btnText + "(" + equation + ",";
                isBracket = true;
                waitingForOperator = false;

                lastNumber = false;
            }

        }
        if (btnText == "Sin")
        {
            if (isOperator == false && lastNumber == true)
            {
                isOperator = false;
                isDot = false;
                isBracket = false;
                equation = btnText + "(" + equation + ")";
                waitingForOperator = true;
                lastNumber = true;
            }
        }
        if (btnText == "(")
        {
            if (isDot == false && isOperator == true && isBracket == false && !waitingForOperator && lastNumber == false)
            {
                equation += btnText;
                isOperator = false;
                isDot = false;
                isBracket = false;
                waitingForOperator = false;
                lastNumber = false;
            }
        }
        if (btnText == ")")
        {
            if (isDot == false && isOperator == false && isBracket == true && !waitingForOperator && lastNumber == true)
            {
                isDot = false;
                isOperator = false;
                equation += btnText;
                isBracket = false;
                lastNumber = false;
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
            await DisplayAlert("Error", "There was an error\n" + ex.Message, "Ok");
        }
    }
    private void btnC(object sender, EventArgs e)
    {
        isOperator = false;
        isDot = false;
        isBracket = false;
        waitingForOperator = false;
        lastNumber = false;

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