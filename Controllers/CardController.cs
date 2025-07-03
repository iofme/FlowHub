using API.DTOs;
using API.Extensions;
using API.Interface;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Card>> GetCards()
        {
            var card = await unitOfWork.CardRepository.GetAllCardsAsync();
            return Ok(card);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Card>> CreateCard(CardDTO cardDTO)
        {
            var novoCard = new Card
            {
                Nome = cardDTO.Nome,
                Atribuido = cardDTO.Atribuido,
                CriadoPor = unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername()).ToString()!,
                DataDeFinalizacao = cardDTO.DataDeFinalizacao,
                Descricao = cardDTO.Descricao,
            };

            unitOfWork.Commit();

            await unitOfWork.CardRepository.CreateCardAsync(novoCard);

            return Ok(novoCard);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<CardDTO>> GetCardById(int id)
        {
            var cardId = await unitOfWork.CardRepository.GetCardById(id);
            if (cardId is null) return BadRequest("Não foi possivel achar o card pelo id");
            return Ok(cardId);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<ActionResult<CardDTO>> DeleteCard(int id)
        {
            var cardDeleted = await unitOfWork.CardRepository.DeleteCardAsync(id);

            return Ok(cardDeleted);
        }
    }
}