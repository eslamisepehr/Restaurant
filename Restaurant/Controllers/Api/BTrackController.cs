using Restaurant.AppManagers.MainModules;
using Restaurant.Helper.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant.Controllers.Api
{
    public class BTrackController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();

        public JsonResult Details(string UserId)
        {
            ArrayList Track = new ArrayList();
            if (UserId != null)
            {
                var ListOrder = userAppManager.FindAllOrder().Where(a => a.UserId == UserId && a.isPay == true).OrderByDescending(a => a.Id).OrderByDescending(a => a.OrderDate);
                //var OrderList = userAppManager.FindAllOrderList().OrderByDescending(a => a.OrderDate);
                var ListOrderType = userAppManager.FindAllOrderType();
                var ListOrderStatus = userAppManager.FindAllOrderStatus();
                var CountFood = userAppManager.FindAllOrderList();
                var TableCount = userAppManager.FindAllRestaurantTable();
                var Courier = userAppManager.FindAllCourier();

                foreach (var item in ListOrder)
                {
                    var L1 = ListOrderType.Single(a => a.Id == item.OrderTypeId);
                    var L2 = ListOrderStatus.Single(a => a.Id == item.OrderStatusId);
                    //var L3 = OrderList.Where(a => a.UserId == UserId && a.IsPay == true && a.OrderId == item.Id);
                    var L3 = CountFood.Where(a => a.OrderId == item.Id).Sum(a => a.FoodCount);

                    Api_Track apiTrack = new Api_Track()
                    {
                        Id = item.Id,
                        OrderDate = item.OrderDate.ToString(),
                        Date = item.Date.ToString(),
                        TotalPrice = item.TotalPrice,
                        isPay = item.isPay,
                        DiscountCode = item.DiscountCode,
                        OrderTypeName = L1.Name,
                        OrderStatuStatus = L2.Status,
                        FoodCount = L3,
                    };

                    try
                    {
                        if (item.OrderTypeId == 1)
                        {
                            var L4 = Courier.Single(a => a.OrderId == item.Id);
                            if (L4 != null)
                                apiTrack.Address = L4.UserAddress.Place;
                        }
                        if (item.OrderTypeId == 2)
                        {
                            var L5 = TableCount.Single(a => a.OrderId == item.Id);
                            if (L5 != null)
                                apiTrack.TableCount = L5.Count;
                        }
                    }
                    catch { }


                    Track.Add(apiTrack);
                }
                var Tarray = new
                {
                    Track = Track
                };
                return Json(Tarray, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = "Failed" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OrderList(string UserId, int OrderId)
        {
            if (UserId != null && OrderId != null)
            {
                var list = userAppManager.FindAllOrderList().Where(a => a.UserId == UserId && a.IsPay == true && a.OrderId == OrderId).OrderByDescending(a => a.OrderDate);
                int TotalPrice = userAppManager.FindAllOrderList().Where(a => a.UserId == UserId && a.IsPay == true && a.OrderId == OrderId).Sum(a => a.Food.Price * a.FoodCount);

                ArrayList OrderList = new ArrayList();

                foreach (var item in list)
                {
                    Api_OrderList api_OrderList = new Api_OrderList();
                    api_OrderList.FoodName = item.Food.Name;
                    api_OrderList.isPay = item.IsPay;
                    api_OrderList.OrderDate = Convert.ToString(item.OrderDate);
                    api_OrderList.FoodCount = item.FoodCount;
                    api_OrderList.TotalPrice = TotalPrice;
                    OrderList.Add(api_OrderList);
                }

                var Oarray = new
                {
                    OrderList = OrderList
                };
                return Json(Oarray, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = "Failed" }, JsonRequestBehavior.AllowGet);
        }

    }
}