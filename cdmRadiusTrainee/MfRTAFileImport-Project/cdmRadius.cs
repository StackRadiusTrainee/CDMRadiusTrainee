

namespace MfRTAFileImport_project
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.CommonDataModel.ObjectModel.Cdm;
    using Microsoft.CommonDataModel.ObjectModel.Storage;
    using Microsoft.CommonDataModel.ObjectModel.Utilities;

    

    class Program
    {
        static async Task Main(string[] args)
        {
           

            var cdmCorpus = new CdmCorpusDefinition();

           
            cdmCorpus.SetEventCallback(new EventCallback
            {
                Invoke = (level, message) =>
                {
                    Console.WriteLine(message);
                }
            }, CdmStatusLevel.Warning);

           
            string pathFromExeToExampleRoot = "  ";

            
            cdmCorpus.Storage.Mount("local", new LocalAdapter(pathFromExeToExampleRoot + "     "));

           
            cdmCorpus.Storage.DefaultNamespace = "local";

            // Storage adapter pointing to the example public standards.
            // This is a fake 'cdm'; normally the CDM Standards adapter would be used to point at the real public standards.
            // Mount it as the 'cdm' device, not the default, so that we must use "cdm:<folder-path>" to get there.
            cdmCorpus.Storage.Mount("cdm", new LocalAdapter(pathFromExeToExampleRoot + "example-public-standards"));

            
            await ExploreManifest(cdmCorpus, "default.manifest.cdm.json");
        }

        static async Task ExploreManifest(CdmCorpusDefinition cdmCorpus, string manifestPath)
        {
            Console.WriteLine($"\nLoading manifest {manifestPath} ...");

            CdmManifestDefinition manifest = await cdmCorpus.FetchObjectAsync<CdmManifestDefinition>(manifestPath);

            if (manifest == null)
            {
                Console.WriteLine($"Unable to load manifest {manifestPath}. Please inspect error log for additional details.");
                return;
            }

            
   \

            while (true)
            {
                int index = 1;

                if (manifest.Entities.Count > 0)
                {
                    Console.WriteLine("List of all entities:");

                    foreach (var entDec in manifest.Entities)
                    {
                        
                        Console.Write("  " + index.ToString().PadRight(3));
                        Console.Write("  " + entDec.EntityName.PadRight(35));
                        Console.WriteLine("  " + entDec.EntityPath);
                        index++;
                    }
                }

                if (manifest.SubManifests.Count > 0)
                {
                    Console.WriteLine("List of all sub-manifests:");

                    foreach (var manifestDecl in manifest.SubManifests)
                    {
                        // Print sub-manifest declarations.
                        Console.Write("  " + index.ToString().PadRight(3));
                        Console.Write("  " + manifestDecl.ManifestName.PadRight(35));
                        Console.WriteLine("  " + manifestDecl.Definition);
                        index++;
                    }
                }

                