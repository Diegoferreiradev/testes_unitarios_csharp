using App.Application.Models;
using App.Application.Models.Enums;
using App.Application.Services;
using App.Application.Services.Interface;
using Moq;

namespace App.Application.Test.CardTests
{
    public class MockExamplesTests
    {
        [Fact(DisplayName = "Testando save com card existente")]
        public void TestandoSaveComCardExistente()
        {
            //Arrange
            Mock<ICardRepository> mockCardRepository = new Mock<ICardRepository>();
            mockCardRepository.Setup(c => c.GetByNumber(It.IsAny<string>())).Returns(CardMock);
            CardService service = new CardService(mockCardRepository.Object);

            var mycard = new Card()
            {
                Number = "12345",
                Id = 10,
                Brand = Brand.Visa,
                Status = CardStatus.Active,
                Responsible = new Responsible(1, "Diego", "Document")
            };

            //act
            var saveMethod = () => service.Save(mycard);

            //Assert
            var exception = Assert.Throws<ArgumentException>(saveMethod);
            Assert.Contains("Já existe um cartão", exception.Message);
        }

        [Fact(DisplayName = "Testando save com card inexistente")]
        public void TestandoSaveComCardInexistente()
        {
            //Arrange
            Mock<ICardRepository> mockCardRepository = new Mock<ICardRepository>();
            mockCardRepository.Setup(c => c.GetByNumber(It.IsAny<string>())).Returns(null as Card);
            mockCardRepository.Setup(c => c.Save(It.IsAny<Card>())).Returns(7);

            CardService service = new CardService(mockCardRepository.Object);

            var mycard = new Card()
            {
                Number = "12345",
                Id = 10,
                Brand = Brand.Visa,
                Status = CardStatus.Active,
                Responsible = new Responsible(1, "Diego", "Document")
            };

            //act
            service.Save(mycard);

            //Assert
            Assert.Equal(7, mycard.Id);
        }

        private Card CardMock()
        {
            return new Card()
            {
                Number = "12345",
                Id = 10,
                Brand = Brand.Visa,
                Status = CardStatus.Active,
                Responsible = new Responsible(1, "Diego", "Document")
            };
        }
    }
}
