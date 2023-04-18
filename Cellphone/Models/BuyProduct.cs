using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cellphone.Models
{
    public class BuyProduct
    {
        mobiledbEntities1 db = new mobiledbEntities1();
        public int ID { get; set; }
        public string TenSP { get; set; }
        public double Giaban  { get; set; }
        public int Soluong { get; set; }
        
        //Tính thành tiền = Giaban * Soluong
        public double ThanhTien()
        {
            return Giaban * Soluong;
        }

        public BuyProduct(int ID)
        {
            this.ID = ID;
            //tìm sản phẩm trong csdl có mã id cần và gán cho buyproduct
            var product = db.SanPhams.Single(s => s.ID == this.ID);
            this.TenSP = product.TenSP;
            this.Giaban = float.Parse(product.GiaBan.ToString());
            this.Soluong = 1;//số lượng ban đầu của 1 mặt hàng là 1 (cho lần click đầu)
        }
    }
}