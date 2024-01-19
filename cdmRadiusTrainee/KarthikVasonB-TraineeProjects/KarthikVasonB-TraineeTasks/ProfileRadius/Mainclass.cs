using Microsoft.CommonDataModel.ObjectModel.Cdm;
using Microsoft.CommonDataModel.ObjectModel.Storage;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Intrinsics.Arm;

namespace KarthikVasonB_TraineeProjects.KarthikVasonB_TraineeTasks.ProfileRadius
{
    public class Mainclass
    {
        cdmoperation










        Define
        {

        }
        the path to your local CDM folder
       string localRoot = "<path to your local CDM folder>";

        // Mount the local storage adapter to the 'local' namespace
        cdmCorpus.Storage.Mount("local", new LocalAdapter(localRoot));

// Set the default namespace to 'local'
       cdmCorpus.Storage.DefaultNamespace = "local";
 
// Fetch the CDM manifest
       CdmManifestDefinition manifest = cdmCorpus.FetchObjectAsync<CdmManifestDefinition>
       ("local:/default.manifest.cdm.json").GetAwaiter().GetResult();

// Connect to SQL Server
       using (SqlConnection conn = new SqlConnection("<your connection string>")) { conn.Open();

// For each entity in the CDM, create a corresponding table in SQL Server
       foreach (CdmEntityDeclarationDefinition entityDeclaration in manifest.Entities)
   
{
// Fetch the entity
      CdmEntityDefinition entity = cdmCorpus.FetchObjectAsync<CdmEntityDefinition>
      (entityDeclaration.EntityPath, manifest).GetAwaiter().GetResult();

    // Start the CREATE TABLE command
    string createTableCommand = $"CREATE TABLE {entity.EntityName} (";


// For each attribute in the entity, add a column in the table
      foreach (CdmTypeAttributeDefinition attribute in entity.Attributes)

{
// Map the CDM data type to a SQL Server data type
      string sqlDataType = MapCdmToSqlDataType(attribute.DataType.NamedReference);
    createTableCommand += $"{attribute.Name} {sqlDataType}, ";
      }

// Remove the trailing comma and space, and add the closing parenthesis
createTableCommand = createTableCommand.TrimEnd(',', ' ') + ")";

// Execute the CREATE TABLE command
using (SqlCommand cmd = new SqlCommand(createTableCommand, conn))
{
    cmd.ExecuteNonQuery();
}
      }
      }
      }

    }
}
