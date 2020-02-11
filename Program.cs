using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using CommandLine;

namespace xml_filter
{
    class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input XML file")]
        public string InputFile { get; set; }

        [Option('o', "output", HelpText = "Output XML file")]
        public string OutputFile { get; set; }

        [Option('x', "xpath", Required = true, HelpText = "XPath filter (added to base-node) used to filter the nodes")]
        public string XPathFilter { get; set; }

        [Option('c', "contents", HelpText = "Displays contents from given number of elements after applied filter")]
        public int? Contents { get; set; }
    }

    class Program
    {
        static string GetNodePath(XElement element)
        {
            string path = string.Empty;

            path = $"/{element.Name}";
            var parent = element.Parent;
            while (parent != null)
            {
                path = $"/{parent.Name}{path}";
                parent = parent.Parent;
            }

            return path;
        }

        static void DoFilter(Options options)
        {
            XDocument doc = null;
            try
            {
                doc = XDocument.Load(options.InputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            XElement[] filteredElements = null;
            try
            {
                filteredElements = doc.XPathSelectElements(options.XPathFilter).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            if (filteredElements.Count() == 0)
            {
                Console.WriteLine("No elements matched the filter!");
                return;
            }

            string nodePath = GetNodePath(filteredElements.First());
#if DEBUG
            // Console.WriteLine($"xpath: {options.XPathFilter}");
            // Console.WriteLine($"node path: {nodePath}");
#endif
            int totalElementCount = doc.XPathSelectElements(nodePath).Count();
            Console.WriteLine($"Filter resulted in {filteredElements.Length} of totally {totalElementCount} elements.");

            if (!string.IsNullOrWhiteSpace(options.OutputFile))
            {
                doc.XPathSelectElements(nodePath).Except(filteredElements).Remove();

                if (!string.IsNullOrEmpty(options.OutputFile))
                {
                    if (File.Exists(options.OutputFile))
                    {
                        Console.Write($"File '{options.OutputFile}' already exists? Overwrite (y/n)? ");
                        ConsoleKeyInfo cki = Console.ReadKey();
                        if (cki.KeyChar != 'y')
                        {
                            Console.WriteLine();
                            return;
                        }
                        Console.WriteLine();
                    }

                    try
                    {
                        doc.Save(options.OutputFile);
                        Console.WriteLine($"'{options.OutputFile}' saved!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                if (options.Contents.HasValue)
                {
                    var elementsToDisplay = filteredElements.Take(options.Contents.Value);

                    Console.WriteLine();
                    string msg = $"Displaying contents of {elementsToDisplay.Count()} elements";
                    Console.WriteLine(msg);
                    Console.WriteLine(new string('-', msg.Length));
                    foreach (var element in elementsToDisplay)
                    {
                        Console.WriteLine(element.ToString());
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o => DoFilter(o));
        }
    }
}
