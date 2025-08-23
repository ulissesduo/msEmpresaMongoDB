using msEmpresaMongoDB.Entity;

namespace msEmpresaMongoDB.Service
{
    public interface IEmpresaService
    {
        Task<List<Empresa>> GetAllEmpresa();
        Task<Empresa> GetEmpresaById(string id);
        Task<Empresa> CreateEmpresa(Empresa product);
        Task<Empresa> UpdateEmpresa(string id, Empresa product);
        Task<bool> DeleteEmpresa(string id);
    }
}
