using App.Application.Models;
using App.Application.Models.Enums;
using App.Application.Services;

namespace App.Application.Test.CardTests
{
    public class CreateCardTests
    {

        [Fact(DisplayName = "Testando cartão com o número")]
        public void DeveCriarCartaoComNumero()
        {
            //Arrange
            CardService services = new CardService();
            Responsible responsible = new Responsible(0, "Diego", "010203665");

            //Act
            var card = services.CreateCard(Brand.MasterCard, responsible);

            //Assert
            Assert.NotNull(card.Number);
            Assert.NotEqual("", card.Number);        
        }

        [Fact(DisplayName = "Testando cartão com o número sem doc")]
        public void DeveCriarCartaoComNumerov2()
        {
            //Arrange
            CardService services = new CardService();
            Responsible responsible = new Responsible(0, "Diego", "");

            //Act
            Func<Card> card = () => services.CreateCard(Brand.MasterCard, responsible);

            //Assert
            var exception = Assert.Throws<ArgumentException>(card);
            Assert.Contains("campo obrigatório", exception.Message.ToLower());
        }

        [Fact]
        public void TestandoDeposito()
        {
            //Arrange
            CardService services = new CardService();
            Responsible responsible = new Responsible(0, "Diego", "010203665");

            //Act
            Card card = services.CreateCard(Brand.Visa, responsible);
            card.Deposit(1000);

            //Assert
            Assert.Equal(1000, card.Money);
        }

        [Fact]
        public void TestandoCompra()
        {
            //Arrange
            CardService services = new CardService();
            Responsible responsible = new Responsible(0, "Diego", "010203665");

            //Act
            Card card = services.CreateCard(Brand.Visa, responsible);
            card.Deposit(1000);
            card.Buy(200);
            card.Buy(300);

            //Assert
            Assert.Equal(500, card.Money);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("123")]
        [InlineData("12345")]
        public void TestandoDocumentosValidos(string document)
        {
            try
            {
                //Arrange
                CardService services = new CardService();
                Responsible responsible = new Responsible(0, "Diego", "010203665");

                //Act
                services.ValidateResponsibleCard(responsible);

            }
            catch (Exception error)
            {
                //Assert
                Assert.Fail(error.Message);
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestandoDocumentosNaoValidos(string document)
        {
            try
            {
                //Arrange
                CardService services = new CardService();
                Responsible responsible = new Responsible(0, "Diego", document);

                //Act
                services.ValidateResponsibleCard(responsible);
                Assert.Fail("Não esperado");

            }
            catch (Exception error)
            {
                //Assert
                Assert.Contains("Campo obrigatório", error.Message);
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestandoResponsaveisNaoValidos(string responsavel)
        {
            try
            {
                //Arrange
                CardService services = new CardService();
                Responsible responsible = new Responsible(0, responsavel, "010203665");

                //Act
                services.ValidateResponsibleCard(responsible);
                Assert.Fail("Não esperado");

            }
            catch (Exception error)
            {
                //Assert
                Assert.Contains("Campo obrigatório", error.Message);
            }
        }
    }
}
