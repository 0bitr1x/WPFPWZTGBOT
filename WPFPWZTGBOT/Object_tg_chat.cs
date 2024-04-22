using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPWZTGBOT
{
    class Object_tg_chat
    {
        [Key]
        public string? Num_phone { get; set; }
        public string? Id_chat_tg { get; set; }

        public Object_tg_chat() { }
        public Object_tg_chat(string num_phone)
        {
            Num_phone = num_phone;
        }
        public Object_tg_chat(string num_phone, string id_chat_tg)
        {
            Num_phone = num_phone;
            Id_chat_tg = id_chat_tg;
        }
    }
}
