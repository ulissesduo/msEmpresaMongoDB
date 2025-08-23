namespace msEmpresaMongoDB.Entity
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string EmpresaCollectionName { get; set; } = "Empresa";
    }
}
