// See https://aka.ms/new-console-template for more information
using BankEmpManager;
using Newtonsoft.Json;

void AddEmployee()
{
    Console.Clear();
    string phone;
    while (true)
    {
        Console.Write("Добавление пользователя:\n\nНомер телефона(без +7): ");
        phone = "+7" + Console.ReadLine();
        if (phone.Length != 12)
        {
            Console.WriteLine("Некорректно введен номер телефона, попробуйте еще раз.");
        }
        else break;
    }

    bool added = false;
    for (int i = 0; i < Employee.Employees.Count; i++)
    {
        if (Employee.Employees[i].PhoneNumber == phone)
        {
            Console.WriteLine("Такой пользователь уже существует!\nНажмите любую клавишу");
            Console.ReadKey();
            AddEmployee();
            added = true;
        }
    }
    if (!added)
    {
        string post;
        while (true)
        {
            bool valid = false;
            Console.Write("\nДолжность(Manager/Consultant): ");
            post = Console.ReadLine();
            switch (post)
            {
                case "Manager":
                    valid = true;
                    break;
                case "Consultant":
                    valid = true;
                    break;
                default:
                    Console.WriteLine("Некорректно указана должность. Попробуйте снова.");
                    break;
            }
            if (valid) break;
        }
        Console.Write("\nПароль: ");
        string password = Console.ReadLine();
        Employee employee = new Employee(phone, password, post);
    }
}
bool AnswerListener(string answer)
{
    bool exit = false;
    if (answer == "Add") AddEmployee();
    else if (answer == "Exit") exit = true;
    else if (answer == "Show")
    {
        foreach (Employee emp in Employee.Employees)
        {
            Console.WriteLine($"{emp.PhoneNumber} - {emp.Post}");
        }
        Console.ReadKey();
    }
    Console.Clear();
    return exit;
}

string Decode(string code)
{
    string separator = Convert.ToByte('z').ToString(); ;
    string[] parsedCode = code.Split(new string[] { "2z57" }, StringSplitOptions.RemoveEmptyEntries);
    code = parsedCode[0];
    int lenght = Convert.ToInt32(parsedCode[1]);
    string result = null;
    for (int i = 0; i < lenght; i++)
    {
        separator += i.ToString() + Convert.ToByte('|').ToString();
        string lastSeparator;
        if (i != 0)
        {
            lastSeparator = Convert.ToByte('z').ToString() + (i - 1).ToString() + Convert.ToByte('|').ToString();
            parsedCode = code.Split(new string[] { lastSeparator, separator }, StringSplitOptions.None);
            result += Convert.ToChar(Convert.ToByte(parsedCode[1]));
        }
        else
        {
            parsedCode = code.Split(new string[] { separator }, StringSplitOptions.None);
            result += Convert.ToChar(Convert.ToByte(parsedCode[0]));
        }
        separator = Convert.ToByte('z').ToString();
    }
    return result;
}



Console.WriteLine("Bank employees manager\n");
if (!File.Exists("empdata.encrypt")) File.Create("empdata.encrypt").Close();
string serializedData = File.ReadAllText("empdata.encrypt");
string decodeData;
try
{
    decodeData = Decode(serializedData);
}
catch
{
    decodeData = "";
}
Employee.Employees = JsonConvert.DeserializeObject<List<Employee>>(decodeData) ?? new List<Employee>();
while (true)
{
    Console.Clear();
    Console.WriteLine("Bank employees manager\n");

    if (Employee.Employees.Count == 0)
        AddEmployee();
    else
    {
        Console.WriteLine("Чтобы добавить сотрудника введите \"Add\"\nЧтобы посмотреть список введите \"Show\"" +
            "\nЧтобы выйти и сохранить данные введите \"Exit\"\n");
        string answer = Console.ReadLine();
        if (AnswerListener(answer)) break;
    }
}

string jsonData = JsonConvert.SerializeObject(Employee.Employees);
string crypt = "";
string separator = Convert.ToByte('z').ToString();
for (int i = 0; i < jsonData.Length; i++)
{
        crypt += Convert.ToByte(jsonData[i]) + separator + i.ToString() + Convert.ToByte('|').ToString(); ;
    if (i == jsonData.Length - 1) crypt += "2z57" + jsonData.Length.ToString();
}
File.WriteAllText("empdata.encrypt", crypt);
Console.ReadKey();

