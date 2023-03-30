
using App.Application.Models;
using App.Application.Models.Enums;
using App.Application.Services;

CardService service = new CardService();

Responsible responsible = new Responsible(1, "Diego", "010203665");

Card card = service.CreateCard(Brand.MasterCard, responsible);
card.Deposit(100);
card.Buy(50);
card.Buy(50);


string result = @$"Saldo: {card.Money} : Número: {card.Number}";

Console.WriteLine(result);
