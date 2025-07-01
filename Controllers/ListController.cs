using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interface;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<List>>> GetLists()
        {
            var listas = await unitOfWork.ListRepository.GetListAsync();

            return Ok(listas);
        }

        [HttpPost]
        public async Task<ActionResult<List>> CreateList(ListDTO listDTO)
        {
            var listCreated = new List
            {
                NomeLista = listDTO.NomeLista,
            };

            await unitOfWork.ListRepository.CreatedList(listCreated);

            return Ok(listCreated);
        }

        [HttpPost("{idCard:int}/{idList:int}")]
        public async Task<ActionResult<List>> AddCardByList(int idCard, int idList)
        {
            var card = await unitOfWork.CardRepository.GetCardById(idCard);

            await unitOfWork.ListRepository.AddCardByList(card, idList);

            return Ok(card);
        }
    }
}