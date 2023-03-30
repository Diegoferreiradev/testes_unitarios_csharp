﻿using App.Application.Models.Enums;

namespace App.Application.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Money { get; set; }
        public CardStatus Status { get; set; }
        public Responsible Responsible { get; set; }
        public Brand Brand { get; set; }

        public void Deposit(decimal value)
        {
            Money += value;
        }

        public void Buy(decimal value)
        {
            Money -= value;
        }
    }
}
