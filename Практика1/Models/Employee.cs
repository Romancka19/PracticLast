using System;
using System.Collections.Generic;

namespace Практика1.Models;

public partial class Employee
{
    public long IdEmployee { get; set; }

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? Post { get; set; }

    public decimal? Salary { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public string Validate()
    {
        if (string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(SecondName) ||
            string.IsNullOrWhiteSpace(Patronymic))
        {
            return "Заполните все обязательные поля.";
        }

        if(FirstName.Length > 15 || SecondName.Length > 15 || Patronymic.Length > 15)
        {
            return "Длина обязательных полей не должна превышать 15 символов!";
        }

        return null; // Возвращаем null, если валидация прошла успешно
    }
}
