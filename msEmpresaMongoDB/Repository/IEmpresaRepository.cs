using msEmpresaMongoDB.Entity;

namespace msEmpresaMongoDB.Repository
{
    public interface IEmpresaRepository
    {
        Task<List<Empresa>> listaEmpresas();
        Task<Empresa> buscaEmpresa(string id);
        Task<Empresa> criarEmpresa(Empresa empresa);
        Task<Empresa> atualizarEmpresa(string id, Empresa empresa);
        Task<bool> deleteEmpresa(string id);
    }
}
