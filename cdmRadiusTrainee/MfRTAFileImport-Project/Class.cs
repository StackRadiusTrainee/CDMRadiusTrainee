

public class Class
using System;
using System.Text.Json;
using DbfLibrary;

namespacenamespace MfRTAFileImport_Project
{
        class Program
        {
            static void Main(string[] args)
            {
                string filename = "";
                ProcessDbfFile(filename);
            }

            static void ProcessDbfFile(string  )
            {
                using (DbfTable table = new DbfTable())
                {
                    foreach (DbfRecord record in table.Records)
                    {
                        string entityName = "  ";
                        Dictionary<string, object> attributes = new Dictionary<string, object>();

                        foreach (DbfField field in table.Fields)
                        {
                            string attributeName = MapFieldToAttribute(field.Name);
                            object attributeValue = record[field.Name];
                            attributes.Add(attributeName, attributeValue);



                        }


                    }
                }
