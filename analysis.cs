static string Func1(string input, string elementName, string attrName)
{
    string[] lines = System.IO.File.ReadAllLines(input); // Необходима провека пути на null; существование и доступность. Стоит добавить обработку исключений. 
    string result = null; // Лучше использовать StringBuilder.

    foreach (var line in lines)
    {
        var startElEndex = line.IndexOf(elementName); // Стоит добавить проверку аргумента на null.

        if (startElEndex != -1)
        {
            if (line[startElEndex - 1] == '<')
            {
                var endElIndex = line.IndexOf('>', startElEndex - 1);
                var attrStartIndex = line.IndexOf(attrName, startElEndex, endElIndex - startElEndex + 1);

                if (attrStartIndex != -1)
                {
                    int valueStartIndex = attrStartIndex + attrName.Length + 2; // Не учитываются возможные пробелы между " и =.

                    while (line[valueStartIndex] != '"')
                    {
                        result += line[valueStartIndex]; // Лучше использовать StringBuilder.
                        valueStartIndex++;
                    }

                    break;
                }
            }
        }
    }

    return result;
}

static string Func2(string input, string elementName, string attrName)
{
    string[] lines = new string[] { };

    bool isFileExists = File.Exists(input);

    if (!isFileExists || String.IsNullOrWhiteSpace(elementName) || String.IsNullOrWhiteSpace(attrName))
        return String.Empty;

    try
    {
        lines = System.IO.File.ReadAllLines(input);
    }
    catch (Exception ex)
    {
        // Код обработки исключений.

        return String.Empty;
    }

    StringBuilder result = new StringBuilder();

    foreach (var line in lines)
    {
        var startElEndex = line.IndexOf(elementName);

        if (startElEndex != -1)
        {
            if (line[startElEndex - 1] == '<')
            {
                var endElIndex = line.IndexOf('>', startElEndex - 1);
                var attrStartIndex = line.IndexOf(attrName, startElEndex, endElIndex - startElEndex + 1);

                if (attrStartIndex != -1)
                {
                    int valueStartIndex = line.IndexOf('"', attrStartIndex) + 1;

                    while (line[valueStartIndex] != '"')
                    {
                        result.Append(line[valueStartIndex]);
                        valueStartIndex++;
                    }

                    break;
                }
            }
        }
    }

    return result.ToString();
}