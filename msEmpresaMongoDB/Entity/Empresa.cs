using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace msEmpresaMongoDB.Entity
{
    public class Empresa
    {
        [BsonRepresentation(BsonType.ObjectId)] // store as ObjectId in Mongo, represent as string in C#
        public string Id { get; set; }
        public string NomeEmpresa { get; set; }
        public string CNPJ { get; set; }
        public string SetorAtividade { get; set; }

        public Empresa() { }

        public Empresa(string id, string nomeEmpresa, string cnpj, string setorAtividade)
        {
            Id = id;
            NomeEmpresa = nomeEmpresa;
            CNPJ = cnpj;
            SetorAtividade = setorAtividade;
        }
    }
}
