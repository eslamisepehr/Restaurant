using Restaurant.AppManagers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Restaurant.Models.Entities;
using Restaurant.DbManagers.MainModules;

namespace Restaurant.AppManagers.MainModules
{
    public class GeneralAppManager : IGeneralAppManager
    {

        #region Privates
        OrderTypeManager orderTypeManager = new OrderTypeManager();
        InfoManager infoManager = new InfoManager();
        GetInPlaceManager getInPlaceManager = new GetInPlaceManager();
        CourierManager courierManager = new CourierManager();
        UserManager aspNetUserManager = new UserManager();
        UserRoleManager aspNetUserRolesManager = new UserRoleManager();
        DiscountCodeManager discountCodeManager = new DiscountCodeManager();
        FoodManager foodsManager = new FoodManager();
        OrderListManager orderListManager = new OrderListManager();
        OrderManager orderManager = new OrderManager();
        OrderStatusManager orderStatusManager = new OrderStatusManager();
        RestaurantTableManager restaurantTableManager = new RestaurantTableManager();
        UserAddressManager userAddressManager = new UserAddressManager();
        #endregion


        #region OrderType
        public void AddOrderType(OrderType OrderType)
        {
            orderTypeManager.Create(OrderType);
            orderTypeManager.Save();
        }
        public void DeleteOrderType(OrderType OrderType)
        {
            orderTypeManager.Delete(OrderType);
            orderTypeManager.Save();
        }
        public void EditOrderType(OrderType OrderType)
        {
            orderTypeManager.Edit(OrderType);
            orderTypeManager.Save();
        }
        public OrderType FindOrderType(int id)
        {
            return orderTypeManager.Find(id);
        }
        public List<OrderType> FindAllOrderType()
        {
            return orderTypeManager.FindAll();
        }

        #endregion

        #region User
        public void AddUser(AspNetUser user)
        {
            aspNetUserManager.Create(user);
            aspNetUserManager.Save();
        }
        public void DeleteUser(AspNetUser user)
        {
            aspNetUserManager.Delete(user);
            aspNetUserManager.Save();
        }
        public void EditUser(AspNetUser user)
        {
            aspNetUserManager.Edit(user);
            aspNetUserManager.Save();
        }
        public AspNetUser FindUser(string id)
        {
            return aspNetUserManager.Find(id);
        }
        public List<AspNetUser> FindAllUser()
        {
            return aspNetUserManager.FindAll();
        }

        #endregion

        #region Courier
        public void AddCourier(Courier Courier)
        {
            courierManager.Create(Courier);
            courierManager.Save();
        }
        public void DeleteCourier(Courier Courier)
        {
            courierManager.Delete(Courier);
            courierManager.Save();
        }
        public void EditCourier(Courier Courier)
        {
            courierManager.Edit(Courier);
            courierManager.Save();
        }
        public Courier FindCourier(int id)
        {
            return courierManager.Find(id);
        }
        public List<Courier> FindAllCourier()
        {
            return courierManager.FindAll();
        }

        #endregion

        #region GetInPlace
        public void AddGetInPlace(GetInPlace GetInPlace)
        {
            getInPlaceManager.Create(GetInPlace);
            getInPlaceManager.Save();
        }
        public void DeleteGetInPlace(GetInPlace GetInPlace)
        {
            getInPlaceManager.Delete(GetInPlace);
            getInPlaceManager.Save();
        }
        public void EditGetInPlace(GetInPlace GetInPlace)
        {
            getInPlaceManager.Edit(GetInPlace);
            getInPlaceManager.Save();
        }
        public GetInPlace FindGetInPlace(int id)
        {
            return getInPlaceManager.Find(id);
        }
        public List<GetInPlace> FindAllGetInPlace()
        {
            return getInPlaceManager.FindAll();
        }

        #endregion

        #region Info
        public void AddInfo(Info Info)
        {
            infoManager.Create(Info);
            infoManager.Save();
        }
        public void DeleteInfo(Info Info)
        {
            infoManager.Delete(Info);
            infoManager.Save();
        }
        public void EditInfo(Info Info)
        {
            infoManager.Edit(Info);
            infoManager.Save();
        }
        public Info FindInfo(int id)
        {
            return infoManager.Find(id);
        }
        public List<Info> FindAllInfo()
        {
            return infoManager.FindAll();
        }

        #endregion

        #region UserRoles

        public void AddUserRole(AspNetUserRole AspNetUserRole)
        {
            aspNetUserRolesManager.Create(AspNetUserRole);
            aspNetUserRolesManager.Save();
        }

        public void DeleteUserRole(AspNetUserRole AspNetUserRole)
        {
            aspNetUserRolesManager.Delete(AspNetUserRole);
            aspNetUserRolesManager.Save();
        }

        public void EditUserRole(AspNetUserRole AspNetUserRole)
        {
            aspNetUserRolesManager.Edit(AspNetUserRole);
            aspNetUserRolesManager.Save();
        }

