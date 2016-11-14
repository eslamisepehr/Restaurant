using Microsoft.AspNet.Identity;
using Restaurant.AppManagers.MainModules;
using Restaurant.Models.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Restaurant.Controllers.User
{
    [Authorize]
    public class OrderController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();
        // GET: Order
        public ActionResult Index()
        {
            // Show Foods
            var orderList = userAppManager.FindAllOrderList().Where(a => a.UserId == User.Identity.GetUserId() && a.IsPay == false).OrderByDescending(a => a.OrderDate);
            /// Food Price
            ViewBag.SumPrice = userAppManager.FindAllOrderList().Where(a => a.UserId == User.Identity.GetUserId() && a.IsPay == false).Sum(a => a.Food.Price * a.FoodCount);
            return View(orderList);
        }

        #region Index
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderList order = userAppManager.FindOrderList(id);
            if (order.UserId == User.Identity.GetUserId())
                return View(order);
            if (order == null)
            {
                return HttpNotFound();
            }
            return HttpNotFound();
        }

        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderList orderList = userAppManager.FindOrderList(id);
            if (orderList.UserId == User.Identity.GetUserId() && orderList.IsPay == false)
                return View(orderList);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,FoodId,FoodCount,OrderDate,Note,isPay,OrderId")] OrderList orderList)
        {
            if (orderList.IsPay == false)
            {
                orderList.UserId = User.Identity.GetUserId();
                orderList.IsPay = false;
                orderList.OrderId = null;
                orderList.OrderDate = DateTime.Now;
                userAppManager.EditOrderList(orderList);
                return RedirectToAction("Index");

            }
            return View(orderList);
        }

        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderList orderList = userAppManager.FindOrderList(id);
            if (orderList.UserId == User.Identity.GetUserId())
                return View(orderList);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            return HttpNotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderList orderList = userAppManager.FindOrderList(id);
            userAppManager.DeleteOrderList(orderList);
            return RedirectToAction("Index");
        }

        #endregion

        [ValidateAntiForgeryToken]
        public ActionResult Way()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Way
        [ValidateAntiForgeryToken]
        public ActionResult Courier()
        {
            var list_address = userAppManager.FindAllUserAddress().Where(a => a.UserId == User.Identity.GetUserId()).OrderByDescending(a => a.Id);
            if (list_address == null)
            {
                return RedirectToAction("Address", "Panel");
            }
            return View(list_address);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ReserveTable([Bind(Include = "RestaurantTableCount")] string RestaurantTableCount, [Bind(Include = "dateTime")] string dateTime)
        {
            if (RestaurantTableCount != null && dateTime != null)
            {
                if (DateTime.Parse(dateTime) >= DateTime.Now.AddMinutes(18))
                {
                    #region Find Table Count by Info
                    Info info = userAppManager.FindInfo(1);
                    int restaurantFreeTable = userAppManager.FindAllRestaurantTable().Where(a => a.Date == DateTime.Parse(dateTime) && a.Date == DateTime.Parse(dateTime).AddMinutes(30) && a.Date.AddMinutes(30) == DateTime.Parse(dateTime)).Sum(a => a.Count);
                    int FreeTable = info.Count - restaurantFreeTable;
                    #endregion

                    #region Check Tables Count
                    if (Convert.ToInt32(RestaurantTableCount) <= FreeTable)
                    {
                        #region Send Params With ViewData
                        ViewData["RestaurantTableCount"] = RestaurantTableCount;
                        ViewData["DateTime"] = dateTime;
                        ViewData["Message1"] = $"تبریک! شما می توانید به تعداد {RestaurantTableCount} نفر صندلی و میز رزرو کنید.";
                        #endregion
                    }
                    else
                    {
                        ViewData["Message2"] = $"متاسفانه هم اکنون به تعداد {RestaurantTableCount} نفر نمی توانید رزرو کنید.";
                    }
                    #endregion
                }
                else
                {
                    ViewData["Message2"] = "متاسفانه شما تاریخ گذشته را انتخاب کردید! لطفا تاریخ آینده را انتخاب کنید.";
                }
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult GetInPlace()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #endregion

        public class CheckProp
        {
            #region Prop
            public string DiscountCode { get; set; }
            public string dateTime { get; set; }
            public string Way { get; set; }
            public string Message_Way { get; set; }
            public string UserAddressId { get; set; }
            public string RestaurantTableCount { get; set; }
            #endregion
        }

        //string DiscountCode, string dateTime, string Way, string Message_Way, string UserAddressId, string RestaurantTableCount

        [ValidateAntiForgeryToken]
        public ActionResult Check([Bind(Include = "DiscountCode,dateTime,Way,Message_Way,UserAddressId,RestaurantTableCount")] CheckProp checkProp)
        {
            if (checkProp.dateTime != null && DateTime.Parse(checkProp.dateTime) >= DateTime.Now.AddMinutes(18))
            {
                #region Queries
                var OrderList = userAppManager.FindAllOrderList().Where(a => a.UserId == User.Identity.GetUserId() && a.IsPay == false).OrderByDescending(a => a.OrderDate);
                var SumPrice = OrderList.Sum(a => a.Food.Price * a.FoodCount);
                #endregion

                #region Courier || UserAddressId
                if (checkProp.Way == "1")
                {
                    if (checkProp.UserAddressId != null)
                    {
                        #region Find UserAddress by Id
                        UserAddress userAddress = userAppManager.FindUserAddress(Convert.ToInt32(checkProp.UserAddressId));
                        #endregion

                        #region Send Params with ViewData
                        ViewData["Way"] = checkProp.Way;
                        ViewData["UserAddressId"] = checkProp.UserAddressId;
                        ViewData["Message_Way"] = $"سفارش شما در تاریخ و ساعت : {checkProp.dateTime}  توسط پیک به آدرس {userAddress.Place} تحویل داده خواهد شد.";
                        #endregion
                    }
                }
                #endregion
                #region Reserve Table || RestaurantTableCount
                if (checkProp.Way == "2")
                {
                    if (checkProp.RestaurantTableCount != null)
                    {
                        #region Send Params with ViewData
                        ViewData["Way"] = checkProp.Way;
                        ViewData["RestaurantTableCount"] = checkProp.RestaurantTableCount;
                        ViewData["Message_Way"] = $"برای شما در تاریخ و ساعت : {checkProp.dateTime}  به تعداد {checkProp.RestaurantTableCount} نفر میز و صندلی رزرو خواهد شد.";
                        #endregion
                    }
                }
                #endregion
                #region Get In Resturant
                if (checkProp.Way == "3")
                {
                    #region Send Params with ViewData
                    ViewData["Way"] = checkProp.Way;
                    ViewData["Message_Way"] = $"شما می بایست به صورت حضوری در تاریخ و ساعت : {checkProp.dateTime} در محل رستوران سفارش خود را تحویل بگیرید.";
                    #endregion
                }
                #endregion

                #region Discount Code
                if (checkProp.DiscountCode != null)
                {
                    #region Query
                    var Discountcode_list = userAppManager.FindAllDiscountCode().Single(a => a.Code == checkProp.DiscountCode);
                    #endregion

                    #region Send Params with ViewData
                    if (Discountcode_list != null)
                    {
                        ViewData["Status_DiscountCode"] = "1";
                        ViewData["Message_DiscountCode"] = $"کد تخفیف صحیح است و برای شما به میزان {Discountcode_list.Persent} درصد تخفیف اعمال می شود.";
                        ViewData["SumPrice_DiscountCode"] = SumPrice * (100 - Discountcode_list.Persent) / 100;
                    }
                    else
                    {
                        ViewData["Status_DiscountCode"] = "2";
                        ViewData["Message_DiscountCode"] = "کد تخفیف وارد شده صحبح نمی باشد!";
                    }
                    #endregion
                }
                #endregion

                #region Send Params for All
                ViewData["SumPrice"] = SumPrice;
                ViewData["DiscountCode"] = checkProp.DiscountCode;
                ViewData["DateTime"] = checkProp.dateTime;
                ViewData["Way"] = checkProp.Way;
                ViewData["UserAddressId"] = checkProp.UserAddressId;
                ViewData["RestaurantTableCount"] = checkProp.RestaurantTableCount;

                if (checkProp.Message_Way != null)
                {
                    ViewData["Message_Way"] = checkProp.Message_Way;
                }
                #endregion

                return View(OrderList);
            }
            else
            {
                ViewData["Message"] = "در انتخاب تاریخ دقت فرمایید! لطفا 20 دقیقه اضافه بر زمان هم اکنون تاریخ و ساعت را ثبت نمایید.";
                return View();
            }
        }

        public class OrderValue
        {
            #region Prop
            public string DiscountCode { get; set; }
            public string dateTime { get; set; }
            public string Way { get; set; }
            public string UserAddressId { get; set; }
            public string RestaurantTableCount { get; set; }
            #endregion
        }

        [ValidateAntiForgeryToken]
        public ActionResult Pay([Bind(Include = "DiscountCode,dateTime,Way,UserAddressId,RestaurantTableCount")]OrderValue orderValue)
        {
            try
            {
                #region Queries
                var OrderList = userAppManager.FindAllOrderList().Where(a => a.UserId == User.Identity.GetUserId() && a.IsPay == false).OrderByDescending(a => a.OrderDate);
                int SumPrice = 0;
                try
                {
                    var Discountcode_list = userAppManager.FindAllDiscountCode().Single(a => a.Code == orderValue.DiscountCode);
                    #endregion

                    #region Total Price Use Discount Code
                    SumPrice = OrderList.Sum(a => a.Food.Price * a.FoodCount) * (100 - Discountcode_list.Persent) / 100;
                    #endregion
                }
                catch
                {
                    SumPrice = OrderList.Sum(a => a.Food.Price * a.FoodCount);
                }
                #region Create Order
                Order order = new Order()
                {
                    UserId = User.Identity.GetUserId(),
                    isPay = true,
                    DiscountCode = orderValue.DiscountCode,
                    TotalPrice = SumPrice,
                    OrderStatusId = 1,
                    OrderTypeId = Convert.ToInt32(orderValue.Way),
                    Date = DateTime.Parse(orderValue.dateTime),
                    OrderDate = DateTime.Now
                };
                userAppManager.AddOrder(order);
                #endregion

                #region OrderList => isPay = true
                foreach (var item in OrderList)
                {
                    OrderList orderlist = userAppManager.FindOrderList(item.Id);
                    orderlist.IsPay = true;
                    orderlist.OrderId = order.Id;
                    userAppManager.EditOrderList(orderlist);
                }
                #endregion

                #region Way
                switch (orderValue.Way)
                {
                    #region Courier || UserAddressId
                    case "1":

                        #region Create Courier
                        Courier courier = new Courier()
                        {
                            UserId = User.Identity.GetUserId(),
                            OrderId = order.Id,
                            UserAddressId = Convert.ToInt32(orderValue.UserAddressId)
                        };
                        userAppManager.AddCourier(courier);
                        #endregion

                        break;
                    #endregion

                    #region #region Reserve Table || RestaurantTableCount
                    case "2":

                        #region Create Restaurant Table
                        RestaurantTable restaurantTable = new RestaurantTable()
                        {
                            UserId = User.Identity.GetUserId(),
                            OrderId = order.Id,
                            Date = Convert.ToDateTime(orderValue.dateTime),
                            Count = Convert.ToInt32(orderValue.RestaurantTableCount),
                        };
                        userAppManager.AddRestaurantTable(restaurantTable);
                        #endregion

                        break;
                    #endregion

                    #region Get In Place
                    case "3":

                        #region Create GetInPlace
                        GetInPlace getInPlcae = new GetInPlace()
                        {
                            UserId = User.Identity.GetUserId(),
                            OrderId = order.Id,
                            Date = DateTime.Parse(orderValue.dateTime)
                        };
                        userAppManager.AddGetInPlace(getInPlcae);
                        #endregion

                        break;
                        #endregion
                }
                #endregion

                #region Pass Params to Pay View
                ViewBag.Message1 = $"تبریک! پرداخت با موفقیت انجام شد! شماره سفارش شما {order.Id} می باشد.";
                #endregion
            }
            catch (Exception ex)
            {
                #region Pass Params to Pay View
                ViewBag.Message2 = ex.Message;
                #endregion
            }

            return View();
        }
    }
}