using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace msEmpresaMongoDB.Entity
{
    public class Empresa
    {
        [BsonRepresentation(BsonType.ObjectId)] // store as ObjectId in Mongo, represent as string in C#
        public string Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string SetorAtividade { get; set; }

        public Empresa() { }

        public Empresa(string id, string nome, string cnpj, string setorAtividade)
        {
            Id = id;
            Nome = nome;
            CNPJ = cnpj;
            SetorAtividade = setorAtividade;
        }
    }
}