        public AspNetUserRole FindUserRole(string id)
        {
            return aspNetUserRolesManager.Find(id);
        }

        public List<AspNetUserRole> FindAllUserRole()
        {
            return aspNetUserRolesManager.FindAll();
        }

        #endregion

        #region DiscountCode
        public void AddDiscountCode(DiscountCode DiscountCode)
        {
            discountCodeManager.Create(DiscountCode);
            discountCodeManager.Save();
        }
        public void DeleteDiscountCode(DiscountCode DiscountCode)
        {
            discountCodeManager.Delete(DiscountCode);
            discountCodeManager.Save();
        }
        public void EditDiscountCode(DiscountCode DiscountCode)
        {
            discountCodeManager.Edit(DiscountCode);
            discountCodeManager.Save();
        }

        public DiscountCode FindDiscountCode(int id)
        {
            return discountCodeManager.Find(id);
        }

        public List<DiscountCode> FindAllDiscountCode()
        {
            return discountCodeManager.FindAll();
        }
        #endregion

        #region Food
        public void AddFood(Food Food)
        {
            foodsManager.Create(Food);
            foodsManager.Save();
        }
        public void DeleteFood(Food Food)
        {
            foodsManager.Delete(Food);
            foodsManager.Save();
        }
        public void EditFood(Food Food)
        {
            foodsManager.Edit(Food);
            foodsManager.Save();
        }
        public Food FindFood(int id)
        {
            return foodsManager.Find(id);
        }
        public List<Food> FindAllFoods()
        {
            return foodsManager.FindAll();
        }
        #endregion

        #region OrderList
        public void AddOrderList(OrderList OrderList)
        {
            orderListManager.Create(OrderList);
            orderListManager.Save();
        }
        public void DeleteOrderList(OrderList OrderList)
        {
            orderListManager.Delete(OrderList);
            orderListManager.Save();
        }
        public void EditOrderList(OrderList OrderList)
        {
            orderListManager.Edit(OrderList);
            orderListManager.Save();
        }
        public OrderList FindOrderList(int id)
        {
            return orderListManager.Find(id);
        }
        public List<OrderList> FindAllOrderList()
        {
            return orderListManager.FindAll();
        }
        #endregion

        #region Order
        public void AddOrder(Order Order)
        {
            orderManager.Create(Order);
            orderManager.Save();
        }
        public void DeleteOrder(Order Order)
        {
            orderManager.Delete(Order);
            orderManager.Save();
        }
        public void EditOrder(Order Order)
        {
            orderManager.Edit(Order);
            orderManager.Save();
        }
        public Order FindOrder(int id)
        {
            return orderManager.Find(id);
        }
        public List<Order> FindAllOrder()
        {
            return orderManager.FindAll();
        }
        #endregion

        #region OrderStatus
        public void AddOrderStatus(OrderStatu OrderStatus)
        {
            orderStatusManager.Create(OrderStatus);
            orderStatusManager.Save();
        }
        public void DeleteOrderStatus(OrderStatu OrderStatus)
        {
            orderStatusManager.Delete(OrderStatus);
            orderStatusManager.Save();
        }
        public void EditOrderStatus(OrderStatu OrderStatus)
        {
            orderStatusManager.Edit(OrderStatus);
            orderStatusManager.Save();
        }
        public OrderStatu FindOrderStatus(int id)
        {
            return orderStatusManager.Find(id);
        }
        public List<OrderStatu> FindAllOrderStatus()
        {
            return orderStatusManager.FindAll();
        }
        #endregion

        #region RestaurantTable
        public void AddRestaurantTable(RestaurantTable RestaurantTable)
        {
            restaurantTableManager.Create(RestaurantTable);
            restaurantTableManager.Save();
        }
        public void DeleteRestaurantTable(RestaurantTable RestaurantTable)
        {
            restaurantTableManager.Delete(RestaurantTable);
            restaurantTableManager.Save();
        }
        public void EditRestaurantTable(RestaurantTable RestaurantTable)
        {
            restaurantTableManager.Edit(RestaurantTable);
            restaurantTableManager.Save();
        }
        public RestaurantTable FindRestaurantTable(int id)
        {
            return restaurantTableManager.Find(id);
        }
        public List<RestaurantTable> FindAllRestaurantTable()
        {
            return restaurantTableManager.FindAll();
        }
        #endregion

        #region UserAddress
        public void AddUserAddress(UserAddress UserAddress)
        {
            userAddressManager.Create(UserAddress);
            userAddressManager.Save();
        }
        public void DeleteUserAddress(UserAddress UserAddress)
        {
            userAddressManager.Delete(UserAddress);
            userAddressManager.Save();
        }
        public void EditUserAddress(UserAddress UserAddres)
        {
            userAddressManager.Edit(UserAddres);
            userAddressManager.Save();
        }
        public UserAddress FindUserAddress(int id)
        {
            return userAddressManager.Find(id);
        }
        public List<UserAddress> FindAllUserAddress()
        {
            return userAddressManager.FindAll();
        }
        #endregion
    }
}
