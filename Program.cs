using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class TransferBatch
{
    public static void Main()
    {
        try
        {
            Console.WriteLine("########################################");
            Console.WriteLine("#                                      #");
            Console.WriteLine("#  Please enter the path to the        #");
            Console.WriteLine("#  transfers file:                     #");
            Console.WriteLine("#                                      #");
            Console.WriteLine("########################################");

            string filePath = Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("No file path provided.");
                return;
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found: " + filePath);
                return;
            }

            var accountTransfers = new Dictionary<string, List<double>>();
            var outputLines = new List<string>();

            ProcessFile(filePath, accountTransfers);

            CalculateCommissions(accountTransfers, outputLines);

            Console.WriteLine();
            Console.WriteLine($"===> @TransferBatch {filePath} ");

            foreach (var line in outputLines)
            {
                Console.WriteLine(line);
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("The file was not found:");
            Console.WriteLine(ex.Message);
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Formatting error occurred while processing the file:");
            Console.WriteLine(ex.Message);
        }
        catch (IOException ex)
        {
            Console.WriteLine("Input/output error while accessing the file:");
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred:");
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.ReadLine();
        }
    }

    private static void ProcessFile(string filePath, Dictionary<string, List<double>> accountTransfers)
    {
        var cultureInfo = CultureInfo.InvariantCulture;

        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split(',');
            if (parts.Length != 3)
            {
                Console.WriteLine("Invalid line in file: " + line);
                continue;
            }

            var accountId = parts[0];

            if (!double.TryParse(parts[2], NumberStyles.Any, cultureInfo, out var transferAmount))
            {
                Console.WriteLine($"Invalid transfer amount: {parts[2]}");
                continue;
            }

            if (!accountTransfers.ContainsKey(accountId))
                accountTransfers[accountId] = new List<double>();

            accountTransfers[accountId].Add(transferAmount);
        }
    }

    private static void CalculateCommissions(Dictionary<string, List<double>> accountTransfers, List<string> outputLines)
    {
        foreach (var account in accountTransfers)
        {
            var transfers = account.Value;
            if (transfers.Count == 0)
                continue;

            var maxTransfer = transfers.Max();

            double totalCommission = CalculateCommission(transfers, maxTransfer);

            outputLines.Add($"{account.Key},{totalCommission:F0}");
        }
    }

    private static double CalculateCommission(List<double> transfers, double maxTransfer)
    {
        if (transfers.Count == 1)
        {
            return transfers[0] * 0.10;
        }

        return transfers.Where(t => t != maxTransfer).Sum(t => t * 0.10);
    }
}
