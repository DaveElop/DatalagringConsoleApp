using ConsoleApp.Entities;
using ConsoleApp.Services;
using ConsoleApp.UserInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class UI : IUI
    {
        private readonly IMainService _mainService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IAddressService _addressService;

        public UI(IMainService mainService, ICustomerService customerService, IProductService productService, IAddressService addressService)
        {
            _mainService = mainService ?? throw new ArgumentNullException(nameof(mainService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        }

        public async Task ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Manage Customers");
                Console.WriteLine("2. Manage Products");
                Console.WriteLine("3. Exit");

                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ManageCustomers();
                        break;
                    case "2":
                        await ManageProducts();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private async Task ManageCustomers()
        {
            while (true)
            {
                Console.WriteLine("1. View All Customers");
                Console.WriteLine("2. Add Customer");
                Console.WriteLine("3. Update Customer");
                Console.WriteLine("4. Delete Customer");
                Console.WriteLine("5. Back");

                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ViewAllCustomers();
                        break;
                    case "2":
                        await AddCustomer();
                        break;
                    case "3":
                        await UpdateCustomer();
                        break;
                    case "4":
                        await DeleteCustomer();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private async Task ViewAllCustomers()
        {
            var customers = await _mainService.GetAllCustomersAsync();
            DisplayCustomers(customers);
        }

        private void DisplayCustomers(IEnumerable<CustomerEntity> customers)
        {
            Console.WriteLine("Customer List:");
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.CustomerID}: {customer.FirstName} {customer.LastName}, Email: {customer.Email}");
            }
        }

        private async Task AddCustomer()
        {
            Console.Write("Enter customer first name: ");
            var firstName = Console.ReadLine();

            Console.Write("Enter customer last name: ");
            var lastName = Console.ReadLine();

            Console.Write("Enter customer email: ");
            var email = Console.ReadLine();

            var newCustomer = new CustomerEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            await _mainService.AddCustomerAsync(newCustomer);
            Console.WriteLine("Customer added successfully.");
        }

        private async Task UpdateCustomer()
        {
            Console.Write("Enter customer ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                var existingCustomer = await _mainService.GetCustomerByIdAsync(customerId);

                if (existingCustomer != null)
                {
                    Console.Write("Enter new first name: ");
                    existingCustomer.FirstName = Console.ReadLine();

                    Console.Write("Enter new last name: ");
                    existingCustomer.LastName = Console.ReadLine();

                    Console.Write("Enter new email: ");
                    existingCustomer.Email = Console.ReadLine();

                    await _mainService.UpdateCustomerAsync(existingCustomer);
                    Console.WriteLine("Customer updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Customer with ID {customerId} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid customer ID.");
            }
        }

        private async Task DeleteCustomer()
        {
            Console.Write("Enter customer ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                var existingCustomer = await _mainService.GetCustomerByIdAsync(customerId);

                if (existingCustomer != null)
                {
                    await _mainService.DeleteCustomerAsync(customerId);
                    Console.WriteLine("Customer deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Customer with ID {customerId} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid customer ID.");
            }
        }

        private async Task ManageProducts()
        {
            while (true)
            {
                Console.WriteLine("1. View All Products");
                Console.WriteLine("2. Add Product");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("5. Back");

                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ViewAllProducts();
                        break;
                    case "2":
                        await AddProduct();
                        break;
                    case "3":
                        await UpdateProduct();
                        break;
                    case "4":
                        await DeleteProduct();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private async Task ViewAllProducts()
        {
            var products = await _mainService.GetAllProductsAsync();
            DisplayProducts(products);
        }

        private void DisplayProducts(IEnumerable<ProductEntity> products)
        {
            Console.WriteLine("Product List:");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID}: {product.ProductName}, Price: {product.Price}");
            }
        }

        private async Task AddProduct()
        {
            Console.Write("Enter product name: ");
            var productName = Console.ReadLine();

            Console.Write("Enter product price: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal productPrice))
            {
                var newProduct = new ProductEntity
                {
                    ProductName = productName,
                    Price = productPrice
                };

                await _mainService.AddProductAsync(newProduct);
                Console.WriteLine("Product added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid product price.");
            }
        }

        private async Task UpdateProduct()
        {
            Console.Write("Enter product ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                var existingProduct = await _mainService.GetProductByIdAsync(productId);

                if (existingProduct != null)
                {
                    Console.Write("Enter new product name: ");
                    existingProduct.ProductName = Console.ReadLine();

                    Console.Write("Enter new product price: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal productPrice))
                    {
                        existingProduct.Price = productPrice;

                        await _mainService.UpdateProductAsync(existingProduct);
                        Console.WriteLine("Product updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid product price.");
                    }
                }
                else
                {
                    Console.WriteLine($"Product with ID {productId} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid product ID.");
            }
        }

        private async Task DeleteProduct()
        {
            Console.Write("Enter product ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                var existingProduct = await _mainService.GetProductByIdAsync(productId);

                if (existingProduct != null)
                {
                    await _mainService.DeleteProductAsync(productId);
                    Console.WriteLine("Product deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Product with ID {productId} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid product ID.");
            }
        }

        public Task ShowMainMenu(IMainService mainService)
        {
            throw new NotImplementedException();
        }
    }
}