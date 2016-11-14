using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Models.Entities;

namespace Restaurant.AppManagers.Interfaces
{
    public interface IGeneralAppManager
    {
        #region AspNetUser
        void AddUser(AspNetUser user);
        void DeleteUser(AspNetUser user);
        void EditUser(AspNetUser user);
        AspNetUser FindUser(string id);
        List<AspNetUser> FindAllUser();
        #endregion

        #region Courier
        void AddCourier(Courier Courier);
        void DeleteCourier(Courier Courier);
        void EditCourier(Courier Courier);
        Courier FindCourier(int id);
        List<Courier> FindAllCourier();
        #endregion

        #region GetInPlace
        void AddGetInPlace(GetInPlace GetInPlace);
        void DeleteGetInPlace(GetInPlace GetInPlace);
        void EditGetInPlace(GetInPlace GetInPlace);
        GetInPlace FindGetInPlace(int id);
        List<GetInPlace> FindAllGetInPlace();
        #endregion

        #region Info
        void AddInfo(Info Info);
        void DeleteInfo(Info Info);
        void EditInfo(Info Info);
        Info FindInfo(int id);
        List<Info> FindAllInfo();
        #endregion

        #region OrderType
        void AddOrderType(OrderType OrderType);
        void DeleteOrderType(OrderType OrderType);
        void EditOrderType(OrderType OrderType);
        OrderType FindOrderType(int id);
        List<OrderType> FindAllOrderType();
        #endregion

        #region AspNetUserRoles
        void AddUserRole(AspNetUserRole AspNetUserRole);
        void DeleteUserRole(AspNetUserRole AspNetUserRole);
        void EditUserRole(AspNetUserRole AspNetUserRole);
        AspNetUserRole FindUserRole(string id);
        List<AspNetUserRole> FindAllUserRole();
        #endregion

        #region DiscountCode
        void AddDiscountCode(DiscountCode DiscountCode);
        void DeleteDiscountCode(DiscountCode DiscountCode);
        void EditDiscountCode(DiscountCode DiscountCode);
        DiscountCode FindDiscountCode(int id);
        List<DiscountCode> FindAllDiscountCode();
        #endregion 

        #region Foods
        void AddFood(Food Food);
        void DeleteFood(Food Food);
        void EditFood(Food Food);
        Food FindFood(int id);
        List<Food> FindAllFoods();
        #endregion

        #region Order
        void AddOrder(Order Order);
        void DeleteOrder(Order Order);
        void EditOrder(Order Order);
        Order FindOrder(int id);
        List<Order> FindAllOrder();
        #endregion

        #region OrderList
        void AddOrderList(OrderList OrderList);
        void DeleteOrderList(OrderList OrderList);
        void EditOrderList(OrderList OrderList);
        OrderList FindOrderList(int id);
        List<OrderList> FindAllOrderList();

        #endregion

        #region OrderStatus
        void AddOrderStatus(OrderStatu OrderStatus);
        void DeleteOrderStatus(OrderStatu OrderStatus);
        void EditOrderStatus(OrderStatu OrderStatus);
        OrderStatu FindOrderStatus(int id);
        List<OrderStatu> FindAllOrderStatus();
        #endregion

        #region RestaurantTable
        void AddRestaurantTable(RestaurantTable RestaurantTable);
        void DeleteRestaurantTable(RestaurantTable RestaurantTable);
        void EditRestaurantTable(RestaurantTable RestaurantTable);
        RestaurantTable FindRestaurantTable(int id);
        List<RestaurantTable> FindAllRestaurantTable();
        #endregion

        #region UserAddress
        void AddUserAddress(UserAddress UserAddres);
        void DeleteUserAddress(UserAddress UserAddres);
        void EditUserAddress(UserAddress UserAddres);
        UserAddress FindUserAddress(int id);
        List<UserAddress> FindAllUserAddress();
        #endregion
    }
}
