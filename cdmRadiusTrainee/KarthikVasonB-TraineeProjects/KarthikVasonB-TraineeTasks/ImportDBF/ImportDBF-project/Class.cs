namespace ImportDBF_project
{
    public class Class
    {

        static void Main(string[] args)
        {
            string excelFilePath = "path/to/your/excel/file.xlsx"; string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES'"; using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open(); using (OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$]", connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        List<object> jsonData = new List<object>(); while (reader.Read())
                        {
                            var rowObjecn                                                                                                                                                                                                                                                   ew
                            {
                                Column1 = reader["Column1"],
                                Column2 = reader["Column2"],  

                            }


}
