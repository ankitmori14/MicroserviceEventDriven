using Npgsql;
using OrderService.Model;
using OrderService.RabbitMq;
using System.Collections.Generic;
using System.Data;

namespace OrderService.Repository
{
    /// <summary>
    /// Order class implementations
    /// </summary>
    /// <param name="connection"></param>
    public class Order(NpgsqlConnection connection) : IOrder, IDisposable
    {
        /// <summary>
        /// Get all ordes details
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrderDetails>> GetAllOrders()
        {
            var ordersDetails = new List<OrderDetails>();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT id, name, description, price,quantity FROM userorderdetails.orders;";
            await connection.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (reader is not null)
            {
                while (await reader.ReadAsync())
                {
                    ordersDetails.Add(new OrderDetails()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        name = Convert.ToString(reader["name"]),
                        description = Convert.ToString(reader["description"]),
                        price = Convert.ToInt32(reader["price"]),
                        quantity = Convert.ToInt32(reader["quantity"]),
                    });
                }
            }
            await connection.CloseAsync();
            return ordersDetails.ToList();
        }

        /// <summary>
        /// Get order details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OrderDetails> GetOrdersById(int id)
        {
            var ordersDetails = new OrderDetails();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT id, name, description, price,quantity FROM userorderdetails.orders" +
                " where Id = @Id;";
            cmd.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (reader is not null)
            {
                while (await reader.ReadAsync())
                {

                    ordersDetails.id = Convert.ToInt32(reader["id"]);
                    ordersDetails.name = Convert.ToString(reader["name"]);
                    ordersDetails.description = Convert.ToString(reader["description"]);
                    ordersDetails.price = Convert.ToInt32(reader["price"]);
                    ordersDetails.quantity = Convert.ToInt32(reader["quantity"]);

                }
            }
            await connection.CloseAsync();
            return ordersDetails;
        }

        /// <summary>
        /// Add new orders
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <returns></returns>
        public async Task<bool> AddOrders(OrderDetails orderDetails)
        {
            const string insertQuery =
                "INSERT INTO userorderdetails.orders (name, description, price,quantity) " +
                "VALUES (@name, @description, @price,@quantity)";

            using var cmd = connection.CreateCommand();
            cmd.CommandText = insertQuery;
            AddParameters(cmd, orderDetails);
            await connection.OpenAsync();
            var rowAffected = await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();
            if (rowAffected > 0)
            {
                RabbitMqPublisher<OrderDetails> rabbitMqPublisher = new RabbitMqPublisher<OrderDetails>();
                rabbitMqPublisher.Publish(orderDetails);
            }
            return rowAffected > 0;

        }

        /// <summary>
        /// Set parameters for query
        /// </summary>
        /// <param name="command"></param>
        /// <param name="orderDetails"></param>
        private static void AddParameters(NpgsqlCommand command, OrderDetails orderDetails)
        {
            var parameters = command.Parameters;

            parameters.AddWithValue("@Id", orderDetails.id);
            parameters.AddWithValue("@name", orderDetails.name ?? string.Empty);
            parameters.AddWithValue("@description", orderDetails.description ?? string.Empty);
            parameters.AddWithValue("@price", orderDetails.price);
            parameters.AddWithValue("@quantity", orderDetails.quantity);
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
            GC.SuppressFinalize(this);
        }
    }
}
