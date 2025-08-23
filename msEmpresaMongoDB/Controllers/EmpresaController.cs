using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using msEmpresaMongoDB.DTO;
using msEmpresaMongoDB.Entity;
using msEmpresaMongoDB.Repository;
using msEmpresaMongoDB.Service;

namespace msEmpresaMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : Controller
    {

        private readonly IEmpresaService _empresaService;
        private readonly IMapper _mapper;

        public EmpresaController(IEmpresaService empresaService, IMapper mapper) 
        {
            _mapper = mapper;
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpresaResponseDTO>>> GetAll() 
        {
            var list = await _empresaService.GetAllEmpresa();
            var response = _mapper.Map<List<EmpresaResponseDTO>>(list);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaResponseDTO>> GetById(string id) 
        {
            var item = await _empresaService.GetEmpresaById(id);
            if (item == null) return NotFound();
            var response = _mapper.Map<EmpresaResponseDTO>(item);
            if (item == null) return NotFound();

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<EmpresaResponseDTO>> Create([FromBody] EmpresaRequestDTO empresadto) 
        {
            var entity = _mapper.Map<Empresa>(empresadto);
            var created = await _empresaService.CreateEmpresa(entity);
            var response = _mapper.Map<EmpresaResponseDTO>(created);
            return Ok(response);            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmpresaResponseDTO>> Delete(string id) 
        {
            var item = await _empresaService.GetEmpresaById(id);
            if (item == null) return NotFound();
            var entity = _mapper.Map<EmpresaResponseDTO>(item);
            var deleted = await _empresaService.DeleteEmpresa(id);
            if(!deleted) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmpresaResponseDTO>> Update(string id, [FromBody] EmpresaRequestDTO dto) 
        {
            var item = await _empresaService.GetEmpresaById(id);
            if (item == null) return NotFound();
            var entity = _mapper.Map(dto, item);
            var updated = await _empresaService.UpdateEmpresa(id, item);
            var response = _mapper.Map<EmpresaResponseDTO>(updated);
            return Ok(response);
        
        
        }
    
    }
}
