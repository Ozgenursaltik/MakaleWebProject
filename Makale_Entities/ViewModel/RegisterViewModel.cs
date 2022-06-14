using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Makale_Entities
{
    public class RegisterViewModel
    {        
        [Required(ErrorMessage = "{0} alanı boş geçilemez"), DisplayName("Kullanıcı Takma Adı"),StringLength(25,ErrorMessage ="{0} max.{1} karakter olmalı.")]
        public string KullaniciTakmaAdi { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez"), DisplayName("E-posta"), StringLength(50, ErrorMessage = "{0} max.{1} karakter olmalı."),EmailAddress(ErrorMessage ="{0} alanı için geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez"), DisplayName("Şifre"), DataType(DataType.Password),StringLength(25, ErrorMessage = "{0} max.{1} karakter olmalı.")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez"), DisplayName("Şifre Tekrar"), DataType(DataType.Password),StringLength(25, ErrorMessage = "{0} max.{1} karakter olmalı."),Compare("Sifre",ErrorMessage ="{0} ile {1} uyuşmuyor")]
        public string Sifre2 { get; set; }
    }
}