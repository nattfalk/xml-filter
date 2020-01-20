using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace xml_filter
{
    class Program
    {
        static void PrintUsage()
        {
            Console.WriteLine(
                "Command-line tool for filtering XML files using XPath.\n"
                + "Copyright (C)) 2019 Michael Nattfalk"
                + "\n"
                + "Usage: xml-filter [in-file] [out-file] [base-node] [xpath-filter]\n"
                + "\n"
                + "in-file:\n"
                + "  The path to the XML file to filter.\n"
                + "out-file:\n"
                + "  The path for saving the filtered XML file.\n"
                + "base-node:\n"
                + "  The base node to apply the filtering on.\n"
                + "xpath-filter:\n"
                + "  XPath filter (added to base-node) used to filter the nodes.\n");
        }

        static void Main(string[] args)
        {

            if (args.Length != 4)
            {
                Console.WriteLine("Wrong arguments\n");

                PrintUsage();
                return;
            }

            string xmlFile = args[0];
            string outFile = args[1];
            string baseNode = args[2];
            string xPathFilter = args[3];

            XDocument doc = null;
            try
            {
                doc = XDocument.Load(xmlFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            int totalElementCount = 0;
            XElement[] filteredElements = null;
            try
            {
                totalElementCount = doc.XPathSelectElements(baseNode).Count();
                filteredElements = doc.XPathSelectElements(baseNode + xPathFilter).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.Write($"Filter resulted in {filteredElements.Length} of {totalElementCount} elements. Continue (y/n)? ");
            ConsoleKeyInfo cki = Console.ReadKey();
            if (cki.KeyChar != 'y')
            {
                Console.WriteLine();
                return;
            }
            Console.WriteLine();

            IEnumerable<XElement> elementsToRemove = null;
            try
            {
                elementsToRemove = doc.XPathSelectElements(baseNode).Except(filteredElements);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine($"Removing {elementsToRemove.Count()} elements.");
            elementsToRemove.Remove();

            if (File.Exists(outFile))
            {
                Console.Write($"File '{outFile} already exists? Overwrite (y/n)? ");
                cki = Console.ReadKey();
                if (cki.KeyChar != 'y')
                {
                    Console.WriteLine();
                    return;
                }
                Console.WriteLine();
            }

            try
            {
                doc.Save(outFile);
                Console.WriteLine($"File '{outFile} saved!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
