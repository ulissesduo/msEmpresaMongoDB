using Microsoft.Extensions.Options;
using MongoDB.Driver;
using msEmpresaMongoDB.Entity;
using System.ComponentModel;
using System.Threading.Tasks;

namespace msEmpresaMongoDB.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly IMongoCollection<Empresa> _empresaCollection;

        public EmpresaRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _empresaCollection = database.GetCollection<Empresa>(settings.Value.EmpresaCollectionName);
        }

        public async Task<Empresa> atualizarEmpresa(string id, Empresa empresa)
        {
            var result = await _empresaCollection.ReplaceOneAsync(x=>x.Id == id, empresa);
            if (result.MatchedCount == 0) return null;
            return empresa;
        }

        public async Task<Empresa> buscaEmpresa(string id)
        {
            return await _empresaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Empresa> criarEmpresa(Empresa empresa)
        {
            await _empresaCollection.InsertOneAsync(empresa);
            return empresa;

        }

        public async Task<bool> deleteEmpresa(string id)
        {
            var result = await _empresaCollection.DeleteOneAsync(x=>x.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<List<Empresa>> listaEmpresas()
        {
            return await _empresaCollection.Find(_ => true).ToListAsync();
        }
    }
}
