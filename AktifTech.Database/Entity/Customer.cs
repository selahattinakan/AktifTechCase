﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AktifTech.Database.Entity
{
    public class Customer
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("FullName")]
        [Required(ErrorMessage = "Lütfen müşteri adını giriniz.")]
        public string FullName { get; set; }

        [JsonPropertyName("Mail")]
        [Required(ErrorMessage = "Lütfen müşteri mailini giriniz.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Lütfen geçerli bir mail giriniz.")]
        public string Mail { get; set; }

        [JsonPropertyName("Phone")]
        [Required(ErrorMessage = "Lütfen müşteri telefonunu giriniz.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Lütfen geçerli bir telefon giriniz.")]
        public string Phone { get; set; }

        [JsonPropertyName("Address")]
        [Required(ErrorMessage = "Lütfen müşteri adresini giriniz.")]
        public string Address { get; set; }

        [JsonPropertyName("Password")]
        [Required(ErrorMessage = "Lütfen müşteri şifresini giriniz.")]
        public string Password { get; set; }

    }
}
