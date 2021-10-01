using FlowerAPI.ProjModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowerController : ControllerBase
    {
        public readonly dbFlowerStoreContext db;
        public FlowerController(dbFlowerStoreContext db)
        {
            this.db = db;
        }


        //---------------------------------------------Customer Registration-----------------------------
        [HttpPost]
        [Route("RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomer(Customer temp)
        {
            db.Customers.Add(temp);
            await db.SaveChangesAsync();
            return Ok();
        }

        //---------------------------------------------Customer Login------------------------------------
        [HttpGet]
        [Route("CustomerLogin")]
        public async Task<ActionResult<Customer>> CustomerLogin(string tempPhone, string tempPass, string tempType)
        {
            foreach (var temp in await db.Customers.ToListAsync())
            {
                //changed
                if (temp.Phone == tempPhone && temp.Password == tempPass && temp.Vendor == tempType)
                {
                    //changed
                    Customer cus = temp;
                    return Ok(cus);
                }
            }

            return NotFound();

        }

        //---------------------------------------------Get Customer by ID---------------------------------
        [HttpGet]
        [Route("CustomerbyId")]
        public async Task<ActionResult<Customer>> CustomerbyId(int id)
        {
            Customer temp = await db.Customers.FindAsync(id);
            return Ok(temp);
        }

        //---------------------------------------------Update Customer Details-----------------------------
        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(Customer temp)
        {
            db.Entry(temp).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok();
        }

        //---------------------------------------------Delete a Customer by ID----------------------------
        [HttpDelete]
        [Route("DeleteCustomerbyId")]
        public async Task<ActionResult<Customer>> DeleteCustomerbyId(int id)
        {
            Customer temp = await db.Customers.FindAsync(id);
            db.Customers.Remove(temp);
            await db.SaveChangesAsync();
            return Ok();
        }

        //---------------------------------------------Flower Registration-------------------------------
        [HttpPost]
        [Route("RegisterFlower")]
        public async Task<IActionResult> RegisterFlower(Flower temp)
        {
            db.Flowers.Add(temp);
            await db.SaveChangesAsync();
            return Ok();
        }

        //---------------------------------------------Get Flower by ID---------------------------------
        [HttpGet]
        [Route("FlowerbyId")]
        public async Task<ActionResult<Flower>> FlowerbyId(int id)
        {
            Flower temp = await db.Flowers.FindAsync(id);
            return Ok(temp);
        }

        //---------------------------------------------Get Flower by Name-------------------------------
        [HttpGet]
        [Route("FlowerbyName")]
        public async Task<ActionResult<Flower>> FlowerbyId(string flowername)
        {
            List<Flower> flowerlist = new List<Flower>();
            foreach (var temp in await db.Flowers.ToListAsync())
            {
                if (temp.Name.ToLower() == flowername.ToLower())
                {
                    flowerlist.Add(temp);
                }
            }
            return Ok(flowerlist);
        }

        //---------------------------------------------Get All Flower-----------------------------------
        [HttpGet]
        [Route("GetAllFlower")]
        public async Task<ActionResult<Flower>> GetAllFlower()
        {
            return Ok(await db.Flowers.ToListAsync());
        }

        //changed
        //---------------------------------------------Get All Occasions-----------------------------------
        [HttpGet]
        [Route("GetAllOccasion")]
        public async Task<ActionResult<Occasion>> GetAllOccasion()
        {
            return Ok(await db.Occasions.ToListAsync());
        }

        //changed
        //---------------------------------------------Update cart after order placed-----------------------------------
        [HttpGet] 
        [Route("UpdateStatusInCart")]
        public async Task<IActionResult> UpdateStatusInCart(int id) 
        { 
            Cart temp = await db.Carts.FindAsync(id);
            temp.Status = "Placed"; 
            db.Entry(temp).State = EntityState.Modified; 
            await db.SaveChangesAsync(); 
            return Ok(); 
        }

        //---------------------------------------------Update Flower Details-----------------------------
        [HttpPut]
        [Route("UpdateFlower")]
        public async Task<IActionResult> UpdateFlower(Flower temp)
        {
            db.Entry(temp).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok();
        }

        //---------------------------------------------Delete a Flower by ID----------------------------
        [HttpDelete]
        [Route("DeleteFlowerbyId")]
        public async Task<ActionResult<Flower>> DeleteFlowerbyId(int id)
        {
            Flower temp = await db.Flowers.FindAsync(id);
            db.Flowers.Remove(temp);
            await db.SaveChangesAsync();
            return Ok();
        }

        //--------------------------------------Add a item to a Cart-------------------------------------
        [HttpPost]
        [Route("AdditemtoCart")]
        public async Task<IActionResult> AdditemtoCart(Cart temp)
        {
            db.Carts.Add(temp);
            await db.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]
        [Route("CartByCartID")]
        public async Task<ActionResult<Flower>> CartByCartID(int id)
        {
            Cart temp = await db.Carts.FindAsync(id);
            return Ok(temp);
        }
        //--------------------------------------Update quantities-------------------------------------


        //[HttpPut]
        //[Route("DecreaseFLowerQuantity")]
        //public async Task<IActionResult> DecreaseFlowerQuantity(int id,int qty)
        //{
        //    Flower temp = await db.Flowers.FindAsync(id);
        //    temp.AvailableQuantity = temp.AvailableQuantity - qty;
        //    db.Entry(temp).State = EntityState.Modified;
        //    await db.SaveChangesAsync();
        //    return Ok();
        //}


        //[HttpPut]
        //[Route("IncreaseFLowerQuantity")]
        //public async Task<IActionResult> IncreaseFlowerQuantity(int id, int qty)
        //{
        //    Flower temp = await db.Flowers.FindAsync(id);
        //    temp.AvailableQuantity = temp.AvailableQuantity + qty;
        //    db.Entry(temp).State = EntityState.Modified;
        //    await db.SaveChangesAsync();
        //    return Ok();
        //}

        //---------------------------------------Delete a item from Cart----------------------------------
        [HttpDelete]
        [Route("DeleteItemFromCart")]
        public async Task<ActionResult<Cart>> DeleteItemFromCart(int CartID)
        {
            Cart temp = await db.Carts.FindAsync(CartID);
            db.Carts.Remove(temp);
            await db.SaveChangesAsync();
            return Ok();
        }

        //changed
        //-----------------------------Get Cart of a Particular Customer by their ID------------------------
        [HttpGet]
        [Route("CartbyCustID")]
        public async Task<ActionResult<Flower>> CartbyCustID(int id)
        {
            List<Cart> Cartlist = new List<Cart>();
            foreach (var temp in await db.Carts.ToListAsync())
            {
                if (temp.CustomerId == id && temp.Status.Equals("Pending"))
                {
                    Cartlist.Add(temp);
                }
            }
            return Ok(Cartlist);
        }



        [HttpGet]
        [Route("AllCarts")]
        public async Task<ActionResult<Flower>> AllCarts()
        {

            List<Cart> Cartlist = await db.Carts.ToListAsync();
            return Ok(Cartlist);
        }


        [HttpGet]
        [Route("AllOrders")]
        public async Task<ActionResult<Flower>> AllOrders()
        {

            List<OrderDetail> olist = await db.OrderDetails.ToListAsync();
            return Ok(olist);
        }

        //------------------Adding a item to OrderDetails table after successful payment-------------------
        [HttpPost]
        [Route("AddingToOrderDetails")]
        public async Task<ActionResult<OrderDetail>> AddingToOrderDetails(OrderDetail temp)
        {
            db.OrderDetails.Add(temp);
            await db.SaveChangesAsync();
            return Ok();
        }

        //------------------------Get all the OrderDetails for a Customer by their Id---------------------
        [HttpGet]
        [Route("OrderdetailsbyCustomerId")]
        public async Task<ActionResult<Flower>> OrderdetailsbyCustomerId(int id)
        {
            List<OrderDetail> OrderDetaillist = new List<OrderDetail>();
            foreach (var temp in await db.OrderDetails.ToListAsync())
            {
                if (temp.CustomerId == id)
                {
                    OrderDetaillist.Add(temp);
                }
            }
            return Ok(OrderDetaillist);
        }

        [HttpGet]
        [Route("OrderByOrderID")]
        public async Task<ActionResult<Flower>> OrderByOrderID(int id)
        {
            OrderDetail temp = await db.OrderDetails.FindAsync(id);
            return Ok(temp);
        }

        [HttpPut]
        [Route("UpdateStatusForOrders")]
        public async Task<IActionResult> UpdateStatusForOrders(OrderDetail temp)
        {
            db.Entry(temp).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}


