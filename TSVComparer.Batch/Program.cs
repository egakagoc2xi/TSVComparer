using System;
using System.IO;
using TSVComparer.Batch.ViewModel;
using TSVComparer.Model;
using TSVComparer.ViewModel;

namespace TSVComparer.Batch
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                CompareInformation compareInformation = new CompareInformation();
                try
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        switch (args[i])
                        {
                            case "-s":
                                switch (args[i + 1])
                                {
                                    case "DSSTE": compareInformation.TypeOfFileComparisson = TypeOfFile.DSSTE; break;
                                    case "GCT": compareInformation.TypeOfFileComparisson = TypeOfFile.GCT; break;
                                    case "STB": compareInformation.TypeOfFileComparisson = TypeOfFile.STB; break;
                                    default:
                                        throw new Exception("Not Valid Source Argument:" + args[i + 1]);

                                }
                                i++;
                                break;
                            case "-if":
                                compareInformation.OriginalFolder = args[i + 1].Trim().ToLowerInvariant();
                                i++;
                                // Process arg 2
                                break;
                            case "-cf":
                                compareInformation.CompareFolder = args[i + 1].Trim().ToLowerInvariant();
                                i++;
                                // Process arg 3
                                break;
                            case "-of":
                                compareInformation.OutputFolder = args[i + 1].Trim().ToLowerInvariant();
                                i++;
                                // Process arg 3
                                break;
                            case "-to":
                                switch (args[i + 1])
                                {
                                    case "HTML": compareInformation.TypeOfOuputFile = TypeOfOutput.HTML; break;
                                    case "Excel": compareInformation.TypeOfOuputFile = TypeOfOutput.Excel; break;
                                    case "BBCode": compareInformation.TypeOfOuputFile = TypeOfOutput.BBCode; break;
                                    case "TSV": compareInformation.TypeOfOuputFile = TypeOfOutput.TSV; break;
                                    default:
                                        throw new Exception("Not Valid Output type Argument:" + args[i + 1]);

                                }
                                i++;
                                break;
                            case "--help":
                                System.Console.WriteLine("These are the parameter available for use:");
                                System.Console.WriteLine("-s: Type of source availables (DSSTE|GCT|STB)");
                                System.Console.WriteLine("-if (Optional): Path of the Original Folder - Default Folder: " + Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "original" + Path.DirectorySeparatorChar);
                                System.Console.WriteLine("-cf (Optional): Path of the Folder to Compare - Default Folder: " + Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "compare" + Path.DirectorySeparatorChar);
                                System.Console.WriteLine("-of (Optional): Path of the Ouput Folder - Default Folder: " + Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "ouput" + Path.DirectorySeparatorChar);
                                System.Console.WriteLine("-to: Type if Output (HTML|Excel|TSV)");
                                System.Console.WriteLine("Note: All Parameters and options are case sensitive");

                                compareInformation = null;

                                break;
                        }
                    }
                    if (compareInformation != null)
                    {


                        if (String.IsNullOrEmpty(compareInformation.OutputFolder)){
                            compareInformation.OutputFolder = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "output" + Path.DirectorySeparatorChar;
                            
                        }
                        if (String.IsNullOrEmpty(compareInformation.CompareFolder)) { 
                            compareInformation.CompareFolder = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "compare" + Path.DirectorySeparatorChar;
                        }

                        if (String.IsNullOrEmpty(compareInformation.OriginalFolder)) { 
                            compareInformation.OriginalFolder = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "original" + Path.DirectorySeparatorChar;
                        }

                        new ConsoleViewModel(String.Empty, String.Empty).BatchProcess(compareInformation);
                    }

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Invalid Argument Exception: " + ex.Message);
                    System.Console.WriteLine(ex.StackTrace);
                }
            }
            else {
                System.Console.WriteLine("Invalid Argument Exception: One or more Arguments were need");
            }
        }
    }
}
