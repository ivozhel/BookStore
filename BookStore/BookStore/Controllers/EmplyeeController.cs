using BookStore.BL.Interfaces;
using BookStore.Models.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("[controller]")]
    public class EmplyeeController : ControllerBase
    {
        private readonly IEmplyeeService _emplyeeService;
        public EmplyeeController(IEmplyeeService emplyeeService)
        {
            _emplyeeService = emplyeeService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(Roles = "Admin")]
        [HttpGet(Name = "GetEmployees")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _emplyeeService.GetAllEmployees());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("EmployeeByID")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var employee = await _emplyeeService.GetByID(id);
            if (employee is not null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound("Employee with this id dose not exist");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] Employee employee)
        {
            return Ok(await _emplyeeService.AddEmployee(employee));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Employee employee)
        {

            if (await _emplyeeService.GetByID(employee.EmployeeID) is null)
            {
                return NotFound("Employee with this id dose not exist");
            }
            return Ok(await _emplyeeService.UpdateEmployee(employee));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _emplyeeService.GetByID(id) is null)
            {
                return NotFound("Employee with this id dose not exist");
            }
            ;
            return Ok(await _emplyeeService.DeleteEmployee(id));
        }

    }
}
