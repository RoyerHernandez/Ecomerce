using Ecomerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecomerce.Clases
{
    public class MovementsHelper : IDisposable
    {
        private static EcomerceContext db = new EcomerceContext();


        public void Dispose()
        {
            db.Dispose();
        }

        internal static Response NewOrder(NewOrderView view, string userName)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
                    var order = new Order
                    {
                        CompanyId = user.CompanyId,
                        CustomerId = view.CustomerId,
                        Date = view.Date,
                        Remarks = view.Remarks,
                        StateId = DbHelper.GetState("Create", db),
                    };
                    db.Orders.Add(order);
                    db.SaveChanges();
                    var details = db.OrderDetailTemps.Where(odt => odt.UserName == user.UserName).ToList();

                    foreach (var detail in details)
                    {
                        var orderDetail = new OrderDetail
                        {
                            Description = detail.Description,
                            OrderId = order.OrderId,
                            Price = detail.Price,
                            ProductId = detail.ProductId,
                            Quantity = detail.Quantity,
                            TaxRate = detail.TaxRate,
                        };

                        db.OrderDetails.Add(orderDetail);
                        db.OrderDetailTemps.Remove(detail);
                    }

                    db.SaveChanges();
                    transaction.Commit();
                    return new Response { Succeeded = true, };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response
                    {
                        Message = ex.Message,
                        Succeeded = false,
                    };
                }
            }
        }
    }
}