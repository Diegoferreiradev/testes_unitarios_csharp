using App.Application.Models;
using App.Application.Models.Enums;
using App.Application.Services.Interface;

namespace App.Application.Services
{
    public class CardService
    {
        private ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public CardService()
        {
            
        }

        public Card CreateCard(Brand brand, Responsible responsible)
        {
            ValidateResponsibleCard(responsible);
            string cardNumber = GenerateCardNumber();

            Card cardCreated = new Card()
            {
                Number = cardNumber,
                Brand = brand,
                Responsible = responsible,
                Status = CardStatus.Active
            };
            return cardCreated;
        }

        public string GenerateCardNumber()
        {
            Random random = new Random();
            int number = random.Next(9999999);
            int digit = random.Next(9);
            return $"{number}-{digit}";
        }


        public void ValidateResponsibleCard(Responsible responsible)
        {
            if (responsible == null)
            {
                throw new ArgumentNullException("Responsável obrigatório.");
            }
            if (string.IsNullOrEmpty(responsible.Name))
            {
                throw new ArgumentNullException("Campo obrigatório: Nome do responsável");
            }
            if (string.IsNullOrEmpty(responsible.Document))
            {
                throw new ArgumentException("Campo obrigatório: Documento do responsável");
            }
        }

        public void Save(Card card)
        {
            var cardExistent = _cardRepository.GetByNumber(card.Number);
            if (cardExistent != null) 
                throw new ArgumentException("Já existe um cartão com este número");

            var idSaved = _cardRepository.Save(card);
            card.Id = idSaved;
        }
    }
}
