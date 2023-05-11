using R2S.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2S.Training.Dao
{
    internal class OrderDAO : IOrderDAO
    {
        private static OrderDAO instance;
        public static OrderDAO Instance
        {
            get { if (instance == null) instance = new OrderDAO(); return OrderDAO.instance; }
            private set { OrderDAO.instance = value; }
        }
        private OrderDAO() { }

        const string ADD_ORDER = @"SP_Add_Order @order_date , @customer_id , @employee_id";
        const string COMPUTE_ORDER_TOTAL = @"SELECT dbo.Fn_ComputeOrderTotal ( @order_id )";
        const string UPDATE_ORDER_TOTAL = @"SP_Update_Order_Total @order_id , @total";


        public bool AddOrder(Order order)
        {
            try
            {
                object[] Para = new object[] { order.OrderDate, order.CustomerId, order.EmployeeId };

                return DataProvider.Instance.ExecuteNonQuery(ADD_ORDER, Para) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public double ComputeOrderTotal(int orderId)
        {
            object[] Para = new object[] { orderId };

            var orderTotal = DataProvider.Instance.ExecuteScalar(COMPUTE_ORDER_TOTAL, Para);

            if (orderTotal is DBNull)
                return 0;
            return Convert.ToDouble(orderTotal);
        }

        public List<Order> GetAllOrdersByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrderTotal(int orderId)
        {
            try
            {
                double orderTotal = ComputeOrderTotal(orderId);
                object[] Para = new object[] { orderId, orderTotal };

                return DataProvider.Instance.ExecuteNonQuery(UPDATE_ORDER_TOTAL, Para) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}