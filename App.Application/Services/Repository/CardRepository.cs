using App.Application.Models;
using App.Application.Services.Interface;

namespace App.Application.Services.Repository
{
    public class CardRepository : ICardRepository
    {
        public Card GetByNumber(string number)
        {
            return null;
        }

        public int Save(Card myCard)
        {
            return 1;
        }
    }
}
