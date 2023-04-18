using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cellphone.Models
{
    public class GioHangList
    {
        private List<BuyProduct> giohang = new List<BuyProduct>();

        public void AddItem(BuyProduct item)
        {
            BuyProduct existingItem = giohang.FirstOrDefault(i => i.ID == item.ID);
            if (existingItem != null)
            {
                existingItem.Soluong += item.Soluong;
            }
            else
            {
                giohang.Add(item);
            }
        }

        public void RemoveItem(int id)
        {
            BuyProduct itemToRemove = giohang.FirstOrDefault(i => i.ID == id);
            if (itemToRemove != null)
            {
                giohang.Remove(itemToRemove);
            }
        }

        public void UpdateItem(int index, int quantity)
        {
            giohang[index].Soluong = quantity;
        }

        public decimal GetTotal()
        {
            return giohang.Sum(item => (decimal)item.ThanhTien());
        }

        public IEnumerable<BuyProduct> GetGioHang()
        {
            return giohang;
        }
    }
}