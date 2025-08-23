using msEmpresaMongoDB.Entity;
using msEmpresaMongoDB.Repository;

namespace msEmpresaMongoDB.Service
{
    public class EmpresaService : IEmpresaService
    {
        public readonly IEmpresaRepository _repository;

        public EmpresaService(IEmpresaRepository repository) 
        {
            _repository = repository;
        }

        public Task<Empresa> CreateEmpresa(Empresa empresa)
        {
            if (empresa == null) 
                throw new ArgumentNullException(nameof(empresa), "Empresa não pode ser nula.");

            if (string.IsNullOrWhiteSpace(empresa.NomeEmpresa))
                throw new ArgumentException("Nome da empresa é obrigatório.", nameof(empresa));

            return _repository.criarEmpresa(empresa);
        }

        public async Task<bool> DeleteEmpresa(string id)
        {
            var getbyid = await _repository.buscaEmpresa(id);
            if (getbyid == null) return false;
            return await _repository.deleteEmpresa(id);
        }

        public Task<List<Empresa>> GetAllEmpresa()
        {
            return _repository.listaEmpresas();


        }

        public Task<Empresa> GetEmpresaById(string id)
        {
            return _repository.buscaEmpresa(id);
        }

        public async Task<Empresa> UpdateEmpresa(string id, Empresa empresa)
        {
            if (empresa == null) { throw new ArgumentException(nameof(empresa)); }

            var existing = await _repository.buscaEmpresa(id);
            if (existing == null) return null;
            return await _repository.atualizarEmpresa(id, empresa);

        }
    }
}
