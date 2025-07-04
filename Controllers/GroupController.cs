using API.DTOs;
using API.Interface;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpPost("createGroup")]
        public async Task<ActionResult<Group>> CreateGroup(GroupDTO groupDTO)
        {
            var newGroup = new Group()
            {
                DisplayName = groupDTO.DisplayName,
            };

            await unitOfWork.GroupRepository.CreateGroup(newGroup);

            return Ok(newGroup);
        }

        [HttpPost("{namUser}/{groupId}")]
        public async Task<ActionResult<Group>> AddUserByGroup(string namUser, int groupId)
        {
            var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(namUser);

            if (user == null) return NotFound($"Usuário {namUser} não encontrado");

            var group = await unitOfWork.GroupRepository.GetGroupById(groupId);
            if (group == null) return NotFound($"Grupo com ID {groupId} não encontrado");

            await unitOfWork.GroupRepository.AddUserByGroup(user, group);

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroup()
        {
            var groups = await unitOfWork.GroupRepository.GetAllGroupsAsync();

            return Ok(groups);
        }
    }
}
