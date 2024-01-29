using AutoMapper;
using BM_API.DTOs.ToDo;
using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.Design;

namespace BM_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public ToDoController(IToDoRepository toDoRepository, ICompanyRepository companyRepository, IMapper mapper)
        {
            _toDoRepository = toDoRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpPost("add-to-do")]
        public async Task<IActionResult> AddToDo(ToDo toDo)
        {
            try
            {
                if(toDo == null)
                {
                    return BadRequest("Input data is null");
                }
                Company company = await _companyRepository.GetCompanyByIdAsync(toDo.CompanyId);
                if(company == null)
                {
                    return NotFound("Company not found");
                }
                ToDo toDoAdd = new ToDo
                {
                    Id = Guid.NewGuid(),
                    Text = toDo.Text,
                    StartDate = toDo.StartDate,
                    EndDate = toDo.EndDate,
                    Description = toDo.Description,
                    CompanyId = toDo.CompanyId,
                    RecurrenceRule = toDo.RecurrenceRule,
                    AllDay = toDo.AllDay,
                };
                
                _toDoRepository.Add(toDoAdd);
                if(await _toDoRepository.SaveChangesAsync())
                {
                    return Ok(toDoAdd);
                }
                return BadRequest("Db fail");
            }
            catch (Exception)
            {
                return BadRequest("Db fail");
            }
        }
        [HttpGet("get-company-to-do/{companyId}")]
        public async Task<IActionResult> GetCompanyToDo(Guid companyId)
        {
            try
            {
                ICollection<ToDo> toDosDTO=new List<ToDo>();
                if (companyId.Equals(Guid.Empty))
                {
                    return BadRequest("Company id is null");
                }
                Company company = await _companyRepository.GetCompanyByIdAsync(companyId);
                if (company == null)
                {
                    return NotFound("Company not found");
                }
                ICollection<ToDo> toDos = await _toDoRepository.GetToDosByCompanyIdAsync(companyId);
                if (toDos.Count <= 0)
                {
                    return NotFound("To dos not found");
                }
                foreach (ToDo toDo in toDos)
                {
                    var toDoDTO=_mapper.Map<ToDo>(toDo);
                    toDosDTO.Add(toDoDTO);
                }
                return Ok(toDosDTO);
            }
            catch (Exception)
            {
                return BadRequest("Db fail");
            }
        }
        [HttpGet("get-to-do-by-id/{id}")]
        public async Task<IActionResult> GetToDoById(Guid id)
        {
            try
            {
                if(id.Equals(Guid.Empty))
                {
                    return BadRequest("Id is null");
                }
                ToDo toDo=await _toDoRepository.GetToDoByIdAsync(id);
                if(toDo == null)
                {
                    return NotFound("To do not found");
                }
                return Ok(toDo);
            }
            catch (Exception)
            {
                return BadRequest("Db fail");
            }
        }
        [HttpPut("update-to-do/{toDoId}")]
        public async Task<IActionResult> UpdateToDo([FromRoute] Guid toDoId, [FromBody] ToDoDTO toDo)
        {
            try
            {
                if (toDoId.Equals(Guid.Empty))
                {
                    return BadRequest("To do id is null");
                }
                if (toDo == null)
                {
                    return BadRequest("To do is null");
                }
                ToDo foundToDo = await _toDoRepository.GetToDoByIdAsync(toDoId);
                if(foundToDo == null)
                {
                    return NotFound("To do not found");
                }
                foundToDo.Text = toDo.Text;
                foundToDo.Description = toDo.Description;
                foundToDo.StartDate = toDo.StartDate;
                foundToDo.EndDate = toDo.EndDate;
                foundToDo.RecurrenceRule = toDo.RecurrenceRule;
                foundToDo.AllDay = toDo.AllDay;
                _toDoRepository.Update(foundToDo);
                if(await _toDoRepository.SaveChangesAsync())
                {
                    return Ok(foundToDo);
                }
                return BadRequest("Db fail");
            }
            catch (Exception)
            {
                return BadRequest("Db fail");
            }
        }
        [HttpDelete("delete-to-do/{toDoId}")]
        public async Task<IActionResult> DeleteToDo(Guid toDoId)
        {
            try
            {
                if (toDoId.Equals(Guid.Empty))
                {
                    return BadRequest("To do id is null");
                }
                ToDo foundToDo = await _toDoRepository.GetToDoByIdAsync(toDoId);
                if(foundToDo == null)
                {
                    return NotFound("To do not found");
                }
                _toDoRepository.Delete(foundToDo);
                if(await _toDoRepository.SaveChangesAsync() )
                {
                    return Ok(foundToDo);
                }
                return BadRequest("Db fail");
            }
            catch (Exception)
            {
                return BadRequest("Db fail");
            }
        }
        [HttpGet("get-today-to-dos/{companyId}")]
        public async Task<IActionResult> GetTodayToDos(Guid companyId)
        {
            try
            {
                ICollection<ToDo> toDosDTO = new List<ToDo>();
                if (companyId.Equals(Guid.Empty))
                {
                    return BadRequest("Company id is null");
                }
                Company company = await _companyRepository.GetCompanyByIdAsync(companyId);
                if (company == null)
                {
                    return NotFound("Company not found");
                }
                ICollection<ToDo> toDos = await _toDoRepository.GetTodayToDosAsync(companyId);
                if (toDos.Count <= 0)
                {
                    return Ok("To dos not found");
                }
                foreach (ToDo toDo in toDos)
                {
                    var toDoDTO = _mapper.Map<ToDo>(toDo);
                    toDosDTO.Add(toDoDTO);
                }
                return Ok(toDosDTO);
            }
            catch (Exception)
            {
                return BadRequest("Db fail");
            }
        }
        [HttpGet("get-upcoming-to-dos/{companyId}")]
        public async Task<IActionResult> GetUpcomingToDos(Guid companyId)
        {
            try
            {
                ICollection<ToDo> toDosDTO = new List<ToDo>();
                if (companyId.Equals(Guid.Empty))
                {
                    return BadRequest("Company id is null");
                }
                Company company = await _companyRepository.GetCompanyByIdAsync(companyId);
                if (company == null)
                {
                    return NotFound("Company not found");
                }
                ICollection<ToDo> toDos = await _toDoRepository.GetUpcomingToDosAsync(companyId);
                if (toDos.Count <= 0)
                {
                    return Ok("To dos not found");
                }
                foreach (ToDo toDo in toDos)
                {
                    var toDoDTO = _mapper.Map<ToDo>(toDo);
                    toDosDTO.Add(toDoDTO);
                }
                return Ok(toDosDTO);
            }
            catch (Exception)
            {
                return BadRequest("Db fail");
            }
        }
    }
}
