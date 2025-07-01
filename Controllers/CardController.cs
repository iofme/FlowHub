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
    public class CardController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Card>> GetCards()
        {
            var card = await unitOfWork.CardRepository.GetAllCardsAsync();
            return Ok(card);
        }

        [HttpPost]
        public async Task<ActionResult<Card>> CreateCard(CardDTO cardDTO)
        {
            var novoCard = new Card
            {
                Nome = cardDTO.Nome,
                Atribuido = cardDTO.Atribuido,
                CriadoPor = cardDTO.CriadoPor,
                DataDeFinalizacao = cardDTO.DataDeFinalizacao,
                Descricao = cardDTO.Descricao,
            };

            unitOfWork.Commit();

            await unitOfWork.CardRepository.CreateCardAsync(novoCard);

            return Ok(novoCard);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CardDTO>> GetCardById(int id)
        {
            var cardId = await unitOfWork.CardRepository.GetCardById(id);
            if(cardId is null) return BadRequest("Não foi possivel achar o card pelo id");
            return Ok(cardId);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CardDTO>> DeleteCard(int id)
        {
            var cardDeleted = await unitOfWork.CardRepository.DeleteCardAsync(id);

            return Ok(cardDeleted);
        }
    }
}