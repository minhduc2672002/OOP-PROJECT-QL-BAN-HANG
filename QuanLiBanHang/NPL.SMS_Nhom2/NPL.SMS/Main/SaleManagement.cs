using R2S.Training.Dao;
using R2S.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2S.Training.Main
{
    class SaleManagement
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            //GetAllCustomers();

            //GetAllItemsByOrderId();

            //AddCustomer();

            //DeleteCustomer();

            //ComputeOrderTotal();

            //UpdateCustomer();

            //AddOrder();

            //AddLineItem();

            //UpdateOrderTotal();
        }

        static int InputInt()
        {
            while (true)
            {
                try
                {
                    return Convert.ToInt32(Console.ReadLine());
                }

                catch (Exception)
                {
                    Console.Write("Input only numbers: ");
                }
            }
        }

        /// <summary>
        /// Order_ID check exists
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        static bool isExistOrderID(int order_id)
        {
            try
            {
                const string CheckOrderID = @"SELECT count (*) FROM LineItem WHERE order_id = @id ";
                object[] para = new object[] { order_id };
                object count = DataProvider.Instance.ExecuteScalar(CheckOrderID, para);
                return (int)count != 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Customer_ID check exists
        /// </summary>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        static bool isExistCustomerID(int customer_id)
        {
            try
            {
                const string CheckCustomerID = @"SELECT count (*) FROM Customer WHERE customer_id = @id ";
                object[] para = new object[] { customer_id };
                object count = DataProvider.Instance.ExecuteScalar(CheckCustomerID, para);
                return (int)count != 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Product_ID check exists
        /// </summary>
        /// <param name="product_id"></param>
        /// <returns></returns>
        static bool isExistProductID(int product_id)
        {
            try
            {
                const string CheckProductID = @"SELECT count (*) FROM Product WHERE product_id = @id ";
                object[] para = new object[] { product_id };
                object count = DataProvider.Instance.ExecuteScalar(CheckProductID, para);
                return (int)count != 0;
            }
            catch
            {
                return false;
            }
        }

        //Func1
        static void GetAllCustomers()
        {
            List<Customer> customersList = CustomerDAO.Instance.GetAllCustomer();

            foreach (Customer customer in customersList)
            {
                Console.Write(customer.CustomerId + " ");
                Console.WriteLine(customer.CustomerName);
            }
        }

        //Func3
        static void GetAllItemsByOrderId()
        {
            Console.Write("Enter order ID: ");
            int order_id = InputInt();

            List<LineItem> lineItems = new List<LineItem>();
            lineItems = LineItemDAO.Instance.GetAllItemsByOrderId(order_id);

            if (lineItems.Count == 0)
                Console.WriteLine("No item found!");
            else
            {
                foreach (LineItem lineItem in lineItems)
                {
                    Console.Write(lineItem.OrderId + " ");
                    Console.Write(lineItem.ProductId + " ");
                    Console.Write(lineItem.Quantity + " ");
                    Console.WriteLine(lineItem.Price);
                }
            }
        }

        //Func4
        static void ComputeOrderTotal()
        {
            int order_id;
            while (true)
            {
                Console.Write("Enter order ID: ");
                order_id = InputInt();

                if (isExistOrderID(order_id))
                    break;
                Console.WriteLine("Order ID does not exist!");
            }

            double orderTotal = OrderDAO.Instance.ComputeOrderTotal(order_id);
            Console.WriteLine("Order total: " + orderTotal);
        }
        
        //Func5
        static void AddCustomer()
        {
            Customer customer = new Customer();
            Console.Write("Enter customer name: ");
            customer.CustomerName = Console.ReadLine();

            bool status = CustomerDAO.Instance.AddCustomer(customer);
            if (status)
                Console.WriteLine("Add successful!");
            else Console.WriteLine("Add failed!");
        }

        //Func6
        static void DeleteCustomer()
        {
            int customer_id;
            while (true)
            {
                Console.Write("Enter customer ID: ");
                customer_id = InputInt();

                if (isExistCustomerID(customer_id))
                    break;
                Console.WriteLine("Customer ID does not exist!");
            }

            bool status = CustomerDAO.Instance.DeleteCustomer(customer_id);
            if (status)
                Console.WriteLine("Delete successful!");
            else Console.WriteLine("Delete failed!");
        }

        //Func7
        static void UpdateCustomer()
        {
            int customer_id;
            while (true)
            {
                Console.Write("Enter customer ID: ");
                customer_id = InputInt();

                if (isExistCustomerID(customer_id))
                    break;
                Console.WriteLine("Customer ID does not exist!");
            }

            Console.Write("Enter customer name: ");
            string customer_name = Console.ReadLine();

            Customer customer = new Customer(customer_id, customer_name);

            bool status = CustomerDAO.Instance.UpdateCustomer(customer);
            if (status)
                Console.WriteLine("Update successful!");
            else Console.WriteLine("Update failed!");
        }

        //Func8
        static void AddOrder()
        {
            Order order = new Order
            {
                OrderDate = DateTime.Now
            };

            Console.Write("Enter customer ID: ");
            order.CustomerId = InputInt();

            Console.Write("Enter employee ID: ");
            order.EmployeeId = InputInt();

            bool status = OrderDAO.Instance.AddOrder(order);
            if (status)
                Console.WriteLine("Add successful!");
            else Console.WriteLine("Add failed!");
        }

        //Func9
        static void AddLineItem()
        {
            LineItem item = new LineItem();

            int order_id;
            while (true)
            {
                Console.Write("Enter order ID: ");
                order_id = InputInt();

                if (isExistOrderID(order_id))
                    break;
                Console.WriteLine("Order ID does not exist!");
            }
            item.OrderId = order_id;

            int product_id;
            while (true)
            {
                Console.Write("Enter product ID: ");
                product_id = InputInt();

                if (isExistProductID(product_id))
                    break;
                Console.WriteLine("Product ID does not exist!");
            }
            item.ProductId = product_id;

            Console.Write("Enter quantity: ");
            item.Quantity = InputInt();

            Console.Write("Enter price: ");
            item.Price = Convert.ToDouble(Console.ReadLine());

            bool status = LineItemDAO.Instance.AddLineItem(item);

            if (status)
                Console.WriteLine("Add successful!");
            else Console.WriteLine("Add failed!");
        }

        //Func10
        static void UpdateOrderTotal()
        {
            int order_id;
            while (true)
            {
                Console.Write("Enter order ID: ");
                order_id = InputInt();

                if (isExistOrderID(order_id))
                    break;
                Console.WriteLine("Order ID does not exist!");
            }

            bool status = OrderDAO.Instance.UpdateOrderTotal(order_id);

            if (status)
                Console.WriteLine("Update successful!");
            else Console.WriteLine("Update failed!");
        }
    }
}