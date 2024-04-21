using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPWZTGBOT
{
    class Object_Orders
    {
        [Key]
        public int Id_order { get; set; }
        public int Id_rackAndCell { get; set; }
        public string? Num_phone { get; set; }
        public string? User_Add { get; set; }
        public DateTime Data_accept { get; set; }
        public DateTime Data_end { get; set; }
        public int Id_status { get; set; }
        public string? User_remove { get; set; }
        public DateTime Data_remove { get; set; }
        public Object_Orders() { }
        public Object_Orders(int id_order, string? num_phone, DateTime data_end)
        {
            Id_order = id_order; 
            Num_phone = num_phone;
            Data_end = data_end;
        }
        public Object_Orders(int id_order, int id_rackAndCell, string? num_phone, string? user_Add, DateTime data_accept, DateTime data_end, int id_status)
        {
            Id_order = id_order;
            Id_rackAndCell = id_rackAndCell;
            Num_phone = num_phone;
            User_Add = user_Add;
            Data_accept = data_accept;
            Data_end = data_end;
            Id_status = id_status;
        }
        public Object_Orders(int id_order, int id_rackAndCell, string? num_phone, string? user_Add,
            DateTime data_accept, DateTime data_end, int id_status, string? user_remove, DateTime data_remove)
        {
            Id_order = id_order;
            Id_rackAndCell = id_rackAndCell;
            Num_phone = num_phone;
            User_Add = user_Add;
            Data_accept = data_accept;
            Data_end = data_end;
            Id_status = id_status;
            User_remove = user_remove;
            Data_remove = data_remove;
        }
    }
}
