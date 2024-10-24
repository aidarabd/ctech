using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CandidateController(IUserManager manager) : ControllerBase
{
    [HttpPost(Name = "add-edit-candidate")]
    public async Task<ActionResult> AddEdit(CandidateDto dto)
    {
        //await manager.AddEditUser(dto);
        return Ok();
    }
}