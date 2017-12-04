using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlowerShop.Models
{
    public class User
    {
        public int ID { get; set; }
       
        [Display(Name = "用户名")]
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "必须是[3，20]个字符")]
        public string Username { get; set; }

        [Display(Name = "密码")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "必须是[6，12]个字符")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "电话")]
        [StringLength(11)]
        [Required]
        public string Phone { get; set; }

        [Display(Name = "电子邮件")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "公司")]
        [Required]
        public string Company { get; set; }

        [Display(Name = "地址")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string Address { get; set; }
    }
}