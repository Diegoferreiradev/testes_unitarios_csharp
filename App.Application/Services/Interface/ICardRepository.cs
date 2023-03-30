using App.Application.Models;

namespace App.Application.Services.Interface
{
    public interface ICardRepository
    {
        Card GetByNumber(string number);
        int Save(Card myCard);
    }
}
